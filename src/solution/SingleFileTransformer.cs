// ReSharper disable PublicMembersMustHaveComments
// ReSharper disable InternalMembersMustHaveComments

namespace Alphacloud.MSBuild.Xslt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using JetBrains.Annotations;
    using Saxon.Api;

    /// <summary>
    ///     Performs file to file XSLT 3 transformation.
    /// </summary>
    /// <seealso cref="T:Alphacloud.MSBuild.Xslt.ISingleFileTransformer" />
    internal class SingleFileTransformer : ISingleFileTransformer
    {
        private static readonly Lazy<Processor> s_processor = new Lazy<Processor>(CreateProcessor);
        private readonly XsltCompiler _xsltCompiler = CreateCompiler();
        private readonly Dictionary<QName, XdmValue> _externalParameters = new Dictionary<QName, XdmValue>();
        private Xslt30Transformer _xslt30Transformer;


        /// <summary>
        ///     Omit <c>xml</c> declaration from XML output.
        /// </summary>
        public bool OmitXmlDeclaration { get; set; }

        public void LoadXslt([NotNull] Stream xslt)
        {
            if (xslt == null) throw new ArgumentNullException(nameof(xslt));
            var errorList = new ArrayList();
            _xsltCompiler.ErrorList = errorList;
            try
            {
                var xsltExecutable = _xsltCompiler.Compile(xslt);
                _xslt30Transformer = xsltExecutable.Load30();
            }
            catch(Exception ex)
            {
                var errors = from StaticError se in errorList
                    select new ErrorMessage(se.Message, se.LineNumber, se.ColumnNumber);
                throw new XsltException("Error loading XSLT.", ex, errors);
            }
        }

        public void Transform([NotNull] Stream inputXml, [NotNull] Stream output)
        {
            if (inputXml == null) throw new ArgumentNullException(nameof(inputXml));
            if (output == null) throw new ArgumentNullException(nameof(output));

            CheckXsltLoaded();

            var serializer = s_processor.Value.NewSerializer(output);
            if (OmitXmlDeclaration)
                serializer.SetOutputProperty(Serializer.OMIT_XML_DECLARATION, "yes");

            _xslt30Transformer.SetStylesheetParameters(_externalParameters);
            _xslt30Transformer.ApplyTemplates(inputXml, serializer);
            serializer.Close();
        }

        public void AddParameter(string name, [NotNull] string value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument is null or whitespace", nameof(name));
            if (value == null) throw new ArgumentNullException(nameof(value));

            var key = new QName(name);
            _externalParameters[key] = new XdmAtomicValue(value);
        }

        private static Processor CreateProcessor()
        {
            return new Processor();
        }

        private static XsltCompiler CreateCompiler()
        {
            return s_processor.Value.NewXsltCompiler();
        }

        private void CheckXsltLoaded()
        {
            if (_xslt30Transformer == null)
                throw new InvalidOperationException("XSLT was not loaded. Load XSLT document with LoadXslt().");
        }
    }
}

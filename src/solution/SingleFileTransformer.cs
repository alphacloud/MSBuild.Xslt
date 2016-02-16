namespace Alphacloud.MSBuild.Xslt
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using JetBrains.Annotations;
    using Saxon.Api;

    public class SingleFileTransformer
    {
        private static readonly Lazy<Processor> _processor = new Lazy<Processor>(CreateProcessor);
        private static readonly Lazy<XsltCompiler> _xsltCompiler = new Lazy<XsltCompiler>(CreateCompiler);
        private Xslt30Transformer _xslt30Transformer;
        private readonly Dictionary<QName, XdmValue> _externalParamenters = new Dictionary<QName, XdmValue>();

        private static Processor CreateProcessor()
        {
            return new Processor();
        }
        private static XsltCompiler CreateCompiler()
        {
            return _processor.Value.NewXsltCompiler();
        }

        public bool OmitXmlDeclaration { get; set; }

        public void LoadXslt([NotNull] Stream xslt)
        {
            if (xslt == null) throw new ArgumentNullException(nameof(xslt));

            var xsltExecutable = _xsltCompiler.Value.Compile(xslt);
            _xslt30Transformer = xsltExecutable.Load30();
        }

        public void Transform([NotNull] Stream inputXml, [NotNull] Stream output)
        {
            if (inputXml == null) throw new ArgumentNullException(nameof(inputXml));
            if (output == null) throw new ArgumentNullException(nameof(output));

            CheckXsltLoaded();

            var serializer = _processor.Value.NewSerializer(output);
            if (OmitXmlDeclaration)
                serializer.SetOutputProperty(Serializer.OMIT_XML_DECLARATION, "yes");

            _xslt30Transformer.SetStylesheetParameters(_externalParamenters);
            _xslt30Transformer.ApplyTemplates(inputXml, serializer);
            serializer.Close();
        }

        private void CheckXsltLoaded()
        {
            if (_xslt30Transformer == null)
                throw new InvalidOperationException(
                    "XSLT was not loaded. Load XSLT document with LoadXslt().");
        }

        public void AddParameter(string name, [NotNull] string value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument is null or whitespace", nameof(name));
            if (value == null) throw new ArgumentNullException(nameof(value));

            var key = new QName(name);
            _externalParamenters[key] = new XdmAtomicValue(value);
        }
    }
}

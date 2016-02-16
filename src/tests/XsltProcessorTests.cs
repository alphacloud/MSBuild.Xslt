// ReSharper disable PublicMembersMustHaveComments

namespace Alphacloud.MSBuild.Xslt.Tests
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml;
    using NUnit.Framework;
    using Properties;
    using Saxon.Api;

    [TestFixture]
    public class XsltProcessorExperiments
    {
        private Processor _processor;
        private XsltCompiler _compiler;

        private static Stream ToStream(string contents)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(contents));
        }

        private TextWriterDestination CreateFileOutput(string fileName)
        {
            var fs = File.Open(Path.Combine(TestContext.CurrentContext.TestDirectory, fileName),
                FileMode.Create);
            var settings = new XmlWriterSettings
            {
                CloseOutput = true,
                Indent = true,
                OmitXmlDeclaration = true,
                
            };
            var xmlWriter = XmlWriter.Create(new StreamWriter(fs), settings);
             
            var res = new TextWriterDestination(xmlWriter) {CloseAfterUse = true};

            return res;
        }

        private TextWriterDestination CreateStringOutput(StringBuilder sb)
        {
            var settings = new XmlWriterSettings
            {
                CloseOutput = true,
                Indent = true,
                OmitXmlDeclaration = true
            };
            var xmlWriter = XmlWriter.Create(sb, settings);

            return new TextWriterDestination(xmlWriter) {CloseAfterUse = true};
        }


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _processor = new Processor();
            _compiler = _processor.NewXsltCompiler();
        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _compiler = null;
            _processor = null;
        }

        [Test]
        public void CanTransformWithXmlWriter()
        {
            
            var xslt = _compiler.Compile(ToStream(Resources.XsltDupFinder));
            var transformer = xslt.Load30();
            var xdm = CreateFileOutput(@"result.html");
            transformer.ApplyTemplates(ToStream(Resources.XmlDupReport), xdm);
        }

        Serializer CreateStringSerializer(StringBuilder sb)
        {
            return _processor.NewSerializer(new StringWriter(sb, CultureInfo.InvariantCulture));
        }

        [Test]
        public void CanTransformWithSerializer()
        {
            var transformer = LoadXslt(Resources.XsltDupFinder);
            var serializer = _processor.NewSerializer();
            serializer.SetOutputProperty(Serializer.SAXON_INDENT_SPACES, "1");
            serializer.SetOutputProperty(Serializer.INDENT, "yes");
            serializer.SetOutputProperty(Serializer.OMIT_XML_DECLARATION, "yes");
            
            serializer.SetOutputFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "serializer-result.html"));
            transformer.ApplyTemplates(ToStream(Resources.XmlDupReport), serializer);
        }

        private Xslt30Transformer LoadXslt(string xslt)
        {
            var xsltExecutable = _compiler.Compile(ToStream(xslt));
            var transformer = xsltExecutable.Load30();
            return transformer;
        }
    }
}

// ReSharper disable PublicMembersMustHaveComments

// ReSharper disable HeapView.ObjectAllocation.Evident
namespace Alphacloud.MSBuild.Xslt.Tests
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using NUnit.Framework;
    using Properties;
    using Saxon.Api;

    [TestFixture]
    public class XsltProcessorExperiments
    {
        private Processor _processor;
        private XsltCompiler _compiler;


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
            
            var xslt = _compiler.Compile(TestHelpers.ToStream(Resources.XsltDupFinder));
            var transformer = xslt.Load30();
            var xdm = TestHelpers.CreateFileOutput(@"result.html");
            transformer.ApplyTemplates(TestHelpers.ToStream(Resources.XmlDupReport), xdm);
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
            transformer.ApplyTemplates(TestHelpers.ToStream(Resources.XmlDupReport), serializer);
        }

        private Xslt30Transformer LoadXslt(string xslt)
        {
            var xsltExecutable = _compiler.Compile(TestHelpers.ToStream(xslt));
            var transformer = xsltExecutable.Load30();
            return transformer;
        }
    }
}

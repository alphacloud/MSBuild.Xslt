// ReSharper disable PublicMembersMustHaveComments
// ReSharper disable HeapView.ObjectAllocation.Evident
namespace Alphacloud.MSBuild.Xslt.Tests
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using NUnit.Framework;
    using Saxon.Api;

    public static class TestHelpers
    {
        public static Stream ToStream(string contents)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(contents));
        }

        public static TextWriterDestination CreateFileOutput(string fileName)
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

        public static TextWriterDestination CreateStringOutput(StringBuilder sb)
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
    }
}
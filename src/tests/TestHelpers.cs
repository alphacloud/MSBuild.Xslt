// ReSharper disable PublicMembersMustHaveComments
// ReSharper disable HeapView.ObjectAllocation.Evident
// ReSharper disable ExceptionNotDocumented
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
namespace Alphacloud.MSBuild.Xslt.Tests
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using NUnit.Framework;
    using Saxon.Api;

    public static class TestHelpers
    {

        public static MemoryStream LoadResource(string resourceName)
        {
            var assembly = typeof(TestHelpers).Assembly;
            var resourceStream = assembly.GetManifestResourceStream("Alphacloud.MSBuild.Xslt.Tests.TestResources."+ resourceName);
            var data = new byte[resourceStream.Length];
            resourceStream.Read(data, 0, (int) resourceStream.Length);
            return new MemoryStream(data);
        }

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
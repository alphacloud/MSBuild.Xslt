// ReSharper disable PublicMembersMustHaveComments
// ReSharper disable HeapView.ObjectAllocation.Evident
// ReSharper disable InternalMembersMustHaveComments
// ReSharper disable HeapView.ClosureAllocation
// ReSharper disable HeapView.ObjectAllocation

namespace Alphacloud.MSBuild.Xslt.Tests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    internal class SingleFileTransformerXsltErrorReportingTests
    {
        [SetUp]
        public void SetUp()
        {
            _transformer = new SingleFileTransformer();
        }

        [TearDown]
        public void TearDown()
        {
            _transformer = null;
        }

        private SingleFileTransformer _transformer;

        [Test]
        public void CanReportXsltCompilationErrorsInException()
        {
            string[] errors =
            {
                "Attribute @select is not allowed on element <xsl:key>",
                "Element must have an @match attribute",
                "An xsl:key element must either have a @use attribute or have content"
            };

            Action load = () => _transformer.LoadXslt(TestHelpers.LoadResource("BrokenTransform.xslt"));
            load.ShouldThrow<XsltException>()
                .Which.XsltErrors
                .Should().OnlyContain(e => errors.Contains(e.Message));
        }
    }
}

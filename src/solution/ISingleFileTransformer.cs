namespace Alphacloud.MSBuild.Xslt
{
    using System.IO;
    using JetBrains.Annotations;

    /// <summary>
    ///     Performs single file to file transformation.
    /// </summary>
    public interface ISingleFileTransformer
    {
        /// <summary>
        ///     Loads XSLT from the stream.
        /// </summary>
        /// <param name="xslt">The XSLT.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="xslt" /> is null.
        /// </exception>
        void LoadXslt([NotNull] Stream xslt);

        /// <summary>
        ///     Transforms XML.
        /// </summary>
        /// <remarks>
        ///     XSLT must be loaded using
        ///     <see cref="ISingleFileTransformer.LoadXslt(System.IO.Stream)" />
        ///     prior to this call.
        /// </remarks>
        /// <param name="inputXml">The input XML.</param>
        /// <param name="output">The output.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="inputXml" /> or <paramref name="output" /> is null.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        ///     XSLT was not loaded with
        ///     <see cref="ISingleFileTransformer.LoadXslt(System.IO.Stream)" />
        ///     method call.
        /// </exception>
        void Transform([NotNull] Stream inputXml, [NotNull] Stream output);


        /// <summary>
        ///     Passes parameter to XSL transformation.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="name" /> is <see langword="null" /> or empty string.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="value" /> is null.
        /// </exception>
        void AddParameter(string name, [NotNull] string value);
    }
}

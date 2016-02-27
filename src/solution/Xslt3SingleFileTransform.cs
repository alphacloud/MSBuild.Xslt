namespace Alphacloud.MSBuild.Xslt
{
    using System;
    using System.IO;
    using JetBrains.Annotations;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    ///     Performs XSLT 3 file to file transformation.
    /// </summary>
    /// <seealso cref="T:Microsoft.Build.Utilities.Task" />
    [PublicAPI]
    public class Xslt3SingleFileTransform : Task
    {
        private ISingleFileTransformer _transformer;

        /// <summary>
        ///     Source XML file specification.
        /// </summary>
        /// <remarks>
        ///     Item metadata (including well-known file metadata) will be passed as XSLT parameters, overriding
        ///     <see cref="Alphacloud.MSBuild.Xslt.Xslt3SingleFileTransform.Xslt" /> item specification.
        /// </remarks>
        [Required]
        public ITaskItem Input { get; set; }

        /// <summary>
        ///     <see cref="Output" /> file name.
        /// </summary>
        [Required]
        public string Output { get; set; }

        /// <summary>
        ///     XSLT specification.
        /// </summary>
        /// <remarks>
        ///     Item metadata (including well-known file metadata) will be passed as
        ///     XSLT parameters. Parameters with same name will be overwritten by
        ///     <see cref="Alphacloud.MSBuild.Xslt.Xslt3SingleFileTransform.Input" />
        ///     item specification.
        /// </remarks>
        [Required]
        public ITaskItem Xslt { get; set; }

        /// <summary>
        ///     Specifies if <c>xml</c> declaration must be omitted from resulting
        ///     xml.
        /// </summary>
        /// <remarks>
        ///     This setting affects only XML output method. See <c>xsl:output</c>
        ///     for more information.
        /// </remarks>
        public bool OmitXmlDeclaration { get; set; }

        /// <summary>
        /// XSLT transformer abstraction.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">value is null.</exception>
        protected ISingleFileTransformer Transformer
        {
            get { return _transformer ?? (_transformer = new SingleFileTransformer()); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                _transformer = value;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the task successfully executed;
        ///     otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            if (!File.Exists(Input.ItemSpec))
            {
                Log.LogError("Source file '{0}' was not found.", Input.ItemSpec);
                return false;
            }
            if (!File.Exists(Xslt.ItemSpec))
            {
                Log.LogError("XSLT file '{0}' was not found.", Xslt.ItemSpec);
            }
            if (File.Exists(Output))
            {
                Log.LogMessage("Output file '{0}' exists and will be overwritten", Output);
            }

            using (var inputXml = File.OpenRead(Input.ItemSpec))
                using (var xslt = File.OpenRead(Xslt.ItemSpec))
                    using (var output = File.Open(Output, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                    {
                        var transformer = new SingleFileTransformer();
                        try
                        {
                            Log.LogMessage(MessageImportance.Low, "Processing XSLT...");
                            transformer.LoadXslt(xslt);

                            AddParameters(transformer);

                            Log.LogMessage(MessageImportance.Low, "Running transformation...");
                            transformer.Transform(inputXml, output);
                            Log.LogMessage(MessageImportance.Normal, "Transformation result saved to '{0}'", Output);
                        }
                        catch (Exception ex)
                        {
                            Log.LogErrorFromException(ex);
                            return false;
                        }
                    }

            return true;
        }

        private void AddParameters(SingleFileTransformer transformer)
        {
            AddInputParameters(transformer, Xslt, "XSLT");
            AddInputParameters(transformer, Input, "XML");
        }

        private void AddInputParameters(SingleFileTransformer transformer, ITaskItem source, string inputType)
        {
            foreach (string paramName in source.MetadataNames)
            {
                var paramValue = source.GetMetadata(paramName);
                Log.LogMessage(MessageImportance.Low, "Adding {0} parameter '{1}' with value: '{2}'", inputType,
                    paramName, paramValue);
                transformer.AddParameter(paramName, paramValue);
            }
        }
    }
}

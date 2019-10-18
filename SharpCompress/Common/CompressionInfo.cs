using SCFP_Compress.Compressor.Deflate;

namespace SCFP_Compress.Common
{
    /// <summary>
    /// Detailed compression properties when saving.
    /// </summary>
    public class CompressionInfo
    {
        public CompressionInfo()
        {
            DeflateCompressionLevel = CompressionLevel.Default;
        }

        /// <summary>
        /// The algorthm to use.  Must be valid for the format type.
        /// </summary>
        public CompressionType Type { get; set; }

        /// <summary>
        /// When CompressionType.Deflate is used, this property is referenced.  Defaults to CompressionLevel.Default.
        /// </summary>
        public CompressionLevel DeflateCompressionLevel { get; set; }

        public static implicit operator CompressionInfo(CompressionType compressionType)
        {
            return new CompressionInfo() {Type = compressionType};
        }
    }
}
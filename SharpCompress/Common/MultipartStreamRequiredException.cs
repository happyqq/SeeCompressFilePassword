namespace SCFP_Compress.Common
{
    public class MultipartStreamRequiredException : ExtractionException
    {
        public MultipartStreamRequiredException(string message)
            : base(message)
        {
        }
    }
}
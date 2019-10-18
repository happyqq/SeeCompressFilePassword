namespace SCFP_Compress.Common
{
    public class IncompleteArchiveException : ArchiveException
    {
        public IncompleteArchiveException(string message)
            : base(message)
        {
        }
    }
}
namespace SCFP_Compress.Common.Zip.Headers
{
    internal enum ZipHeaderType
    {
        Ignore,
        LocalEntry,
        DirectoryEntry,
        DirectoryEnd,
        Split,
    }
}
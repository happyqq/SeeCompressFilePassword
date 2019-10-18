using SCFP_Compress.Common;

namespace SCFP_Compress.Archive
{
    internal interface IArchiveExtractionListener : IExtractionListener
    {
        void EnsureEntriesLoaded();
        void FireEntryExtractionBegin(IArchiveEntry entry);
        void FireEntryExtractionEnd(IArchiveEntry entry);
    }
}
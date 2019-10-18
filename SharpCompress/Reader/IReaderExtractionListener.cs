using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCFP_Compress.Archive;
using SCFP_Compress.Common;

namespace SCFP_Compress.Reader
{
    internal  interface IReaderExtractionListener:IExtractionListener
    {
//        void EnsureEntriesLoaded();
        void FireEntryExtractionBegin(Entry entry);
        void FireEntryExtractionEnd(Entry entry);
    }
}

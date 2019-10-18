using System;
using System.IO;
using SCFP_Compress.Common;

namespace SCFP_Compress.Writer
{
    public interface IWriter : IDisposable
    {
        ArchiveType WriterType { get; }
        void Write(string filename, Stream source, DateTime? modificationTime);
    }
}
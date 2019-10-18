using System;
using System.IO;
using SCFP_Compress.Common;
using SCFP_Compress.Writer.GZip;
using SCFP_Compress.Writer.Tar;
using SCFP_Compress.Writer.Zip;

namespace SCFP_Compress.Writer
{
    public static class WriterFactory
    {
        public static IWriter Open(Stream stream, ArchiveType archiveType, CompressionType compressionType)
        {
            return Open(stream, archiveType, new CompressionInfo
                                                 {
                                                     Type = compressionType
                                                 });
        }

        public static IWriter Open(Stream stream, ArchiveType archiveType, CompressionInfo compressionInfo)
        {
            switch (archiveType)
            {
                case ArchiveType.GZip:
                    {
                        if (compressionInfo.Type != CompressionType.GZip)
                        {
                            throw new InvalidFormatException("GZip archives only support GZip compression type.");
                        }
                        return new GZipWriter(stream);
                    }
                case ArchiveType.Zip:
                    {
                        return new ZipWriter(stream, compressionInfo, null);
                    }
                case ArchiveType.Tar:
                    {
                        return new TarWriter(stream, compressionInfo);
                    }
                default:
                    {
                        throw new NotSupportedException("Archive Type does not have a Writer: " + archiveType);
                    }
            }
        }
    }
}
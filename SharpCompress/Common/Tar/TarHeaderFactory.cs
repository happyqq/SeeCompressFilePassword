﻿using System.Collections.Generic;
using System.IO;
using SCFP_Compress.Common.Tar.Headers;
using SCFP_Compress.IO;

namespace SCFP_Compress.Common.Tar
{
    internal static class TarHeaderFactory
    {
        internal static IEnumerable<TarHeader> ReadHeader(StreamingMode mode, Stream stream)
        {
            while (true)
            {
                TarHeader header = null;
                try
                {
                    BinaryReader reader = new BinaryReader(stream);
                    header = new TarHeader();
                    if (!header.Read(reader))
                    {
                        yield break;
                    }
                    switch (mode)
                    {
                        case StreamingMode.Seekable:
                            {
                                header.DataStartPosition = reader.BaseStream.Position;
                                //skip to nearest 512
                                reader.BaseStream.Position += PadTo512(header.Size);
                            }
                            break;
                        case StreamingMode.Streaming:
                            {
                                header.PackedStream = new TarReadOnlySubStream(stream, header.Size);
                            }
                            break;
                        default:
                            {
                                throw new InvalidFormatException("Invalid StreamingMode");
                            }
                    }
                }
                catch
                {
                    header = null;
                }
                yield return header;
            }
        }

        private static long PadTo512(long size)
        {
            int zeros = (int) (size%512);
            if (zeros == 0)
            {
                return size;
            }
            return 512 - zeros + size;
        }
    }
}
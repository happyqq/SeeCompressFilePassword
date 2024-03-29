﻿using System;
using System.IO;

namespace SCFP_Compress.Compressor.LZMA
{
    internal static class CRC
    {
        public const uint kInitCRC = 0xFFFFFFFF;
        private static uint[] kTable = new uint[4 * 256];

        static CRC()
        {
            const uint kCrcPoly = 0xEDB88320;

            for (uint i = 0; i < 256; i++)
            {
                uint r = i;
                for (int j = 0; j < 8; j++)
                    r = (r >> 1) ^ (kCrcPoly & ~((r & 1) - 1));

                kTable[i] = r;
            }

            for (uint i = 256; i < kTable.Length; i++)
            {
                uint r = kTable[i - 256];
                kTable[i] = kTable[r & 0xFF] ^ (r >> 8);
            }
        }

        public static uint From(Stream stream, long length)
        {
            uint crc = kInitCRC;
            byte[] buffer = new byte[Math.Min(length, 4 << 10)];
            while (length > 0)
            {
                int delta = stream.Read(buffer, 0, (int)Math.Min(length, buffer.Length));
                if (delta == 0)
                    throw new EndOfStreamException();
                crc = Update(crc, buffer, 0, delta);
                length -= delta;
            }
            return Finish(crc);
        }

        public static uint Finish(uint crc)
        {
            return ~crc;
        }

        public static uint Update(uint crc, byte bt)
        {
            return kTable[(crc & 0xFF) ^ bt] ^ (crc >> 8);
        }

        public static uint Update(uint crc, uint value)
        {
            crc ^= value;
            return kTable[0x300 + (crc & 0xFF)]
                   ^ kTable[0x200 + ((crc >> 8) & 0xFF)]
                   ^ kTable[0x100 + ((crc >> 16) & 0xFF)]
                   ^ kTable[0x000 + (crc >> 24)];
        }

        public static uint Update(uint crc, ulong value)
        {
            return Update(Update(crc, (uint)value), (uint)(value >> 32));
        }

        public static uint Update(uint crc, long value)
        {
            return Update(crc, (ulong)value);
        }

        public static uint Update(uint crc, byte[] buffer, int offset, int length)
        {
            for (int i = 0; i < length; i++)
                crc = Update(crc, buffer[offset + i]);

            return crc;
        }

#if !PORTABLE && !NETFX_CORE
        public static unsafe uint Update(uint crc, byte* buffer, int length)
        {
            while (length > 0 && ((int) buffer & 3) != 0)
            {
                crc = Update(crc, *buffer);
                buffer++;
                length--;
            }

            while (length >= 4)
            {
                crc = Update(crc, *(uint*) buffer);
                buffer += 4;
                length -= 4;
            }

            while (length > 0)
            {
                crc = Update(crc, *buffer);
                length--;
            }

            return crc;
        }

#endif
    }
}
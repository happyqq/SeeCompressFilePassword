﻿using System;
using System.IO;
using SCFP_Compress.Compressor.Rar.PPM;

namespace SCFP_Compress.Compressor.PPMd
{
    public class PpmdStream : Stream
    {
        private PpmdProperties properties;
        private Stream stream;
        private bool compress;
        private I1.Model model;
        private H.ModelPPM modelH;
        private LZMA.RangeCoder.Decoder decoder;
        private long position = 0;
        private bool isDisposed;

        public PpmdStream(PpmdProperties properties, Stream stream, bool compress)
        {
            this.properties = properties;
            this.stream = stream;
            this.compress = compress;

            if (properties.Version == PpmdVersion.I1)
            {
                model = new I1.Model();
                if (compress)
                    model.EncodeStart(properties);
                else
                    model.DecodeStart(stream, properties);
            }
            if (properties.Version == PpmdVersion.H)
            {
                modelH = new H.ModelPPM();
                if (compress)
                    throw new NotImplementedException();
                else
                    modelH.decodeInit(stream, properties.ModelOrder, properties.AllocatorSize);
            }
            if (properties.Version == PpmdVersion.H7z)
            {
                modelH = new H.ModelPPM();
                if (compress)
                    throw new NotImplementedException();
                else
                    modelH.decodeInit(null, properties.ModelOrder, properties.AllocatorSize);
                decoder = new LZMA.RangeCoder.Decoder();
                decoder.Init(stream);
            }
        }

        public override bool CanRead
        {
            get { return !compress; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return compress; }
        }

        public override void Flush()
        {
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposed)
            {
                return;
            }
            isDisposed = true;
            if (isDisposing)
            {
                if (compress)
                    model.EncodeBlock(stream, new MemoryStream(), true);
            }
            base.Dispose(isDisposing);
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get { return position; }
            set { throw new NotSupportedException(); }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (compress)
                return 0;
            int size = 0;
            if (properties.Version == PpmdVersion.I1)
                size = model.DecodeBlock(stream, buffer, offset, count);
            if (properties.Version == PpmdVersion.H)
            {
                int c;
                while (size < count && (c = modelH.decodeChar()) >= 0)
                {
                    buffer[offset++] = (byte) c;
                    size++;
                }
            }
            if (properties.Version == PpmdVersion.H7z)
            {
                int c;
                while (size < count && (c = modelH.decodeChar(decoder)) >= 0)
                {
                    buffer[offset++] = (byte) c;
                    size++;
                }
            }
            position += size;
            return size;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (compress)
                model.EncodeBlock(stream, new MemoryStream(buffer, offset, count), false);
        }
    }
}
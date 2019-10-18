using System;

namespace SCFP_Compress.Common
{
    public class PasswordProtectedException : ExtractionException
    {
        public PasswordProtectedException(string message)
            : base(message)
        {
        }

        public PasswordProtectedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
using System;
using System.Reflection;
using System.Runtime.CompilerServices;


#if PORTABLE
[assembly: AssemblyTitle("SharpCompress.Portable")]
[assembly: AssemblyProduct("SharpCompress.Portable")]
#else

[assembly: AssemblyTitle("SCFP_Compress")]
[assembly: AssemblyProduct("SCFP_Compress")]
#endif

#if UNSIGNED
[assembly: InternalsVisibleTo("SCFP_Compress.Test")]
[assembly: InternalsVisibleTo("SCFP_Compress.Test.Portable")]
[assembly: InternalsVisibleTo("SCFP_Compress.SeeCompressFilePassword")]

#endif

[assembly: InternalsVisibleTo("SCFP_Compress.SeeCompressFilePassword")]

[assembly: CLSCompliant(true)]

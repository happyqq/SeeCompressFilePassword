namespace SCFP_Compress.Compressor.Rar.VM
{
    internal class VMPreparedOperand
    {
        internal VMOpType Type { get; set; }
        internal int Data { get; set; }
        internal int Base { get; set; }
        internal int Offset { get; set; }
    }
}
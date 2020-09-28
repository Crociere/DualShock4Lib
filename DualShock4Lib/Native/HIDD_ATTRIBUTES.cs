using System.Runtime.InteropServices;

internal static partial class NativeMethods
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct HIDD_ATTRIBUTES
	{
		public uint Size;
		public ushort VendorID;
		public ushort ProductID;
		public ushort VersionNumber;
	}
}

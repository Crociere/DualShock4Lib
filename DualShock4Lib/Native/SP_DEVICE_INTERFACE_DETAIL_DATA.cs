using System.Runtime.InteropServices;

internal static partial class NativeMethods
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
	{
		public int cbSize;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string DevicePath;
	}
}
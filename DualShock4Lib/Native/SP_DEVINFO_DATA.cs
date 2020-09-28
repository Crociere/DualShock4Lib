using System;
using System.Runtime.InteropServices;

internal static partial class NativeMethods
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct SP_DEVINFO_DATA
	{
		public UInt32 cbSize;
		public Guid ClassGuid;
		public UInt32 DevInst;
		public IntPtr Reserved;
	}
}
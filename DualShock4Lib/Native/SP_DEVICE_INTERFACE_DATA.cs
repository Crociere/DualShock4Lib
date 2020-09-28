using System;
using System.Runtime.InteropServices;

internal static partial class NativeMethods
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct SP_DEVICE_INTERFACE_DATA
	{
		public Int32 cbSize;
		public Guid interfaceClassGuid;
		public Int32 flags;
		private UIntPtr reserved;
	}
}
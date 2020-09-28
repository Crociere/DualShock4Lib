using System;
using System.Runtime.InteropServices;

internal static partial class NativeMethods
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct SECURITY_ATTRIBUTES
	{
		internal int nLength;
		internal IntPtr lpSecurityDescriptor;
		internal bool bInheritHandle;
	}
}
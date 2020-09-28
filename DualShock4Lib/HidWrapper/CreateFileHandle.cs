using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace HidWrapper
{
	public static partial class Devices
	{
		private static SafeFileHandle CreateFileHandle(
			string devicePath, 
			NativeMethods.EFileAccess desiredAccess = 0, // If this parameter is zero, can query certain metadata without accessing device
			NativeMethods.EFileAttributes fileAttribs = 0 // Use overlapped IO?
		)
		{
			// Default security attributes
			var security = new NativeMethods.SECURITY_ATTRIBUTES
			{
				lpSecurityDescriptor = IntPtr.Zero,
				bInheritHandle = true,
				nLength = Marshal.SizeOf(typeof(NativeMethods.SECURITY_ATTRIBUTES))
			};

			// Get safe handle to device
			var handle = NativeMethods.CreateFile(devicePath,
				desiredAccess,
				NativeMethods.EFileShare.Read | NativeMethods.EFileShare.Write,
				ref security,
				NativeMethods.ECreationDisposition.OpenExisting,
				fileAttribs,
				IntPtr.Zero);

			// Throw exception if handle isn't valid
			if (handle.IsInvalid) Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());

			// Return handle
			return handle;
		}
	}
}
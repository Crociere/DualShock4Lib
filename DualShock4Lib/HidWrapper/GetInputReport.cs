using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace HidWrapper
{
	public static partial class Devices
	{
		public static byte[] GetInputReport(HidDevice device)
		{
			// Buffer to return as result
			byte[] result = null;

			// Create handle with read access and no overlapped IO
			using (var handle = CreateFileHandle(device.DevicePath, NativeMethods.EFileAccess.GenericRead))
			{
				// Length of input buffer
				var bufferLength = device.Capabilities.InputReportByteLength;

				// Create buffer for native call
				IntPtr buffer = Marshal.AllocHGlobal(bufferLength);

				// Empty OVERLAPPED structure
				var overlapped = new NativeOverlapped();

				try
				{
					// Read data
					if (NativeMethods.ReadFile(handle, buffer, (uint)bufferLength, out uint bytesRead, ref overlapped))
					{
						result = new byte[bufferLength];
						Marshal.Copy(buffer, result, 0, (int)bytesRead);
					}
					else
					{
						Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
					}
				}
				finally
				{
					// Release buffer
					Marshal.FreeHGlobal(buffer);
				}
			}

			// Return data
			return result;
		}
	}
}
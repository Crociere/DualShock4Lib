using System.Runtime.InteropServices;

namespace HidWrapper
{
	public static partial class Devices
	{
		public static byte[] GetFeatureReport(HidDevice device, byte reportId)
		{
			// Create buffer for native call
			byte[] buffer = new byte[device.Capabilities.FeatureReportByteLength];

			// Set desired report ID
			buffer[0] = reportId;

			// Create handle with read access and no overlapped IO
			using (var handle = CreateFileHandle(device.DevicePath, NativeMethods.EFileAccess.GenericRead))
			{
				// Read data
				if (!NativeMethods.HidD_GetFeature(handle, buffer, buffer.Length))
				{
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
			}

			// Return data
			return buffer;
		}
	}
}
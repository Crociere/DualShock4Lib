using System;
using System.Runtime.InteropServices;

namespace HidWrapper
{
	public static partial class Devices
	{
		private static string GetDevicePath(IntPtr deviceInfoSet, ref NativeMethods.SP_DEVICE_INTERFACE_DATA deviceInterfaceData)
		{
			// Final result
			string result = null;

			// Object to store device path
			var deviceInterfaceDetail = new NativeMethods.SP_DEVICE_INTERFACE_DETAIL_DATA
			{
				cbSize = (IntPtr.Size == 8) 
					? 8 // 64 bit
					: 4 + Marshal.SystemDefaultCharSize // 32 bit
			};

			// Sizes
			uint requiredSize = 0;
			const uint bufferSize = 1024;

			// Get device path
			if (NativeMethods.SetupDiGetDeviceInterfaceDetail(deviceInfoSet, 
				ref deviceInterfaceData, 
				ref deviceInterfaceDetail, 
				bufferSize, 
				ref requiredSize, 
				IntPtr.Zero))
			{
				result = deviceInterfaceDetail.DevicePath;
			}

			// Return
			return result;
		}
	}
}
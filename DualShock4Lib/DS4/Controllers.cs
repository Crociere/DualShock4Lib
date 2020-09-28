using System;
using System.Collections.Generic;
using System.Linq;
using HidWrapper;

namespace DualShock4Lib
{
	internal static class Controllers
	{
		internal static int VendorId => 1356;
		internal static int[] ProductIds => new int[] { 1476, 2508 };
		
		// Test if device is a DS4
		internal static bool DeviceIsDS4(HidDevice device)
		{
			return device.Attributes.VendorId == VendorId 
				&& ProductIds.Contains(device.Attributes.ProductId);
		}

		// Get all DS4 controllers
		internal static IEnumerable<HidDevice> GetControllers()
		{
			return Devices.EnumerateDevices().Where(DeviceIsDS4);
		}

		// Test if device is connected via USB
		internal static bool IsConnectedToUsb(HidDevice device)
		{
			return (device.Capabilities.InputReportByteLength == 64);
		}

		// Get input report from device
		internal static byte[] GetHidReport(HidDevice device)
		{
			return Devices.GetInputReport(device);
		}
	}
}
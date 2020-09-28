using System.Collections.Generic;
using System.Linq;
using HidWrapper;

namespace DualShock4Lib
{
	internal static class Controllers
	{
		internal static int VendorId => 1356;
		internal static int ProductId => 2508;

		// Get all DS4 controllers
		internal static IEnumerable<HidDevice> GetControllers()
		{
			return Devices.EnumerateDevices()
				.Where(x => x.Attributes.VendorId == VendorId && x.Attributes.ProductId == ProductId);
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
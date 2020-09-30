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
		internal static IEnumerable<Controller> GetControllers()
		{
			foreach(var device in Devices.EnumerateDevices().Where(DeviceIsDS4))
			{
				yield return new Controller(device);
			}
		}

		// Get first DS4 controller
		internal static Controller GetFirstController()
		{
			// Get first device
			var device = Devices.EnumerateDevices().Where(DeviceIsDS4).FirstOrDefault();

			// Return controller
			return new Controller(device);
		}
	}
}
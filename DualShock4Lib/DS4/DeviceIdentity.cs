using System.Linq;
using HidWrapper;

namespace DualShock4Lib
{
	// Test if device is a DS4
	public static class DeviceIdentity
	{
		internal static int VendorId => 1356;
		internal static int[] ProductIds => new int[] { 1476, 2508 };

		public static bool IsDS4(HidDevice device)
		{
			return device.Attributes.VendorId == VendorId 
				&& ProductIds.Contains(device.Attributes.ProductId);
		}
	}
}
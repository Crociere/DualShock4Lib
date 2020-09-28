using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace HidWrapper
{
	public static partial class Devices
	{
		private static HidAttributes GetDeviceAttributes(SafeFileHandle handle)
		{
			// Initialise object
			var attribs = default(NativeMethods.HIDD_ATTRIBUTES);
			attribs.Size = (uint)Marshal.SizeOf(attribs);

			// Get attributes
			NativeMethods.HidD_GetAttributes(handle, ref attribs);
			
			// Copy to POCO
			return new HidAttributes
			{
				VendorId = attribs.VendorID,
				ProductId = attribs.ProductID,
				VersionNumber = attribs.VersionNumber
			};
		}
	}
}
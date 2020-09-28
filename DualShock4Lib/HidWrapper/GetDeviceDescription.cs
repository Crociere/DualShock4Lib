using System;
using System.Text;

namespace HidWrapper
{
	public static partial class Devices
	{
		private static string GetDeviceDescription(IntPtr deviceInfoSet, ref NativeMethods.SP_DEVINFO_DATA deviceInfoData)
		{
			// Output
			uint requiredSize; // unused - actual size of buffer contents
			uint propertyRegDataType; // unused - pointer to variable that receives data type of the property
			var sb = new StringBuilder(1024);

			// Get device description
			if (NativeMethods.SetupDiGetDeviceRegistryProperty(deviceInfoSet,
				ref deviceInfoData,
				NativeMethods.SPDRP.SPDRP_DEVICEDESC,
				out propertyRegDataType,
				sb,
				(uint)sb.Capacity,
				out requiredSize))
			{
				// Return result
				return sb.ToString();
			}

			// Return nothing
			return null;
		}
	}
}
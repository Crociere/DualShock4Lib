using System;
using Microsoft.Win32.SafeHandles;

namespace HidWrapper
{
	public static partial class Devices
	{
		private static HidCapabilities GetDeviceCapabilities(SafeFileHandle handle)
		{
			// Initialise objects
			var caps = default(NativeMethods.HIDP_CAPS);
			var data = default(IntPtr);

			// Get capabilities
			if (NativeMethods.HidD_GetPreparsedData(handle, ref data))
			{
				NativeMethods.HidP_GetCaps(data, ref caps);
				NativeMethods.HidD_FreePreparsedData(data);
			}

			// Copy to POCO
			return new HidCapabilities
			{
				Usage = caps.Usage,
				UsagePage = caps.UsagePage,
				InputReportByteLength = caps.InputReportByteLength,
				OutputReportByteLength = caps.OutputReportByteLength,
				FeatureReportByteLength = caps.FeatureReportByteLength,
				NumberLinkCollectionNodes = caps.NumberLinkCollectionNodes,
				NumberInputButtonCaps = caps.NumberInputButtonCaps,
				NumberInputValueCaps = caps.NumberInputValueCaps,
				NumberInputDataIndices = caps.NumberInputDataIndices,
				NumberOutputButtonCaps = caps.NumberOutputButtonCaps,
				NumberOutputValueCaps = caps.NumberOutputValueCaps,
				NumberOutputDataIndices = caps.NumberOutputDataIndices,
				NumberFeatureButtonCaps = caps.NumberFeatureButtonCaps,
				NumberFeatureValueCaps = caps.NumberFeatureValueCaps,
				NumberFeatureDataIndices = caps.NumberFeatureDataIndices,
			};
		}
	}
}
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace HidWrapper
{
	public static partial class Devices
	{
		public static IEnumerable<HidDevice> EnumerateDevices()
		{
			// Get GUID for the HID class
			NativeMethods.HidD_GetHidGuid(out Guid hidClassGuid);

			// Pointer to device information set
			IntPtr deviceInfoSet = IntPtr.Zero;

			try
			{
				// Get device information set
				var deviceInfoSetFlags = NativeMethods.DiGetClassFlags.DIGCF_PRESENT | NativeMethods.DiGetClassFlags.DIGCF_DEVICEINTERFACE;
				deviceInfoSet = NativeMethods.SetupDiGetClassDevs(ref hidClassGuid, null, IntPtr.Zero, deviceInfoSetFlags);

				// Create SP_DEVINFO_DATA structure
				var deviceInfoData = default(NativeMethods.SP_DEVINFO_DATA);
				deviceInfoData.cbSize = (uint)Marshal.SizeOf(deviceInfoData);

				// Enumerate over device infos
				uint deviceIndex = 0;
				while (NativeMethods.SetupDiEnumDeviceInfo(deviceInfoSet, deviceIndex++, ref deviceInfoData))
				{
					// A device interface in a device information set
					var deviceInterfaceData = new NativeMethods.SP_DEVICE_INTERFACE_DATA();
					deviceInterfaceData.cbSize = Marshal.SizeOf(deviceInterfaceData);

					// Enumerate over devices
					uint memberIndex = 0;
					while (NativeMethods.SetupDiEnumDeviceInterfaces(deviceInfoSet, ref deviceInfoData, ref hidClassGuid, memberIndex++, ref deviceInterfaceData))
					{
						var device = new HidDevice
						{
							DevicePath = GetDevicePath(deviceInfoSet, ref deviceInterfaceData),
							Description = GetDeviceDescription(deviceInfoSet, ref deviceInfoData)
						};

						using (var handle = CreateFileHandle(device.DevicePath))
						{
							device.Capabilities = GetDeviceCapabilities(handle);
							device.Attributes = GetDeviceAttributes(handle);
							device.Manufacturer = GetManufacturer(handle);
							device.Product = GetProduct(handle);
						}

						// Return item
						yield return device;
					}
				}
			}
			finally
			{
				// Deletes a device information set and frees all associated memory
				NativeMethods.SetupDiDestroyDeviceInfoList(deviceInfoSet);
			}
		}
	}
}
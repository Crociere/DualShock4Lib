using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

internal static partial class NativeMethods
{
	// Get GUID for the HID class
	[DllImport("hid.dll", EntryPoint = "HidD_GetHidGuid", SetLastError = true)]
	internal static extern void HidD_GetHidGuid(out Guid Guid);

	// Get device information set
	[DllImport("setupapi.dll", CharSet = CharSet.Auto)]
	internal static extern IntPtr SetupDiGetClassDevs(
		ref Guid ClassGuid,
		[MarshalAs(UnmanagedType.LPTStr)] string Enumerator,
		IntPtr hwndParent,
		DiGetClassFlags Flags);

	// Deletes a device information set and frees all associated memory
	[DllImport("setupapi.dll", SetLastError = true)]
	internal static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

	// Enumerate over devices
	[DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
	internal static extern Boolean SetupDiEnumDeviceInterfaces(
		IntPtr hDevInfo,
		ref SP_DEVINFO_DATA deviceInfoData,
		ref Guid interfaceClassGuid,
		UInt32 memberIndex,
		ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

	// Returns a device path
	[DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
	internal static extern Boolean SetupDiGetDeviceInterfaceDetail(
		IntPtr hDevInfo,
		ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
		ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
		UInt32 deviceInterfaceDetailDataSize,
		ref UInt32 requiredSize,
		IntPtr deviceInfoData);

	// Creates or opens a file or I/O device
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	internal static extern SafeFileHandle CreateFile(
		string lpFileName,
		EFileAccess dwDesiredAccess,
		EFileShare dwShareMode,
		ref SECURITY_ATTRIBUTES lpSecurityAttributes,
		ECreationDisposition dwCreationDisposition,
		EFileAttributes dwFlagsAndAttributes,
		IntPtr hTemplateFile);

	// Reads data from the specified file or input/output (I/O) device
	[DllImport("kernel32.dll", SetLastError = true)]
	internal static extern bool ReadFile(
		SafeFileHandle handle, 
		byte[] bytes, 
		uint numBytesToRead, 
		out uint numBytesRead, 
		ref NativeOverlapped overlapped);

	// Returns a feature report from a specified top-level collection.
	[DllImport("hid.dll", SetLastError = true)]
	internal static extern bool HidD_GetFeature(
		SafeFileHandle HidDeviceObject,
		byte[] ReportBuffer,
		int ReportBufferLength);

	// Returns a top-level collection's preparsed data
	[DllImport("hid.dll", SetLastError = true)]
	internal static extern bool HidD_GetPreparsedData(
		SafeFileHandle HidDeviceObject, 
		ref IntPtr PreparsedData);

	// Releases the resources that the HID class driver allocated to hold a top-level collection's preparsed data
	[DllImport("hid.dll", SetLastError = true)]
	internal static extern bool HidD_FreePreparsedData(IntPtr PreparsedData);

	// Returns a top-level collection's HIDP_CAPS structure
	[DllImport("hid.dll", SetLastError = true)]
	internal static extern uint HidP_GetCaps(
		IntPtr PreparsedData, 
		ref HIDP_CAPS Capabilities);

	// Returns the attributes of a specified top-level collection
	[DllImport("hid.dll", CharSet = CharSet.Auto, SetLastError = true)]
	internal static extern bool HidD_GetAttributes(
		SafeFileHandle HidDeviceObject,
		ref HIDD_ATTRIBUTES Attributes);

	// Returns a top-level collection's embedded string that identifies the manufacturer
	[DllImport("hid.dll", CharSet = CharSet.Auto, SetLastError = true)]
	internal static extern Boolean HidD_GetManufacturerString(
		SafeFileHandle HidDeviceObject,
		StringBuilder Buffer,
		ulong BufferLength);

	// Returns the embedded string of a top-level collection that identifies the manufacturer's product
	[DllImport("hid.dll", CharSet = CharSet.Auto, SetLastError = true)]
	internal static extern bool HidD_GetProductString(
		SafeFileHandle HidDeviceObject,
		StringBuilder Buffer,
		ulong BufferLength);

	// Returns a SP_DEVINFO_DATA structure that specifies a device information element in a device information set
	[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
	internal static extern bool SetupDiEnumDeviceInfo(
		IntPtr hDevInfo,
		uint memberIndex,
		ref SP_DEVINFO_DATA deviceInfoData);

	// Retrieves a specified Plug and Play device property
	[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
	internal static extern bool SetupDiGetDeviceRegistryProperty(
		IntPtr deviceInfoSet,
		ref SP_DEVINFO_DATA deviceInfoData,
		SPDRP property,
		out UInt32 propertyRegDataType,
		StringBuilder propertyBuffer,
		uint propertyBufferSize,
		out UInt32 requiredSize);
}

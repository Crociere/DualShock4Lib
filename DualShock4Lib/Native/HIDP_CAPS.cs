using System.Runtime.InteropServices;

internal static partial class NativeMethods
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct HIDP_CAPS
	{
		internal ushort Usage;
		internal ushort UsagePage;
		internal ushort InputReportByteLength;
		internal ushort OutputReportByteLength;
		internal ushort FeatureReportByteLength;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
		internal ushort[] Reserved;
		internal ushort NumberLinkCollectionNodes;
		internal ushort NumberInputButtonCaps;
		internal ushort NumberInputValueCaps;
		internal ushort NumberInputDataIndices;
		internal ushort NumberOutputButtonCaps;
		internal ushort NumberOutputValueCaps;
		internal ushort NumberOutputDataIndices;
		internal ushort NumberFeatureButtonCaps;
		internal ushort NumberFeatureValueCaps;
		internal ushort NumberFeatureDataIndices;
	}
}
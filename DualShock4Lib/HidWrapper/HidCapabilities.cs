namespace HidWrapper
{
	public class HidCapabilities
	{
		public ushort Usage { get; set; }
		public ushort UsagePage { get; set; }
		public ushort InputReportByteLength { get; set; }
		public ushort OutputReportByteLength { get; set; }
		public ushort FeatureReportByteLength { get; set; }
		public ushort NumberLinkCollectionNodes { get; set; }
		public ushort NumberInputButtonCaps { get; set; }
		public ushort NumberInputValueCaps { get; set; }
		public ushort NumberInputDataIndices { get; set; }
		public ushort NumberOutputButtonCaps { get; set; }
		public ushort NumberOutputValueCaps { get; set; }
		public ushort NumberOutputDataIndices { get; set; }
		public ushort NumberFeatureButtonCaps { get; set; }
		public ushort NumberFeatureValueCaps { get; set; }
		public ushort NumberFeatureDataIndices { get; set; }
	}
}
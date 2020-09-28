namespace HidWrapper
{
	public class HidDevice
	{
		public string DevicePath { get; internal set; }
		public string Description { get; internal set; }
		public string Manufacturer { get; internal set; }
		public string Product { get; internal set; }
		public HidAttributes Attributes { get; internal set; }
		public HidCapabilities Capabilities { get; internal set; }
	}
}
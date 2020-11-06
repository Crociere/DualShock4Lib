using System;
using HidWrapper;

namespace DualShock4Lib
{
	public interface IController
	{
		ushort VendorId { get; }
		ushort ProductId { get; }
		string Manufacturer { get; }
		string Product { get; }
		string DevicePath { get; }
		bool IsConnectedToUsb { get; }
		IBatteryState GetBatteryState();
	}

	public class Controller : IController
	{
		private HidDevice device;

		internal Controller(HidDevice device)
		{
			// Check
			if (device == null) throw new ArgumentNullException("device");

			// Store
			this.device = device;

			// If bluetooth, ask for feature report 0x02 to obtain input report 0x11
			if (!IsConnectedToUsb) Devices.GetFeatureReport(device, 0x02);
		}

		// Provide some HID attributes
		public ushort VendorId => device.Attributes.VendorId;
		public ushort ProductId => device.Attributes.ProductId;
		public string Manufacturer => device.Manufacturer;
		public string Product => device.Product;
		public string DevicePath => device.DevicePath;

		// Returns true if connected via USB
		public bool IsConnectedToUsb
		{
			get
			{
				// InputReportByteLength is used as an indicator for connection type
				return (device.Capabilities.InputReportByteLength == 64);
			}
		}

		// Requests an input report
		internal byte[] GetInputReport()
		{
			return Devices.GetInputReport(device);
		}

		// Requests current battery state for device
		public IBatteryState GetBatteryState()
		{
			// Get input report from device
			byte[] report = GetInputReport();

			// Return battery state
			return new BatteryState(report, IsConnectedToUsb);
		}
	}
}
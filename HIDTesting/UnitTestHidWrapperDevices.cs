using System;
using System.Linq;
using System.Text.Json;
using DualShock4Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HIDTesting
{
	[TestClass]
	public partial class UnitTestHidWrapperDevices
	{
		// Debug output formatting
		static JsonSerializerOptions serializerOptions = new JsonSerializerOptions{ WriteIndented = true };

		[TestMethod]
		public void ListLiveDevices()
		{
			foreach (var device in HidWrapper.Devices.EnumerateDevices())
			{
				System.Diagnostics.Debug.WriteLine($"{JsonSerializer.Serialize(device, serializerOptions)}");
			}
		}

		[TestMethod]
		public void TestEnumerateDevices()
		{
			// Get first device
			var device = HidWrapper.Devices.EnumerateDevices().Where(Controllers.DeviceIsDS4).FirstOrDefault();
			
			// Check
			Assert.IsNotNull(device);
			System.Diagnostics.Debug.WriteLine($"{JsonSerializer.Serialize(device, serializerOptions)}");
		}

		[TestMethod]
		public void TestGetHidFeatureReport()
		{
			// Get first device
			var device = HidWrapper.Devices.EnumerateDevices().Where(Controllers.DeviceIsDS4).FirstOrDefault();
			
			// Check
			Assert.IsNotNull(device);

			// Get report
			byte[] data = HidWrapper.Devices.GetFeatureReport(device, 0x02);

			// Check data is there
			Assert.IsNotNull(data);
		}

		[TestMethod]
		public void TestGetInputReport()
		{
			// Get first device
			var device = HidWrapper.Devices.EnumerateDevices().Where(Controllers.DeviceIsDS4).FirstOrDefault();
			
			// Check
			Assert.IsNotNull(device);

			// Test for USB or BT connection
			var viaUSB = (device.Capabilities.InputReportByteLength == 64);

			// If connected via BT, request feature report 0x02 first
			// This triggers the DS4 into returning input report 0x11 instead of 0x01
			// USB returns a different 0x01 report
			if (!viaUSB)
			{
				HidWrapper.Devices.GetFeatureReport(device, 0x02);
			}

			// Get input report
			var data = HidWrapper.Devices.GetInputReport(device);

			// Check data is there
			Assert.IsNotNull(data);

			// Check report id
			var expectedReportId = viaUSB ? 0x01 : 0x11;
			Assert.AreEqual(expectedReportId, data[0]);

			// Dump
			System.Diagnostics.Debug.WriteLine($"{JsonSerializer.Serialize(data, serializerOptions)}");
		}

		[TestMethod]
		public void TestMultipleGetInputReports()
		{
			// Iterate over controllers			
			foreach(var device in HidWrapper.Devices.EnumerateDevices().Where(Controllers.DeviceIsDS4))
			{
				// Check
				Assert.IsNotNull(device);

				// Get input report
				var data = HidWrapper.Devices.GetInputReport(device);

				// Check
				Assert.IsNotNull(data);
				System.Diagnostics.Debug.WriteLine($"{JsonSerializer.Serialize(data, serializerOptions)}");
			}
		}

		[TestMethod]
		public void TestGetBatteryState()
		{
			// Get device
			var device = HidWrapper.Devices.EnumerateDevices().Where(Controllers.DeviceIsDS4).FirstOrDefault();

			// Get feature report if needed
			bool viaUSB = (device.Capabilities.InputReportByteLength == 64);
			if (!viaUSB) HidWrapper.Devices.GetFeatureReport(device, 0x02);

			// Get input report
			var data = HidWrapper.Devices.GetInputReport(device);

			// Get battery state
			BatteryState battery = new BatteryState(data, viaUSB);

			// Test
			Assert.IsNotNull(battery);
			System.Diagnostics.Debug.WriteLine($"{JsonSerializer.Serialize(battery, serializerOptions)}");
		}
	}
}

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
		public void TestGetInputReport()
		{
			// Get first device
			var device = HidWrapper.Devices.EnumerateDevices().Where(Controllers.DeviceIsDS4).FirstOrDefault();
			
			// Check
			Assert.IsNotNull(device);

			// Get input report
			var data = HidWrapper.Devices.GetInputReport(device);

			// Check data is there
			Assert.IsNotNull(data);

			// Check report id
			var expectedReportId = (device.Capabilities.InputReportByteLength == 64) ? 0x01 : 0x11;
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
			// Get input report
			var device = HidWrapper.Devices.EnumerateDevices().Where(Controllers.DeviceIsDS4).FirstOrDefault();
			var data = HidWrapper.Devices.GetInputReport(device);
			bool viaUSB = (device.Capabilities.InputReportByteLength == 64);

			// Get battery state
			BatteryState battery = BatteryState.GetBatteryState(data, viaUSB);

			// Test
			Assert.IsNotNull(battery);
			System.Diagnostics.Debug.WriteLine($"{JsonSerializer.Serialize(battery, serializerOptions)}");
		}
	}
}

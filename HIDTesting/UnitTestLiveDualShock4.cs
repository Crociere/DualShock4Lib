using System.Linq;
using DualShock4Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HIDTesting
{
	[TestClass]
	public partial class UnitTestLiveDualShock4
	{
		[TestMethod]
		public void TestGetFirstController()
		{
			var device = Controllers.GetControllers().FirstOrDefault();
			Assert.IsNotNull(device);
			Assert.AreEqual(1356, device.Attributes.VendorId);
			Assert.AreEqual(2508, device.Attributes.ProductId);
		}

		[TestMethod]
		public void TestIsNotConnectedToUsb()
		{
			var device = Controllers.GetControllers().FirstOrDefault();
			Assert.IsNotNull(device);

			var result = Controllers.IsConnectedToUsb(device);
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void TestGetHidReport()
		{
			var device = Controllers.GetControllers().FirstOrDefault();
			Assert.IsNotNull(device);

			byte[] report = Controllers.GetHidReport(device);

			// Check data is there
			Assert.IsNotNull(report);

			// Check report id
			var expectedReportId = Controllers.IsConnectedToUsb(device) ? 0x01 : 0x11;
			Assert.AreEqual(expectedReportId, report[0]);

			// Dump
			System.Diagnostics.Debug.WriteLine(System.Text.Json.JsonSerializer.Serialize(report));
		}

		[TestMethod]
		public void TestFirstDS4Battery()
		{
			BatteryState battery = Batteries.GetBatteryState();
			Assert.IsNotNull(battery);
			System.Diagnostics.Debug.WriteLine($"Battery: {battery.Level}% Charging: {battery.ChargingState}");
		}

		[TestMethod]
		public void TestMultipleDS4Batteries()
		{
			var batteries = Batteries.GetAllBatteryStates();
			
			foreach (var battery in batteries)
			{
				Assert.IsNotNull(battery);
				System.Diagnostics.Debug.WriteLine($"Battery: {battery.Level}% Charging: {battery.ChargingState}");
			}
		}
	}
}

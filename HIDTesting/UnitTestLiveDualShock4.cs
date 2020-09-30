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
			var controller = Controllers.GetFirstController();
			Assert.IsNotNull(controller);
			Assert.AreEqual(1356, controller.VendorId);
			Assert.AreEqual(2508, controller.ProductId);
		}

		[TestMethod]
		public void TestIsNotConnectedToUsb()
		{
			var device = Controllers.GetFirstController();
			Assert.IsNotNull(device);
			Assert.AreEqual(false, device.IsConnectedToUsb);
		}

		[TestMethod]
		public void TestGetHidInputReport()
		{
			// Get device
			var device = Controllers.GetFirstController();
			Assert.IsNotNull(device);

			// Get report
			byte[] report = device.GetInputReport();

			// Dump
			System.Diagnostics.Debug.WriteLine(System.Text.Json.JsonSerializer.Serialize(report));

			// Check data is there
			Assert.IsNotNull(report);

			// Check report id
			var expectedReportId = device.IsConnectedToUsb ? 0x01 : 0x11;
			Assert.AreEqual(expectedReportId, report[0]);
			System.Diagnostics.Debug.WriteLine($"Report ID: {report[0]}");
		}

		[TestMethod]
		public void TestFirstDS4Battery()
		{
			BatteryState battery = Controllers.GetFirstController().GetBatteryState();
			Assert.IsNotNull(battery);
			System.Diagnostics.Debug.WriteLine($"Battery: {battery.Level}% Charging: {battery.ChargingState}");
		}

		[TestMethod]
		public void TestMultipleDS4Batteries()
		{
			foreach (var controller in Controllers.GetControllers())
			{
				BatteryState battery = controller.GetBatteryState();
				Assert.IsNotNull(battery);
				System.Diagnostics.Debug.WriteLine($"Battery: {battery.Level}% Charging: {battery.ChargingState}");
			}
		}
	}
}

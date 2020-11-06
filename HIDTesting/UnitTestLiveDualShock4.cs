using DualShock4Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HIDTesting
{
	[TestClass]
	public partial class UnitTestLiveDualShock4
	{
		// Shared controllers provider
		private static IControllersProvider controllers = new Controllers();

		[TestMethod]
		public void TestGetFirstController()
		{
			var controller = controllers.GetFirstController();
			Assert.IsNotNull(controller);
			Assert.AreEqual(1356, controller.VendorId);
			Assert.AreEqual(2508, controller.ProductId);
		}

		[TestMethod]
		public void TestIsNotConnectedToUsb()
		{
			var device = controllers.GetFirstController();
			Assert.IsNotNull(device);
			Assert.AreEqual(false, device.IsConnectedToUsb);
		}

		[TestMethod]
		public void TestGetHidInputReport()
		{
			// Get device
			var device = controllers.GetFirstController() as Controller;
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
			IBatteryState battery = controllers.GetFirstController().GetBatteryState();
			Assert.IsNotNull(battery);
			System.Diagnostics.Debug.WriteLine($"Battery: {battery.Level}% Charging: {battery.ChargingState}");
		}

		[TestMethod]
		public void TestMultipleDS4Batteries()
		{
			foreach (var controller in controllers.GetAllControllers())
			{
				IBatteryState battery = controller.GetBatteryState();
				Assert.IsNotNull(battery);
				System.Diagnostics.Debug.WriteLine($"Battery: {battery.Level}% Charging: {battery.ChargingState}");
			}
		}
	}
}

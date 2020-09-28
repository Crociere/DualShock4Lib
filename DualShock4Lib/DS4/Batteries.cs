using System.Collections.Generic;
using System.Linq;
using HidWrapper;

namespace DualShock4Lib
{
	public static class Batteries
	{
		// Gets battery state for first controller
		public static BatteryState GetBatteryState()
		{
			HidDevice device = Controllers.GetControllers().FirstOrDefault();
			byte[] report = Controllers.GetHidReport(device);
			bool viaUSB = Controllers.IsConnectedToUsb(device);
			return BatteryState.GetBatteryState(report, viaUSB);
		}

		// Gets battery states for all controllers
		public static IEnumerable<BatteryState> GetAllBatteryStates()
		{
			foreach (var device in Controllers.GetControllers())
			{
				byte[] report = Controllers.GetHidReport(device);
				bool viaUSB = Controllers.IsConnectedToUsb(device);
				yield return BatteryState.GetBatteryState(report, viaUSB);
			}
		}
	}
}
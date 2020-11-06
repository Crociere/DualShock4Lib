using System;
using DualShock4Lib;

namespace DS4BatteryCheck
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"Checking DS4 battery levels...");

			IControllersProvider controllers = new Controllers();
			
			foreach (var controller in controllers.GetAllControllers())
			{
				IBatteryState battery = controller.GetBatteryState();
				
				if (battery != null)
				{
					Console.WriteLine($"> Battery: {battery.Level}% State: {battery.ChargingState}");
				}
			}

			Console.WriteLine($"Press any key to exit");
			Console.ReadKey(true);
		}
	}
}

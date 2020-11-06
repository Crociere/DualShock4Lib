using System.Collections.Generic;
using System.Linq;
using HidWrapper;

namespace DualShock4Lib
{
	public interface IControllersProvider
	{
		IEnumerable<IController> GetAllControllers();
		IController GetFirstController();
	}

	public class Controllers : IControllersProvider
	{
		// Get all DS4 controllers
		public IEnumerable<IController> GetAllControllers()
		{
			foreach(var device in Devices.EnumerateDevices().Where(DeviceIdentity.IsDS4))
			{
				yield return new Controller(device);
			}
		}

		// Get first DS4 controller
		public IController GetFirstController()
		{
			// Get first device
			var device = Devices.EnumerateDevices().Where(DeviceIdentity.IsDS4).FirstOrDefault();

			// Return controller
			return new Controller(device);
		}
	}
}
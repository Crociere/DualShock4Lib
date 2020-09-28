using System.Text;
using Microsoft.Win32.SafeHandles;

namespace HidWrapper
{
	public static partial class Devices
	{
		private static string GetManufacturer(SafeFileHandle handle)
		{
			var sb = new StringBuilder(128);
			
			if (NativeMethods.HidD_GetManufacturerString(handle, sb, (ulong)sb.Capacity))
			{
				return sb.ToString();
			}

			return null;
		}
	}
}
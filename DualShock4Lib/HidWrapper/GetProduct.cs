using System.Text;
using Microsoft.Win32.SafeHandles;

namespace HidWrapper
{
	public static partial class Devices
	{
		private static string GetProduct(SafeFileHandle handle)
		{
			var sb = new StringBuilder(128);
			
			if (NativeMethods.HidD_GetProductString(handle, sb, (ulong)sb.Capacity))
			{
				return sb.ToString();
			}

			return null;
		}
	}
}
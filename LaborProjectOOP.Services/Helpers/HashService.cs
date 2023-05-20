using System.Security.Cryptography;
using System.Text;

namespace LaborProjectOOP.Services.Helpers
{
	public class HashService
	{
		public static string GetMD5Hash(string key)
		{
			using(MD5 md5 = MD5.Create())
			{
				byte[]inputBytes = Encoding.UTF8.GetBytes(key);
				byte[]hashBytes = md5.ComputeHash(inputBytes);
				string hashString = BitConverter.ToString(hashBytes).Replace("-",string.Empty);
				return hashString;
			}
		}
	}
}

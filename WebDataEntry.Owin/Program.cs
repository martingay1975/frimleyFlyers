
using System;
using Microsoft.Owin.Hosting;
using WebDataEntry.Owin.StartUp;

namespace WebDataEntry.Owin
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			// Start OWIN host 

			try
			{
				using ((WebApp.Start(@"http://localhost:53002/", FrimleyFlyersStartUp.Configuration)))
				{
					Console.WriteLine("FrimleyFlyers Co Uk-53002");
					Console.WriteLine("Press Enter key to quit.");
					Console.ReadLine();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.GetType().Name} - {e.Message}");
				Console.WriteLine("Try running netstat -ano to find if the port is already in use.");
				Console.ReadLine();
			}
		}
	}
}
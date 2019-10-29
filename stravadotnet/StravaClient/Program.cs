using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strava.Athletes;
using Strava.Authentication;

namespace StravaClient
{
	class Program
	{
		private static StravaRepository stravaRepository;

		static void Main(string[] args)
		{
			var auth = new StaticAuthentication("7cf431232c89760483f6211c4c5142e018988596");
			var client = new Strava.Clients.StravaClient(auth);
			stravaRepository = new StravaRepository(client);
			stravaRepository.GetCompetitorsData("");
		}


		void GetActivities()
		{
			
		}
	}

	public class StravaRepository
	{
		private readonly Strava.Clients.StravaClient client;

		public StravaRepository(Strava.Clients.StravaClient client)
		{
			this.client = client;
		}

		public Task GetCompetitorsData(string id)
		{
			id = "332";
			//var athlete = client.Athletes.GetAthlete(id);
			var fff =  client.Athletes.GetActivities(id);

			Trace.WriteLine(fff[0].Athlete);
			return null;
		}
	}
}

using Galactic.Data.Interfaces;
using Galactic.Data.Models;
using Newtonsoft.Json;

namespace Galactic.Data.Processing
{
    public class FileRouteFetcher : IRouteFetcher
    {
		private string Path = "Routes/galacticRoutes.txt";
        public List<RawRouteData> GetAllRoutes()
        {
			try
			{
				string rawJson = File.ReadAllText(Path);
				RawRouteDataContainer container = JsonConvert.DeserializeObject<RawRouteDataContainer>(rawJson);
				return container.galacticRoutes;
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }

		public RawRouteData? GetRoute(string routeName)
		{
			List<RawRouteData> allRoutes = GetAllRoutes();

			return allRoutes.Find(x => x.Name == routeName);
		}
	}
}

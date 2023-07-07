public interface IRoute
{
	List<(Station from, Station to)> Edges { get; }
	List<Station> Stations { get; }

	List<(Station from, Station to)> GetArrivingRoute();
	List<(Station from, Station to)> GetDepartingRoute();

}
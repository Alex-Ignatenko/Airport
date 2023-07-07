public interface IHubService
{
	Task UpdateState(List<Station> currentUpdate);
	Task UpdateFlights(List<Flight> currentUpdate);
}
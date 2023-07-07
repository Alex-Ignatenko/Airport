using Microsoft.AspNetCore.SignalR;
public class HubService : IHubService
{
	private readonly IHubContext<AirportHub> _hubContext;
	public HubService(IHubContext<AirportHub> hubContext)
	{
		_hubContext = hubContext;
	}

    //Use signalR to update server status aka is sim running or not
    public async Task UpdateState(List<Station> currentUpdate )
	{
		await _hubContext.Clients.All.SendAsync("GetStations", currentUpdate ); 
    }

    //Use signalR to update the status of the stations
    public async Task UpdateFlights(List<Flight> flightsUpdate)
	{
		await _hubContext.Clients.All.SendAsync("GetFlights", flightsUpdate); 
    }
}










using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

public class Station
{

    private SemaphoreSlim _sem;//Controls flight entery so two diffrent flight tasks wont enter the same station at once
    public string StationName { get; set; }
	public Flight? CurrentFlight { get; set; } //The current flight occupying the station if any
	public Station(string stationName)
	{
		StationName = stationName;
		_sem  = new(1);
	}


    //Once a flight got permission and entered the station this func does the following:
    //Print a console msg
    //Send a route status update to the client via signalR hub
    //Use delay to simulate stay at this station
    public async Task EnterStation(IHubService hub, List<Station> stations)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Flight {CurrentFlight!.FlightName} {CurrentFlight!.FlightNumber} Entered station {StationName} at {CurrentFlight!.EnterTime}");
        await hub.UpdateState(stations);
        await Task.Delay(2000);
    }


    //Check if a given station is free for entery
    //When a flight attempts entery only one flight task will be entered to only one station at a given moment
    //Once it passes the semapore the CurrentFlight flag of the given station will be updated with this flight object
    public async Task<Station> CheckEnter(Flight flight, CancellationTokenSource cts)
    {
        CancellationToken  token = cts.Token; //Controls flight entery so that same flight wont enter two diffrent stations at once (i.e the case of station 6 and 7)
        await _sem.WaitAsync(token);
        cts.Cancel();
        if (CurrentFlight == null)
            CurrentFlight = flight;
        return this;
    }

    //On exit this func does the following:
    //Free the CurrentFlight flag of the given station
    //Free sempore of the given station
    //Print a console msg
    //Send a route status update to the client via signalR hub
    public async Task ExitStation(Flight flight,  IHubService hub , List<Station> stations)
	{
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Flight {flight.FlightName} {flight.FlightNumber} existed station {StationName} at {flight.EnterTime}");
		await hub.UpdateState(stations);
        CurrentFlight = null;
		_sem.Release();
	}

}




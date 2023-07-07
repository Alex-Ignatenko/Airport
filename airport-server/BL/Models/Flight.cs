
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

public class Flight 
{
   private CancellationTokenSource _cts = null!; //Controls flight entery so that same flight wont enter two diffrent stations at once (i.e the case of station 6 and 7)
    public int FlightNumber { get; set; }
   public string FlightName { get; private set; }
   public bool IsArriving { get; set; }
   public DateTime EnterTime { get; set; }
   public DateTime ExitTime { get; set; }

    public Flight(string flightName,int flightNumber)
    {
		FlightName = flightName;
        FlightNumber = flightNumber;
        IsArriving = true;
    }

    //Runs a given flight through its whole route in the airport
    //Each flight run is a seperate task in a seperate thread simulates movment of flights independant from each other

    // get the starting station of current flight from the first index to of the arrive/depart route list of edges
    // get the first next  station of current flight from the first index to of the arrive/depart route list of edges

    //Send a signalR update to active flights tbl at client - on flight entery to airport

    //Loop while we have stations to move through, each loop attempt entery to next station if there is one:
        //Exit current station
        //Get next possible movment connections
        //if we got an empty list of connections it means we are right before the exit from airport - this is the last station so we dont have to advance to next
        //break the loop and run the exit from airport code after loop
        //if we got a connection
        //Get the first avilable movment option from the list of possible movment options
        //move to the next station via the right connection we got
        //update current station to next iteration

    //After the flight exits from airport:
        //Prints console msg
        //Removes flight from active flights list
        //Send a signalR update to active flights tbl at client - on flight exit from airport
    public async Task RunFlight(IHubService hub, List<(Station from, Station to)> edges, IRoute route, List<Flight> activeFlights)
    {
        await Task.Run(async() =>
        {
            Station NextStation = edges[0].to; 
            Station? CurrentStation = edges[0].from; 

            await hub.UpdateFlights(activeFlights);
       
            while (CurrentStation != null){
                _cts = new();
                await CurrentStation.ExitStation(this, hub, route.Stations);
                List<Station> tos = GetNextStations(CurrentStation, edges);

                if (tos.Count == 0)
                    break;

                NextStation = await GetFirstAvilable(tos);
                await MoveToNextStation(NextStation, hub, route.Stations);
                CurrentStation = NextStation;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Flight {FlightNumber} {FlightName} left the terminal at {ExitTime}");
            activeFlights.Remove(this);
            await hub.UpdateFlights(activeFlights); 
        });
    }

    //Depends on the current from station in the run loop will return all the possible connections to move to 
    private List<Station> GetNextStations(Station from, List<(Station from, Station to)> edges)
    {
        var listOfTo = new List<Station>();
        foreach (var edge in edges)
        {
            if (from == edge.from)
                listOfTo.Add(edge.to);
        }
        return listOfTo;
    }

    //From all possible connection from current station to possible next stations returns the one to station that is empty and free to move to
    //uses linq to select the first free next station 
    //returns the right next station to loop
    private async Task<Station> GetFirstAvilable(List<Station> stations)
    {
        var enterStationTasks = stations
           .Select(async s => await s.CheckEnter(this, _cts)).ToList();

        var enteredStation = await Task.WhenAny(enterStationTasks);
        return await enteredStation;
    }

    //Preforms the enter station func on the chosen next station
    private async Task MoveToNextStation(Station NextStation, IHubService hub, List<Station> stationList)
    {
        await NextStation.EnterStation( hub, stationList);
    }
}




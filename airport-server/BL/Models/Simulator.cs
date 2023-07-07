using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using Microsoft.AspNetCore.SignalR;
using BL.Interfaces;

namespace BL.Models
{
    namespace Airportproject1.Services
    {
        public class Simulator : ISimulator
		{
			private readonly System.Timers.Timer _timerArriving;//Timer for arriving flights generation
            private readonly System.Timers.Timer _timerDeparturing;//Timer for departing flights generation
            private readonly IHubService _hub;//Hub for SignalR 
            private readonly FlightGenerator _flightGenerator;//Random flight generator
            private readonly IRoute _route;// Airport route
            private readonly List<Flight> _activeFlights;//List of all current active flights in the airport


            IFlightRepository<Flight> _repo;//Db repository
            public bool isStarted { get; set; } //Simulator activity status flag

            public Simulator(IHubService hub , IRoute route , IFlightRepository<Flight> repo)
			{
				_flightGenerator = new FlightGenerator();
				_hub = hub;
				_timerArriving = new System.Timers.Timer(5000);
				_timerDeparturing = new System.Timers.Timer(5000);
				_route = route;
				_activeFlights = new List<Flight>();
				_repo = repo;
			}

            //Activiate timers that call back flight creation functions and set simulator run status to on
            public void Start()
			{
				_timerArriving.Elapsed += CreateArrivngFlight;
				_timerArriving.AutoReset = true;
				_timerArriving.Enabled = true;

				_timerDeparturing.Elapsed += CreateDepartingFlight;
				_timerDeparturing.AutoReset = true;
				_timerDeparturing.Enabled = true;

                isStarted = true;

            }

            //Request random flight generation, set its type to arriving and set its creation time
            //In a task run the flight through the airport via a function of route class
            //Each flight runs in a seperate thread via its own task to simulate independance from other flights
            //The route run func also recieves the flights needed route which is an arriving flight route
            //This is the order of stations this flight needs to traverse
            //Addes the new flight to active flight list 
            //Print a msg to console
            //Update exit time of flight and update db with this flight at the end of its run
            public void CreateArrivngFlight(object? sender, ElapsedEventArgs e)
			{          
                var f = _flightGenerator.GenerateRandomFLight();
				f.IsArriving = true;
				f.EnterTime = DateTime.Now;

                Task.Run(async () =>
				{
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Created Arriving Flight {f.FlightNumber} {f.FlightName} at {f.EnterTime} ");
                    var arrRoute = _route.GetArrivingRoute();
				    _activeFlights.Add(f);
					await f.RunFlight(_hub, arrRoute, _route , _activeFlights);
				});
              
                f.ExitTime = DateTime.Now;
                _repo.LogFlightToDb(f);
            }

            //Operates the same as CreateArrivngFlight only for departing flights
            public void CreateDepartingFlight(object? sender, ElapsedEventArgs e)
			{
				var f = _flightGenerator.GenerateRandomFLight();
				f.IsArriving = false;
				f.EnterTime = DateTime.Now;
				Task.Run(async () =>
				{
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Created Departing Flight {f.FlightNumber} {f.FlightName} at {f.EnterTime} ");
                    var dep = _route.GetDepartingRoute();
				    _activeFlights.Add(f);
                    await f.RunFlight(_hub, dep , _route , _activeFlights);
				});
                f.ExitTime = DateTime.Now;
				_repo.LogFlightToDb(f);
			}
		}
	}
}
using Accessories.Interfaces;
using Accessories.Mappers;
using BL.Interfaces;
using DAL;
using DAL.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
	public class FlightRepository : IFlightRepository<Flight>
	{
	    private readonly IMapper<Flight,FlightDto> _mapper;
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public FlightRepository(IServiceScopeFactory serviceScopeFactory)
		{
			_mapper = new FlightMapper();

            //context is scope based while our sim flight and route are all singleton
            //it is needed to properly sync up the life time of the scoped repo inside the singleton life cycle

            _serviceScopeFactory = serviceScopeFactory;
		}

        //Uses the mapper to map the flight model to the flight dto
        //Addes a flight to the flights history tbl in db
        public void LogFlightToDb(Flight flight)
		{
			using (var scope = _serviceScopeFactory.CreateScope())
			{
				var DbContextScopedService = scope.ServiceProvider.GetService<AirportDbContext>();
				var dto = _mapper.Map(flight);
                if (DbContextScopedService != null)
				{
					DbContextScopedService.FlightDtos.Add(dto); 
                    DbContextScopedService.SaveChanges();
					Console.WriteLine($"Flight: {flight.FlightNumber} {flight.FlightName} info saved to DB");
				}
			}
		}
	}
}

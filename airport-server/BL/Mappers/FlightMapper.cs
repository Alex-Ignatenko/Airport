using Accessories.Interfaces;
using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accessories.Mappers
{
	public class FlightMapper : IMapper<Flight,FlightDto>
	{
        //Mapper between the flight class to it coresponding dto that is saved as data in the flight history tbl in db
        public FlightDto Map(Flight type)
		{
			var newDto = new FlightDto();
			newDto.FlightNumber = type.FlightNumber;
			newDto.FlightName = type.FlightName;
			newDto.EnterTime = type.EnterTime;
			newDto.ExitTime = type.ExitTime;
			
			//Translate the arrive/depart flag from bool to string
			if (type.IsArriving)
				newDto.FlightType = "Arrving Flight";
			else
				newDto.FlightType = "Departing Flight";

			return newDto;
		}
	}
}

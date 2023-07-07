using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
	//Saves the data of each flight that passes through the airport to a DB 
	//DB builds a history log of all the flights that passed through the airport
	public class FlightDto
	{	
		public int Id { get; set; }
		public int FlightNumber { get; set; }
		public string? FlightName { get;  set; }
		public string? FlightType { get; set; }
		public DateTime EnterTime { get; set; }
		public DateTime ExitTime { get; set; }
	}
}

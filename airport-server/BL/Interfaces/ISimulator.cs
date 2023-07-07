using System.Timers;

namespace BL.Interfaces
{
    public interface ISimulator
    {
        public bool isStarted { get;  set; }

        void CreateArrivngFlight(object? sender, ElapsedEventArgs e);
        void CreateDepartingFlight(object? sender, ElapsedEventArgs e);
		void Start();
    }
}
namespace BL.Interfaces
{
	public interface IFlightRepository<T>
	{
		void LogFlightToDb(T obj);
	}
}

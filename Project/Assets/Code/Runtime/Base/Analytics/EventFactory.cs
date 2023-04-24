using stroibot.Factory;

namespace stroibot.Analytics
{
	public class EventFactory :
		IFactory<string, string, Event>
	{
		public Event Create(
			string type,
			string data)
		{
			return new Event(type, data);
		}
	}
}

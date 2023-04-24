using System;

namespace stroibot.Analytics
{
	[Serializable]
	public class Event
	{
		public string Type;
		public string Data;

		public Event(
			string type,
			string data)
		{
			Type = type;
			Data = data;
		}
	}
}

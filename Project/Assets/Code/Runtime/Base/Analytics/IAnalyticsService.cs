namespace stroibot.Base.Analytics
{
	public interface IAnalyticsService
	{
		public void TrackEvent(string type, string data);
	}
}

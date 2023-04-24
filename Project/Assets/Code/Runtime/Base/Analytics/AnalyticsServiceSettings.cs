using System;

namespace stroibot.Analytics
{
	[Serializable]
	public class AnalyticsServiceSettings
	{
		public string ServerURL;
		public string RequestMethod;
		public string RequestContentType;
		public float CooldownBeforeSend;
		public int BatchSize;
	}
}

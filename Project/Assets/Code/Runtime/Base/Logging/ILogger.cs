namespace stroibot.Logging
{
	public interface ILogger
	{
		public void Log(string tag, object message);
		public void LogWarning(string tag, object message);
		public void LogError(string tag, object message);
	}
}

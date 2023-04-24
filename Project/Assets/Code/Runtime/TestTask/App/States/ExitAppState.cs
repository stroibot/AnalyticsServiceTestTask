namespace stroibot.TestTask
{
	public class ExitAppState :
		AppState
	{
		public ExitAppState(
			App app) :
			base(app)
		{ }

		public override void OnEnter()
		{
			App.AnalyticsService.TrackEvent("app_exit", string.Empty);
			App.AnalyticsService.SendPendingEvents();
#if UNITY_EDITOR
			App.Logger.Log(nameof(ExitAppState),"Application Quit");
#endif
		}
	}
}

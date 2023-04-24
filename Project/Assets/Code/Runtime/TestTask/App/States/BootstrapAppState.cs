namespace stroibot.TestTask
{
	public class BootstrapAppState :
		AppState
	{
		public BootstrapAppState(
			App app) :
			base(app)
		{ }

		public override void OnEnter()
		{
			App.AnalyticsService.LoadAndSendPendingEvents();
			App.AnalyticsService.TrackEvent("app_launch", string.Empty);
			App.StateMachine.Enter(AppStateTag.LoadMainMenu);
		}
	}
}

namespace stroibot.TestTask.App.States
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
			App.AnalyticsService.TrackEvent("app_launch", string.Empty);
			App.StateMachine.Enter(AppStateTag.LoadMainMenu);
		}
	}
}

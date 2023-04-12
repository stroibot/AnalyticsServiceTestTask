namespace stroibot.TestTask.App.States
{
	public class MainMenuAppState :
		AppState
	{
		public MainMenuAppState(
			App app) :
			base(app)
		{ }

		public override void OnEnter()
		{
			App.AnalyticsService.TrackEvent("app_main_menu", string.Empty);
		}
	}
}

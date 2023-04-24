namespace stroibot.TestTask
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

namespace stroibot.TestTask
{
	public class LoadMainMenuAppState :
		AppState
	{
		public LoadMainMenuAppState(
			App app) :
			base(app)
		{ }

		public override void OnEnter()
		{
			App.AnalyticsService.TrackEvent("app_load_main_menu", string.Empty);
			App.LoadingScreen.Toggle(true);
			App.SceneService.LoadMainMenu(OnMainMenuSceneLoaded);
		}

		public override void OnExit()
		{
			App.LoadingScreen.Toggle(false);
		}

		private void OnMainMenuSceneLoaded()
		{
			App.StateMachine.Enter(AppStateTag.MainMenu);
		}
	}
}

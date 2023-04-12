using UnityEngine;

namespace stroibot.TestTask.App.States
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
#if UNITY_EDITOR
			Application.Quit();
			App.Logger.Log("Application Quit");
#endif
		}
	}
}

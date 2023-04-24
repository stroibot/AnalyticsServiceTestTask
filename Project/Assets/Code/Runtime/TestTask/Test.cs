using UnityEngine;
using Zenject;

namespace stroibot.TestTask
{
	public class Test :
		MonoBehaviour
	{
		private App _app;

		[Inject]
		public void Construct(
			App app)
		{
			_app = app;
		}

		private void OnGUI()
		{
			if (GUI.Button(new Rect(50, 25, 250, 75), "Trigger Event"))
			{
				_app.AnalyticsService.TrackEvent("test_event", "test_data");
			}
		}
	}
}

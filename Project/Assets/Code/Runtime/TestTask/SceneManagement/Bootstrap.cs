using stroibot.TestTask.App.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace stroibot.TestTask.SceneManagement
{
	public class Bootstrap :
		MonoBehaviour
	{
		private App.App _app;

		[Inject]
		public void Construct(
			App.App app)
		{
			_app = app;
		}

		private void Start()
		{
			_app.StateMachine.Enter(AppStateTag.Bootstrap);
		}

		private void Update()
		{
			var currentScene = SceneManager.GetActiveScene();

			if (currentScene.buildIndex != 0)
			{
				SceneManager.UnloadSceneAsync(0);
			}
		}
	}
}

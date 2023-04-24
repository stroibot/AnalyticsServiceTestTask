using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace stroibot.TestTask
{
	public class Bootstrap :
		MonoBehaviour
	{
		private App _app;
		private AppStateFactory _appStateFactory;

		[Inject]
		public void Construct(
			App app,
			AppStateFactory appStateFactory)
		{
			_app = app;
			_appStateFactory = appStateFactory;
		}

		private void Awake()
		{
			Application.quitting += () => _app.StateMachine.Enter(AppStateTag.Exit);
			_app.StateMachine.Add(AppStateTag.Bootstrap, _appStateFactory.Create(AppStateTag.Bootstrap));
			_app.StateMachine.Add(AppStateTag.MainMenu, _appStateFactory.Create(AppStateTag.MainMenu));
			_app.StateMachine.Add(AppStateTag.LoadMainMenu, _appStateFactory.Create(AppStateTag.LoadMainMenu));
			_app.StateMachine.Add(AppStateTag.Exit, _appStateFactory.Create(AppStateTag.Exit));
		}

		private void Start()
		{
			_app.StateMachine.Enter(AppStateTag.Bootstrap);
		}

		private void Update()
		{
			// Unload boot scene
			var currentScene = SceneManager.GetActiveScene();

			if (currentScene.buildIndex != 0)
			{
				SceneManager.UnloadSceneAsync(0);
			}
		}
	}
}

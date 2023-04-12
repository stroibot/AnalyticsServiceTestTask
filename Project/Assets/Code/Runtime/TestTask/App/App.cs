using stroibot.Base.Analytics;
using stroibot.Base.SceneManagement;
using stroibot.Base.StateMachine;
using stroibot.TestTask.App.States;
using stroibot.TestTask.SceneManagement;
using UnityEngine;
using Zenject;

namespace stroibot.TestTask.App
{
	public class App :
		IInitializable
	{
		public StateMachine<AppStateTag> StateMachine { get; }
		public ILoadingScreen LoadingScreen { get; }
		public SceneService SceneService { get; }
		public ILogger Logger { get; }
		public IAnalyticsService AnalyticsService { get; }

		private readonly AppState.Factory _appStateFactory;

		public App(
			StateMachine<AppStateTag> stateMachine,
			ILoadingScreen loadingScreen,
			SceneService sceneService,
			ILogger logger,
			IAnalyticsService analyticsService,
			AppState.Factory appStateFactory)
		{
			StateMachine = stateMachine;
			LoadingScreen = loadingScreen;
			SceneService = sceneService;
			Logger = logger;
			AnalyticsService = analyticsService;
			_appStateFactory = appStateFactory;
		}

		public void Initialize()
		{
			StateMachine.Add(AppStateTag.Bootstrap, _appStateFactory.Create(AppStateTag.Bootstrap));
			StateMachine.Add(AppStateTag.MainMenu, _appStateFactory.Create(AppStateTag.MainMenu));
			StateMachine.Add(AppStateTag.LoadMainMenu, _appStateFactory.Create(AppStateTag.LoadMainMenu));
			StateMachine.Add(AppStateTag.Exit, _appStateFactory.Create(AppStateTag.Exit));
		}
	}
}

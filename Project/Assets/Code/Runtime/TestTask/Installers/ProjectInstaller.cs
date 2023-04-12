using stroibot.Base.Analytics;
using stroibot.Base.Base.WebRequest;
using stroibot.Base.Coroutine;
using stroibot.Base.FileSystem;
using stroibot.Base.Saving;
using stroibot.Base.SceneManagement;
using stroibot.Base.Serializer;
using stroibot.Base.StateMachine;
using stroibot.Base.WebRequest;
using stroibot.TestTask.App.States;
using stroibot.TestTask.SceneManagement;
using System;
using UnityEngine;
using Zenject;
using Event = stroibot.Base.Analytics.Event;

namespace stroibot.TestTask
{
	public class ProjectInstaller :
		MonoInstaller
	{
		[Serializable]
		public class Settings
		{
			[Header("Event System")]
			public GameObject EventSystem;

			[Header("Scene Management")]
			public GameObject LoadingScreen;

			[Header("Misc")]
			public GameObject CoroutineRunner;
		}

		[Inject] private Settings _settings;

		public override void InstallBindings()
		{
			InstallLogging();
			InstallFileSystem();
			InstallSerializer();
			InstallEventSystem();
			InstallUnityWebRequest();
			InstallSaving();
			InstallAnalytics();
			InstallSceneManagement();
			InstallApp();
			InstallMisc();
		}

		private void InstallLogging()
		{
			var logger = new Logger(Debug.unityLogger.logHandler);

			Container
				.Bind<ILogger>()
				.To<Logger>()
				.FromInstance(logger);
		}

		private void InstallFileSystem()
		{
			Container
				.Bind<IFileSystem>()
				.To<FileSystem>()
				.AsSingle();
		}

		private void InstallSerializer()
		{
			Container
				.Bind<ISerializer>()
				.To<JSONSerializer>()
				.AsSingle();
		}

		private void InstallEventSystem()
		{
			Container
				.InstantiatePrefab(_settings.EventSystem);
		}

		private void InstallUnityWebRequest()
		{
			Container
				.Bind<IUnityWebRequest>()
				.To<MockUnityWebRequestWrapper>()
				.AsTransient();

			Container
				.BindFactory<string, string, IUnityWebRequest, UnityWebRequestFactory>()
				.To<MockUnityWebRequestWrapper>();
		}

		private void InstallSaving()
		{
			Container
				.BindFactory<ISaveData, SaveService, SaveService.Factory>();
		}

		private void InstallAnalytics()
		{
			Container
				.BindFactory<string, string, Event, Event.Factory>()
				.FromPoolableMemoryPool(pool => pool.WithInitialSize(8));

			Container
				.BindInterfacesAndSelfTo<AnalyticsService>()
				.AsSingle();
		}

		private void InstallSceneManagement()
		{
			Container
				.Bind<ILoadingScreen>()
				.To<LoadingScreen>()
				.FromComponentInNewPrefab(_settings.LoadingScreen)
				.AsSingle();

			Container
				.Bind<SceneService>()
				.AsSingle();
		}

		private void InstallApp()
		{
			Container
				.Bind<StateMachine<AppStateTag>>()
				.AsSingle();

			Container
				.BindFactory<AppStateTag, AppState, AppState.Factory>()
				.FromFactory<AppStateFactory>();

			Container
				.BindInterfacesAndSelfTo<App.App>()
				.AsSingle();
		}

		private void InstallMisc()
		{
			Container
				.Bind<ICoroutineRunner>()
				.FromComponentInNewPrefab(_settings.CoroutineRunner)
				.AsSingle();
		}
	}
}

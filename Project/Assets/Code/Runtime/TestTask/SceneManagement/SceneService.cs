using stroibot.Base.SceneManagement;
using System;

namespace stroibot.TestTask.SceneManagement
{
	public class SceneService
	{
		[Serializable]
		public class Settings
		{
			public GameScene MainMenu;
		}

		private readonly Settings _settings;

		private bool _isLoading;

		public SceneService(
			Settings settings)
		{
			_settings = settings;
		}

		public void LoadMainMenu(Action callback)
		{
			if (_isLoading)
			{
				return;
			}

			var mainMenu = _settings.MainMenu;
			mainMenu.SceneReference.LoadSceneAsync().CompletedTypeless += (_) =>
			{
				_isLoading = false;
				callback?.Invoke();
			};
		}
	}
}

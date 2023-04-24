using System;

namespace stroibot.TestTask
{
	public class SceneService
	{
		private readonly SceneServiceSettings _settings;

		private bool _isLoading;

		public SceneService(
			SceneServiceSettings settings)
		{
			_settings = settings;
		}

		public void LoadMainMenu(
			Action callback)
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

using stroibot.SceneManagement;
using UnityEngine;

namespace stroibot.TestTask
{
	public class LoadingScreen :
		MonoBehaviour,
		ILoadingScreen
	{
		[SerializeField] private GameObject _loadingInterface;

		public void Toggle(
			bool active)
		{
			_loadingInterface.SetActive(active);
		}
	}
}

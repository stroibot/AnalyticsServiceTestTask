using UnityEngine;
using UnityEngine.AddressableAssets;

namespace stroibot.Base.SceneManagement
{
	[CreateAssetMenu(fileName = "GameScene", menuName = "Data/Game Scene/Game Scene")]
	public class GameScene :
		ScriptableObject,
		IGameScene
	{
		[SerializeField] private AssetReference _sceneReference;

		public string Name => name;
		public AssetReference SceneReference => _sceneReference;
	}
}

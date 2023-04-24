using UnityEngine.AddressableAssets;

namespace stroibot.SceneManagement
{
	public interface IGameScene
	{
		public string Name { get; }
		public AssetReference SceneReference { get; }
	}
}

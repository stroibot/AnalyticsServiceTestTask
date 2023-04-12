using UnityEngine.AddressableAssets;

namespace stroibot.Base.SceneManagement
{
	public interface IGameScene
	{
		public string Name { get; }
		public AssetReference SceneReference { get; }
	}
}

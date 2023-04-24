using stroibot.Analytics;
using UnityEngine;
using Zenject;

namespace stroibot.TestTask
{
	[CreateAssetMenu(fileName = "ProjectSettings", menuName = "Settings/Project Settings")]
	public class ProjectSettingsInstaller :
		ScriptableObjectInstaller<ProjectSettingsInstaller>
	{
		public ProjectSettings ProjectInstallerSettings;
		public AnalyticsServiceSettings AnalyticsServiceSettings;
		public SceneServiceSettings SceneServiceSettings;

		public override void InstallBindings()
		{
			Container
				.BindInstance(ProjectInstallerSettings)
				.IfNotBound();
			Container
				.BindInstance(AnalyticsServiceSettings)
				.IfNotBound();
			Container
				.BindInstance(SceneServiceSettings)
				.IfNotBound();
		}
	}
}

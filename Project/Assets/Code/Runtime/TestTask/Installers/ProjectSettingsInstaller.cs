using stroibot.Base.Analytics;
using stroibot.TestTask.SceneManagement;
using UnityEngine;
using Zenject;

namespace stroibot.TestTask
{
	[CreateAssetMenu(fileName = "ProjectSettings", menuName = "Settings/Project Settings")]
	public class ProjectSettingsInstaller :
		ScriptableObjectInstaller<ProjectSettingsInstaller>
	{
		public ProjectInstaller.Settings ProjectInstallerSettings;
		public AnalyticsService.Settings AnalyticsServiceSettings;
		public SceneService.Settings SceneServiceSettings;

		public override void InstallBindings()
		{
			Container.BindInstance(ProjectInstallerSettings).IfNotBound();
			Container.BindInstance(AnalyticsServiceSettings).IfNotBound();
			Container.BindInstance(SceneServiceSettings).IfNotBound();
		}
	}
}

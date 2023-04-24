using System;
using UnityEngine;

namespace stroibot.TestTask
{
	[Serializable]
	public class ProjectSettings
	{
		[Header("Event System")]
		public GameObject EventSystem;

		[Header("Scene Management")]
		public GameObject LoadingScreen;

		[Header("Misc")]
		public GameObject CoroutineRunner;
	}
}

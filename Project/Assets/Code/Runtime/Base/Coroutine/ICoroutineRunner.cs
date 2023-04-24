using System.Collections;

namespace stroibot.Coroutine
{
	public interface ICoroutineRunner
	{
		public UnityEngine.Coroutine StartCoroutine(IEnumerator coroutine);
		public void StopCoroutine(UnityEngine.Coroutine coroutine);
	}
}

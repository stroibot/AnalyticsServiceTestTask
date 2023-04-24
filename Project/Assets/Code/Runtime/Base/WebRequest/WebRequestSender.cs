using stroibot.Coroutine;
using System;
using System.Collections;
using UnityEngine.Networking;

namespace stroibot.WebRequest
{
	public class WebRequestSender
	{
		public bool IsReady => !_isSending && (DateTime.Now - _lastSentTime).TotalSeconds >= CooldownBeforeSend;
		public float CooldownBeforeSend { get; set; }

		private readonly ICoroutineRunner _coroutineRunner;

		private bool _isSending;
		private DateTime _lastSentTime = DateTime.MinValue;

		public WebRequestSender(
			ICoroutineRunner coroutineRunner)
		{
			_coroutineRunner = coroutineRunner;
		}

		public void Send(
			IUnityWebRequest request,
			Action onSuccess,
			Action onFail)
		{
			_coroutineRunner.StartCoroutine(SendAsync(request, onSuccess, onFail));
		}

		private IEnumerator SendAsync(
			IUnityWebRequest request,
			Action onSuccess,
			Action onFail)
		{
			_isSending = true;

			yield return request.SendWebRequest();

			if (request.Result == UnityWebRequest.Result.Success &&
				request.ResponseCode == 200)
			{
				_lastSentTime = DateTime.Now;
				onSuccess.Invoke();
			}
			else
			{
				onFail.Invoke();
			}

			_isSending = false;
		}
	}
}

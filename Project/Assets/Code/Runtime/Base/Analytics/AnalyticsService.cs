using stroibot.Base.Coroutine;
using stroibot.Base.Saving;
using stroibot.Base.Serializer;
using stroibot.Base.WebRequest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace stroibot.Base.Analytics
{
	public class AnalyticsService :
		IAnalyticsService,
		IInitializable,
		ITickable,
		IDisposable
	{
		[Serializable]
		public class Settings
		{
			public string ServerURL;
			public string RequestMethod;
			public string RequestContentType;
			public float CooldownBeforeSend;
			public int BatchSize;
		}

		[Serializable]
		private class EventsData : ISaveData
		{
			public List<Event> events;
		}

		private const string AnalyticsEventsSaveFilename = "analytics_events.json";

		private readonly Settings _settings;
		private readonly ILogger _logger;
		private readonly ISerializer _serializer;
		private readonly ICoroutineRunner _coroutineRunner;
		private readonly UnityWebRequestFactory _webRequestFactory;
		private readonly Event.Factory _eventFactory;
		private readonly SaveService _saveService;
		private readonly List<Event> _events;

		private DateTime _lastSentTime = DateTime.MinValue;
		private bool _isSending;

		public AnalyticsService(
			Settings settings,
			ILogger logger,
			ISerializer serializer,
			ICoroutineRunner coroutineRunner,
			UnityWebRequestFactory webRequestFactory,
			Event.Factory eventFactory,
			SaveService.Factory saveServiceFactory)
		{
			_settings = settings;
			_logger = logger;
			_serializer = serializer;
			_coroutineRunner = coroutineRunner;
			_webRequestFactory = webRequestFactory;
			_eventFactory = eventFactory;
			_saveService = saveServiceFactory.Create(new EventsData());
			_events = new List<Event>(_settings.BatchSize);
		}

		public void TrackEvent(string type, string data)
		{
			var @event = _eventFactory.Create(type, data);
			_events.Add(@event);
			CheckAndSendEvents();
		}

		public void Initialize()
		{
			LoadEventsFromFile();
			_coroutineRunner.StartCoroutine(SendEvents());
		}

		public void Tick()
		{
			CheckAndSendEvents();
		}

		public void Dispose()
		{
			_coroutineRunner.StartCoroutine(SendEvents());
		}

		private void CheckAndSendEvents()
		{
			if (!_isSending && _events.Count >= _settings.BatchSize && (DateTime.Now - _lastSentTime).TotalSeconds >= _settings.CooldownBeforeSend)
			{
				_coroutineRunner.StartCoroutine(SendEvents());
			}
		}

		private IEnumerator SendEvents()
		{
			if (_events.Count == 0)
			{
				yield break;
			}

			_isSending = true;
			var eventsToSend = new List<Event>(_events);
			_events.Clear();
			var json = _serializer.Serialize(new EventsData { events = eventsToSend });
			var request = _webRequestFactory.Create(_settings.ServerURL, _settings.RequestMethod);
			byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
			request.UploadHandler = new UploadHandlerRaw(bodyRaw);
			request.DownloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", _settings.RequestContentType);

			yield return request.SendWebRequest();

			if (request.Result == UnityWebRequest.Result.Success && request.ResponseCode == 200)
			{
				_logger.Log(nameof(AnalyticsService), "Events were sent successfully");
				_lastSentTime = DateTime.Now;
				DeleteEventsFile();
			}
			else
			{
				_logger.LogWarning(nameof(AnalyticsService), "Failed to send events");
				_events.AddRange(eventsToSend);
				SaveEventsToFile();
			}

			_isSending = false;
			request.Dispose();
		}

		private void SaveEventsToFile()
		{
			var saveData = _saveService.SaveData as EventsData;
			saveData.events = _events;
			_saveService.Save(AnalyticsEventsSaveFilename);
		}

		private void LoadEventsFromFile()
		{
			if (_saveService.Load(AnalyticsEventsSaveFilename))
			{
				var saveData = _saveService.SaveData as EventsData;
				var events = saveData.events;
				_events.AddRange(events);
			}
		}

		private void DeleteEventsFile()
		{
			_saveService.Delete(AnalyticsEventsSaveFilename);
		}
	}
}

using stroibot.Logging;
using stroibot.Saving;
using stroibot.WebRequest;
using System.Collections.Generic;

namespace stroibot.Analytics
{
	public class AnalyticsService
	{
		private static readonly string LogTag = $"{nameof(AnalyticsService)}";
		private const string AnalyticsEventsSaveFilename = "analytics_events.json";

		private readonly AnalyticsServiceSettings _settings;
		private readonly ILogger _logger;
		private readonly EventFactory _eventFactory;
		private readonly SaveService _saveService;
		private readonly WebRequestConfigurator _webRequestConfigurator;
		private readonly WebRequestSender _webRequestSender;

		private EventsData _eventsData;

		public AnalyticsService(
			AnalyticsServiceSettings settings,
			ILogger logger,
			EventFactory eventFactory,
			SaveService saveService,
			WebRequestConfigurator webRequestConfigurator,
			WebRequestSender webRequestSender)
		{
			_settings = settings;
			_logger = logger;
			_eventFactory = eventFactory;
			_saveService = saveService;
			_webRequestConfigurator = webRequestConfigurator;
			_webRequestSender = webRequestSender;
			_webRequestSender.CooldownBeforeSend = _settings.CooldownBeforeSend;
			_eventsData = new EventsData()
			{
				Events = new List<Event>(_settings.BatchSize)
			};
		}

		public void TrackEvent(
			string type,
			string data)
		{
			var @event = _eventFactory.Create(type, data);
			_eventsData.Events.Add(@event);
			_saveService.Save(_eventsData, AnalyticsEventsSaveFilename);
			CheckAndSendEvents();
		}

		public void LoadAndSendPendingEvents()
		{
			if (_saveService.Load(ref _eventsData, AnalyticsEventsSaveFilename))
			{
				SendPendingEvents();
			}
		}

		public void Tick()
		{
			CheckAndSendEvents();
		}

		public void SendPendingEvents()
		{
			if (_eventsData.Events.Count == 0)
			{
				return;
			}

			if (!_webRequestSender.IsReady)
			{
				_saveService.Save(_eventsData, AnalyticsEventsSaveFilename);
				return;
			}

			_webRequestConfigurator.Configure(
				_settings.ServerURL,
				_settings.RequestMethod,
				_settings.RequestContentType,
				_eventsData);
			_webRequestSender.Send(
				_webRequestConfigurator.Request,
				() =>
				{
					_eventsData.Events.Clear();
					_logger.Log(LogTag, "Events were sent successfully");
					_saveService.Delete(AnalyticsEventsSaveFilename);
				},
				() =>
				{
					_logger.LogWarning(LogTag, "Failed to send events");
					_saveService.Save(_eventsData, AnalyticsEventsSaveFilename);
				});
		}

		private void CheckAndSendEvents()
		{
			if (_eventsData.Events.Count >= _settings.BatchSize &&
				_webRequestSender.IsReady)
			{
				SendPendingEvents();
			}
		}
	}
}

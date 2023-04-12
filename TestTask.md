
C# Unity Developer Test Task
---

Develop **small** service, which will accept and send events to the analytics service.\
Exmaple of events: level_start, reward_granted, coin_spent.\
Project platforms: Android, WebGL.

The estimated time to complete the task is 2-3 hours (the time is not fixed, it is quite possible to spend more if there is a desire).

Event - is an object that includes fields:

  

*  `type` - event type, string

*  `data` - event data, string

  
The server accepts multiple events in a single `POST` request in `JSON` format.\
Do not create the server itself. It is assumed that another person in the team is doing it, and they are not ready yet.\
The format of the request to the server is described in the example below:
```json
    {
        "events": [
            {
                "type": "level_start",
                "data": "level:3"
            },
            ...
        ]
    }
```

The URL to send is set by the external parameter of the service `serverUrl`.

The service accepts events via the `TrackEvent` method with the arguments `string type` and `string data`. The received event is not sent immediately, but after a certain time (set by the service parameter `cooldownBeforeSend`). This cooldown is needed to accumulate and send several events simultaneously.

For example, if you call the `TrackEvent` method several times within a short period of time (less than `cooldownBeforeSend`), the service will send all these events at once in one message.

It is also important to ensure guaranteed delivery of events to the server. Events are considered delivered only if, in response to the message, the server returned [200 OK](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/200).

The analytics server may not always be available (for example, there is no network on the phone), so successful sending may occur after an indefinite time. If the application has terminated (or crashed by mistake), then undelivered events should be sent the next time the application is launched (we assume that the service starts with the application). Thus, events should not be lost.

It is enough to place the service itself in the MonoBehaviour heir class, for example:
```c#
    public class EventService : MonoBehaviour {
        public void TrackEvent(string type, string data) { }
    }
```

For the project we use Unity 2021 LTS.\
You can use additional libraries.

Upload the result to the public project on [github.com](https://github.com/)

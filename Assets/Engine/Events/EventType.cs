namespace enjoythevibes.EventsManagerCore
{
    public struct EventTypeKeys<T> where T : IEventType
    {
        public static int EventTypeKey { get; }

        static EventTypeKeys()
        {
            EventTypeKey = EventTypeKeysCounter.lastEventType++;
        }
    }

    public static class EventTypeKeysCounter
    {
        public static int lastEventType = 1;
    }
}
using System;
using System.Collections.Generic;

public static class EventBusSystem
{
    private static Dictionary<Type, Delegate> events = new Dictionary<Type, Delegate>();

    public static void Subscribe<EventType>(Action<EventType> listener)
    {
        if (events.TryGetValue(typeof(EventType), out Delegate existing))
        {
            events[typeof(EventType)] = Delegate.Combine(existing, listener);
        }
        else
        {
            events[typeof(EventType)] = listener;
        }
    }

    public static void Unsubscribe<EventType>(Action<EventType> listener)
    {
        if (events.TryGetValue(typeof(EventType), out Delegate existing))
        {
            var newDelegate = Delegate.Remove(existing, listener);
            if (newDelegate == null)
                events.Remove(typeof(EventType));
            else
                events[typeof(EventType)] = newDelegate;
        }
    }

    public static void Publish<EventType>(EventType eventData)
    {
        if (events.TryGetValue(typeof(EventType), out Delegate existing))
        {
            ((Action<EventType>)existing)?.Invoke(eventData);
        }
        else
        {
            UnityEngine.Debug.LogWarning("No subscribers for event type: " + typeof(EventType).ToString());
        }
    }
}



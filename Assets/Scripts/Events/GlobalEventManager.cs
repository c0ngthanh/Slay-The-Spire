using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple, strongly-typed event bus.
/// </summary>
public class GlobalEventManager
{
    private static GlobalEventManager _instance;
    public static GlobalEventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GlobalEventManager();
            }
            return _instance;
        }
    }

    private Dictionary<Type, Delegate> triggers = new Dictionary<Type, Delegate>();

    public void AddListener<T>(Action<T> listener) where T : GameEvent
    {
        if (triggers.TryGetValue(typeof(T), out Delegate existingDelegate))
        {
            triggers[typeof(T)] = Delegate.Combine(existingDelegate, listener);
        }
        else
        {
            triggers[typeof(T)] = listener;
        }
    }

    public void RemoveListener<T>(Action<T> listener) where T : GameEvent
    {
        if (triggers.TryGetValue(typeof(T), out Delegate existingDelegate))
        {
            Delegate currentDelegate = Delegate.Remove(existingDelegate, listener);

            if (currentDelegate == null)
            {
                triggers.Remove(typeof(T));
            }
            else
            {
                triggers[typeof(T)] = currentDelegate;
            }
        }
    }

    public void Invoke<T>(T gameEvent) where T : GameEvent
    {
        if (triggers.TryGetValue(typeof(T), out Delegate existingDelegate))
        {
            var callback = existingDelegate as Action<T>;
            callback?.Invoke(gameEvent);
        }
    }
}
// Force Unity Recompile

using System;
using System.Collections.Generic;

public static class EventManager
{
    private static Dictionary<Enum, Action<object>> _gameEventDict = new Dictionary<Enum, Action<object>>();

    // Create an event and add to the dictionary
    public static void Initialise(Enum gameEventName)
    {
        if (!_gameEventDict.ContainsKey(gameEventName))
        {
            Action<object> newGameEvent = null;
            _gameEventDict.Add(gameEventName, newGameEvent);
        }
    }

    // Subscribe function handler to event
    public static void Subscribe(Enum gameEventName, Action<object> funcToSub)
    {
        // Check if event exists, then sub handler function to it
        if (!_gameEventDict.ContainsKey(gameEventName))
        {
            Initialise(gameEventName);
        }

        _gameEventDict[gameEventName] += funcToSub;
    }

    // Unsubscribe function handler from event
    public static void Unsubscribe(Enum gameEventName, Action<object> funcToUnsub)
    {
        // Check if event exists, then unsub handler function from it
        if (_gameEventDict.ContainsKey(gameEventName))
        {
            _gameEventDict[gameEventName] -= funcToUnsub;
        }
    }

    // Trigger event
    public static void Trigger(Enum gameEventName, object data)
    {
        // Check if event exists, then invoke and execute all handlers subscribed to it
        if (_gameEventDict.ContainsKey(gameEventName))
        {
            _gameEventDict[gameEventName]?.Invoke(data);
        }
    }
}
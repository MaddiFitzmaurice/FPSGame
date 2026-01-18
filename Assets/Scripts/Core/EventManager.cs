using System;
using System.Collections.Generic;

public static class EventManager
{
    private static Dictionary<Enum, Action<object>> _gameEventDict = new Dictionary<Enum, Action<object>>();

    // Create an event and add to the dictionary
    public static void Activate(Enum gameEventName)
    {
        if (!_gameEventDict.ContainsKey(gameEventName))
        {
            Action<object> newGameEvent = null;
            _gameEventDict.Add(gameEventName, newGameEvent);
        }
    }

    // Delete an event if there are no subscribers 
    public static bool Deactivate(Enum gameEventName)
    {
        if (_gameEventDict.ContainsKey(gameEventName))
        {
            if (_gameEventDict[gameEventName].GetInvocationList().Length == 0)
            {
                _gameEventDict.Remove(gameEventName);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    // Subscribe function handler to event
    public static void Subscribe(Enum gameEventName, Action<object> funcToSub)
    {
        // Check if event exists, then sub handler function to it
        if (!_gameEventDict.ContainsKey(gameEventName))
        {
            Activate(gameEventName);
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
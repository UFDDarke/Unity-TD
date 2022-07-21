using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseGameEvent<T> : ScriptableObject
{
	// Events are called, then everything listening for that event is notified.
	// Therefore, this needs to store the listeners for events.
	private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();
	// Whenever we declare what <T> is, T is then defined as that type for the rest of the class.

	public void Raise(T item)
	{
		// We must do a backwards for loop, incase a listener destroys itself and throws everything out of wack
		for (int i = eventListeners.Count() - 1; i >= 0; i--)
		{
			// Inform all listeners that the event has happened, and pass the data.
			eventListeners[i].OnEventRaised(item);
		}
	}

	public void RegisterListener(IGameEventListener<T> listener)
	{
		// Check to make sure that the listener isn't already in the list
		if(!eventListeners.Contains(listener))
		{
			eventListeners.Add(listener);
		}
	}

	public void UnregisterListener(IGameEventListener<T> listener)
	{
		if(eventListeners.Contains(listener))
		{
			eventListeners.Remove(listener);
		}
	}
}

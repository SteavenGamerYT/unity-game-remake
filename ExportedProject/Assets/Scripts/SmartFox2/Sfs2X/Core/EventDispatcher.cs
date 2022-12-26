using System;
using System.Collections;

namespace Sfs2X.Core
{
	public class EventDispatcher
	{
		private object target;

		private Hashtable listeners = new Hashtable();

		public EventDispatcher(object target)
		{
			this.target = target;
		}

		public void AddEventListener(string eventType, EventListenerDelegate listener)
		{
			EventListenerDelegate a = listeners[eventType] as EventListenerDelegate;
			a = (EventListenerDelegate)Delegate.Combine(a, listener);
			listeners[eventType] = a;
		}

		public void DispatchEvent(BaseEvent evt)
		{
			//Discarded unreachable code: IL_007b
			EventListenerDelegate eventListenerDelegate = listeners[evt.Type] as EventListenerDelegate;
			if (eventListenerDelegate != null)
			{
				evt.Target = target;
				try
				{
					eventListenerDelegate(evt);
				}
				catch (Exception ex)
				{
					throw new Exception("Error dispatching event " + evt.Type + ": " + ex.Message + " " + ex.StackTrace, ex);
				}
			}
		}

		public void RemoveAll()
		{
			listeners.Clear();
		}
	}
}

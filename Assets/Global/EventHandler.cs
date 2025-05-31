using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.LightTransport;

	public enum Type
	{
		Enemy
	}

	public class EventHandler
	{
		private Dictionary<Enum, EventContainer> EventDic = new Dictionary<Enum, EventContainer>();

		public void Register<TEvent>(Enum enumType, Action<TEvent> action)
		{
			if (!EventDic.ContainsKey(enumType))
			{
				EventDic.Add(enumType, new EventContainer());
			}
			EventDic[enumType].Register<TEvent>(new EventWrapper<TEvent>(action));
		}

		public void UnRegister(Enum enumType)
		{
			if (EventDic.ContainsKey(enumType))
			{
				EventDic.Remove(enumType);
			}
		}

		public void Invoke<TEvent>(Enum enumType, TEvent ev)
		{
			if (!EventDic.ContainsKey(enumType))
			{
				Debug.LogError($"[EventHandler] NotRegisterEvent {nameof(enumType)}");
				return;
			}
			EventDic[enumType].Invoke<TEvent>(ev);
		}
	}

	public class EventContainer
	{
		public HashSet<EventWrapper> EventWrapperSet = new();

		public void Register<TEvent>(EventWrapper<TEvent> eventWrapper)
		{
			EventWrapperSet.Add(eventWrapper);
		}

		public void UnRegister<TEvent>(Action<TEvent> registerEventr)
		{
			foreach (var ev in EventWrapperSet)
			{
				if (ev.EqualEvent(registerEventr))
				{
					EventWrapperSet.Remove(ev);
					return;
				}
			}
		}

		public void Invoke<TEvent>(TEvent ev)
		{
			foreach (var post in EventWrapperSet)
			{
				post.Invoke(ev);
			}
		}
	}

	public abstract class EventWrapper
	{
		public abstract void Invoke(object ev);
		public abstract bool EqualEvent(object ev);
	}

	public class EventWrapper<TEvent> : EventWrapper//  원하는 매개변수로 받을수 있게 Action을 제네릭 클래스로 덮음
	{

		public Action<TEvent> GameEvent;

		public override bool EqualEvent(object ev)
		{
			if (GameEvent == (Action<TEvent>)ev)
			{
				return true;
			}
			return false;
		}

		public override void Invoke(object ev) //object타입으로 받는거 해결 언박싱 문제
		{
			GameEvent?.Invoke((TEvent)ev);
		}

		public EventWrapper(Action<TEvent> ev)
		{
			GameEvent = ev;
		}

	}


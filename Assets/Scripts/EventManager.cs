﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public static EventManager Instance
    {
        get { return instance; }
        set { }
    }

    private static EventManager instance = null;

    private Dictionary<EVENT_TYPE, List<IListener>> Listeners = new Dictionary<EVENT_TYPE, List<IListener>>();
	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }
	}
	
    public void AddListener(EVENT_TYPE Event_Type, IListener Listener)
    {
        List<IListener> ListenList = null;
        if(Listeners.TryGetValue(Event_Type, out ListenList))
        {
            ListenList.Add(Listener);
            return;
        }

        ListenList = new List<IListener>();
        ListenList.Add(Listener);
        Listeners.Add(Event_Type, ListenList);
    }

    public void PoistNotification(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        // 모든 리스너들에게 이벤트를 알린다

        List<IListener> ListenList = null;
        if (!Listeners.TryGetValue(Event_Type, out ListenList))
            return;

        for(int i = 0; i < ListenList.Count; i++)
        {
            if(!ListenList[i].Equals(null))
            {
                ListenList[i].OnEvent(Event_Type, Sender, Param);
            }
        }
    }

    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        Listeners.Remove(Event_Type);
    }

    public void RemoveRedundancies()
    {
        // 쓸모없는 항목을 제거한다.
        Dictionary<EVENT_TYPE, List<IListener>> TmpListeners = new Dictionary<EVENT_TYPE, List<IListener>>();

        foreach(KeyValuePair<EVENT_TYPE, List<IListener>> Item in Listeners)
        {
            for(int i = Item.Value.Count-1; i >= 0; i--)
            {
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }

            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);
        }

        Listeners = TmpListeners;
    }

    private void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }
}
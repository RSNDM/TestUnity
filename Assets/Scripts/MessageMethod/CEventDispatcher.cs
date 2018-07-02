using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public delegate void CEventListenerDelegate(CBaseEvent evt);
public class CEventDispatcher : MonoBehaviour {
    static CEventDispatcher instance;
    public static CEventDispatcher GetInstance()
    {
        if (instance==null)
        {
            instance = new CEventDispatcher();
        }
        return instance;
    }
    private Hashtable listeners = new Hashtable();
    public void AddEventListener(CEventType eventType,CEventListenerDelegate listener)
    {
        CEventListenerDelegate cEventListenerDelegate = this.listeners[eventType] as CEventListenerDelegate;
        //将两个委托链接起来
        cEventListenerDelegate = (CEventListenerDelegate)Delegate.Combine(cEventListenerDelegate, listener);
        this.listeners[eventType] = cEventListenerDelegate;
    }
    public void RemoveEventListener(CEventType eventType,CEventListenerDelegate listener)
    {
        CEventListenerDelegate cEventListenerDelegate = this.listeners[eventType] as CEventListenerDelegate;
        if (cEventListenerDelegate!=null)
        {
            cEventListenerDelegate = (CEventListenerDelegate)Delegate.Remove(cEventListenerDelegate, listener);
        }
        this.listeners[eventType] = cEventListenerDelegate;
    }
    /// <summary>
    /// 使用枚举传递
    /// </summary>
    /// <param name="evt"></param>
    public void DispatchEvent(CBaseEvent evt)
    {
        CEventListenerDelegate cEventListenerDelegate = this.listeners[evt.Type] as CEventListenerDelegate;
        if (cEventListenerDelegate!=null)
        {
            try
            {
                cEventListenerDelegate(evt);
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat(new string[]
                {
                    "Error dispatching event",
                    evt.Type.ToString(),
                    ":",
                    ex.Message,
                    " ",
                    ex.StackTrace
                }), ex);
            }
        }
    }
    /// <summary>
    /// 使用字符串传递
    /// </summary>
    /// <param name="evt"></param>
    public void DispatchStringEvent(CBaseEvent evt)
    {
        CEventListenerDelegate cEventListenerDelegate = this.listeners[evt.EventName] as CEventListenerDelegate;
        if (cEventListenerDelegate != null)
        {
            try
            {
                cEventListenerDelegate(evt);
            }
            catch (Exception ex)
            {

                throw new Exception(string.Concat(new string[]
                {
                    "Error dispatching event",
                    evt.Type.ToString(),
                    ":",
                    ex.Message,
                    " ",
                    ex.StackTrace
                }), ex);
            }
        }
    }
    public void RemoveAll()
    {
        this.listeners.Clear();
    }
    private void Awake()
    {
       CEventDispatcher.GetInstance().AddEventListener(CEventType.GAME_DATA, MyTestFun);
       CEventDispatcher.GetInstance().AddEventListener(CEventType.GAME_OVER, MyTestFun);
    }
    // Use this for initialization
    void Start () {
        foreach (var item in listeners)
        {
            Debug.Log(item);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void MyTestFun(CBaseEvent evt)
    {
        Debug.Log("收到消息" + evt.EventName);
    }
}

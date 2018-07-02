using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CEventType
{
    GAME_OVER,
    GAME_WIN,
    PAUSE,
    GAME_DATA,
}
public class CBaseEvent : MonoBehaviour {
    protected Hashtable arguments;
    protected CEventType type;
    protected object sender;
    protected string eventName;


    public CEventType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public IDictionary Params
    {
        get { return this.arguments; }
        set
        {
            this.arguments = (value as Hashtable);
        }
    }
    public object Sender
    {
        get
        {
            return this.sender;
        }
        set
        {
            this.sender = value;
        }
    }
    public string EventName
    {
        get
        {
            return eventName;
        }
        set
        {
            eventName = value;
        }
    }

    public override string ToString()
    {
        return this.type+"{"+((this.sender==null)?"null":this.sender.ToString())+"}";
    }
    public CBaseEvent Clone()
    {
        return new CBaseEvent(this.type, this.arguments, sender);
    }
    public CBaseEvent(CEventType type,Hashtable args,object Sender)
    {
        this.type = type;
        this.arguments = args;
        sender = Sender;
        if (this.arguments==null)
        {
            this.arguments = new Hashtable();
        }
    }
    public CBaseEvent(CEventType type, object Sender)
    {
        this.type = type;
        //this.arguments = args;
        sender = Sender;
        if (this.arguments == null)
        {
            this.arguments = new Hashtable();
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

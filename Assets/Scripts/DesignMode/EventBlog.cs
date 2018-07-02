using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//委托充当订阅者的接口类
public delegate void NotifyEventHadnler(NewBlog sender);

//抽象订阅类
public class NewBlog
{
    public NotifyEventHadnler NotifyEvent;
    public string Symbol { get; set; }//描述订阅号的相关信息
    public string Info { get; set; }//描写这次update的信息
    public NewBlog(string symbol,string info)
    {
        this.Symbol = symbol;
        this.Info = info;
    }
    #region 新增对订阅号列表的维护操作
    public void AddObserver(NotifyEventHadnler ob)
    {
        NotifyEvent += ob;
    }
    public void RemoveObserver(NotifyEventHadnler ob)
    {
        NotifyEvent -= ob;
    }
    #endregion

    public void Update()
    {
        if (NotifyEvent!=null)
        {
            NotifyEvent(this);
        }
    }

}
    //具体订阅号类
    public class MyNewBlog:NewBlog
    {
        public MyNewBlog(string symbol,string info):base(symbol,info)
        {

        }
    }

    //具体订阅者类
    public class Subscriber
    {
        public string Name { get; set; }
        public Subscriber(string name)
        {
            this.Name = name;
        }
        public void Receive(NewBlog obj)
        {
            
            if (obj!=null )
            {
                Debug.Log(string.Format("订阅者：{0}  观察到了{1} {2}", Name, obj.Symbol, obj.Info));
            }
        }
    }

public class EventBlog : MonoBehaviour {

	// Use this for initialization
	void Start () {
        NewBlog @new = new MyNewBlog("你看我像谁", "发布了一篇新微博");
        Subscriber bb = new Subscriber("beibei");
        Subscriber cc = new Subscriber("ceicei");

        @new.AddObserver(bb.Receive);
        @new.AddObserver(cc.Receive);
        @new.Update();
        @new.RemoveObserver(cc.Receive);
        @new.Update();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

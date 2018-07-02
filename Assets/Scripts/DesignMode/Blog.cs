using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//订阅者接口
public interface IObserve
{
    void Receive(mBlog message);
}
//订阅号抽象类
public abstract class mBlog
{
    //保存订阅者列表
    private List<IObserve> observers = new List<IObserve>();

    public string Symbol { get; set; }//描写订阅号的相关信息
    public string Info { get; set; }//描写此次update的信息
    public mBlog(string symbol,string info  )
    {
        this.Symbol = symbol;
        this.Info = info;
    }

    //对同一个订阅号，新增和删除订阅者的操作
    public void AddObserver(IObserve ob)
    {
        observers.Add(ob);
    }
    public void RemoveObserver(IObserve ob)
    {
        if (observers.Contains(ob))
        {
            observers.Remove(ob);
        }
    }

    public void Update()
    {
        //遍历订阅者进行通知
        foreach (IObserve ob in observers)
        {
            if (ob!=null)
            {
                ob.Receive(this);
            }
        }
    }
}

//具体的订阅类
public class Subscribe:IObserve
{
    public string Name { get; set; }
    public Subscribe(string name)
    {
        this.Name = name;
    }
    public void Receive(mBlog mB)
    {
        Debug.Log(string.Format("订阅者：{0}  观察到了{1} {2}", Name, mB.Symbol, mB.Info));
    }
}

public class MyBlog : mBlog
{
    public MyBlog(string symbol, string info) : base(symbol, info)
    {

    }
}
public class Blog : MonoBehaviour {

    private int num = 0;
    private const int total = 30;
	// Use this for initialization
	void Start () {
        mBlog b = new MyBlog("你看我像谁", "发布了一篇微博");
        b.AddObserver(new Subscribe("金装"));
        b.AddObserver(new Subscribe("Sony"));
        b.Update();
        //StartCoroutine("coRoutine");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Update");
        int[] t = { 1,2,3};
        
        paramasArg(t);
	}
    private void LateUpdate()
    {
        //Debug.Log("LateUpdate");
    }
    IEnumerator coRoutine()
    {
        while (num<total)
        {
            num++;
            Debug.Log(num);
            yield return null;
        }
    }
    public void paramasArg(params int[] nums)
    {
        foreach (int  c in nums)
        {
            Debug.Log("paramas num=" + c);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaluculateClass : MonoBehaviour {
    public delegate int CalDel(int n1, int n2);
    public delegate void MorningDelegate(string n);
    
    static MorningDelegate morning;
    public int CallCaluculate(int n1,int n2,CalDel cal)
    {
        Debug.Log("底层处理" + n1);
        Debug.Log("底层处理" + n2);
        return cal(n1, n2);
    }
    // Use this for initialization
    void Start () {
        transform.gameObject.AddComponent<Funclass>();
        transform.GetComponent<Funclass>().action += EventFunclass;

        transform.GetComponent<Funclass>().GetSum(1, 2);
        //CallCaluculate(1, 2, fun.GetSum);
        //CallCaluculate(3, 4, fun.GetMulti);
        //SayMorning("贝贝", MorningChinese);
        //SayMorning("bess", MorningEnglish);
        morning += MorningChinese;
        morning += MorningEnglish;
        morning("Bess");
        StartCoroutine(sayMorning());
	}
    public void SayMorning(string n,MorningDelegate morningDelegate)
    {
        morningDelegate(n);
    }
    public void EventFunclass(Funclass c)
    {
        Debug.Log("我加入进Funclass的方法里面啦,名字是: "+c.Name);
    }
    public void MorningChinese(string n)
    {
        Debug.Log("早安"+n);
    }
    public void MorningEnglish(string n)
    {
        Debug.Log("Good Morning"+n);
    }
    IEnumerator sayMorning()
    {
        string tem = "贝贝";
        SayMorning(tem, MorningChinese);
        //暂缓一帧下一帧接着处理
        yield return tem="bess";
        SayMorning(tem, MorningEnglish);

    }
}

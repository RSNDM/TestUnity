using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funclass : MonoBehaviour {
    public System.Action<Funclass> action;
    public string Name="第一个FunClass";
	// Use this for initialization
	void Start () {
        //Debug.Log("Funclass计算结果： " + GetSum(1, 2));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public int GetSum(int n1,int n2)
    {
        action(this);
        return n1 + n2;
    }
    public int GetMulti(int n1,int n2)
    {
        return n1 + n2;
    }
}

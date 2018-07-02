using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleDataManage : MonoBehaviour {
    private static BattleDataManage instance;
    //定义一个表示确保线程同步
    private static readonly object locker = new object();
    private BattleDataManage()
    {
        instance = this;
    }
    private Dictionary<long, SpriteController> DicMonsters;
    public Dictionary<long, SpriteController> dicMonsters
    {
        get
        {
            return DicMonsters;
        }

    }

    public SpriteController selfplayer
    {
        get
        {
            return SelfPlayer;
        }

        set
        {
            SelfPlayer = value;
        }
    }

    private SpriteController SelfPlayer;//加入房间或者游戏开始将玩家赋值

    /// <summary>
    /// 定义公有方法提供一个全局访问点，同时你也可以定义共有属性来提供全局访问点
    /// </summary>
    /// <returns></returns>
    public static BattleDataManage GetInstance()
    {
        lock (locker)
        {
            if (instance==null)
            {
                instance = new BattleDataManage();
            }
        }
        return instance;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

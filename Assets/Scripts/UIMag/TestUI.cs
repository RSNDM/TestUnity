using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour {
    public GameObject LoginPrf;
    public BaseUI LoginBase;
	// Use this for initialization
	void Start () {
        //LoginBase.UIName = "登录界面";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnGUI()
    {
        LoadUIFinish uIFinish = new LoadUIFinish();
        if(GUILayout.Button("这是个按钮")          )
        {
            //Command tempcmd = new Command(Command.CmdType.Create, "主界面",typeof(BaseUI), uIFinish);
            UIMgr.Instance.AddUI(LoginBase);
            UIMgr.Instance.CreateUI("LoginPanel", typeof(BaseUI), uIFinish);
        }
        if (GUILayout.Button("1"))
        {
            UIMgr.Instance.DestroyUI("LoginPanel");
        }
    }
}
public class LoadUIFinish : ILoadUIListener
{
    
    public void Failure()
    {
        throw new System.NotImplementedException();
    }

    public void Finish(BaseUI uI)
    {
        Debug.Log("成功加载UI界面： " + uI.UIName);
    }
}

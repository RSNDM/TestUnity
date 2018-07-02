using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour {
    /// <summary>
    /// 当前界面名称
    /// </summary>
    [HideInInspector]
    public string UIName;

    private Transform mTransform;
    public Transform CacheTransform
    {
        get
        {
            if (mTransform == null) mTransform = this.transform;
            return mTransform;
        }
    }

    private GameObject mGo;
    public GameObject CacheGameObject
    {
        get
        {
            if (mGo == null) mGo = this.gameObject;
            return mGo;
        }
    }

    /// <summary>
    /// 显示当前UI
    /// </summary>
    /// <param name="param">附加参数</param>
    public void Show(object param=null)
    {
        CacheGameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏当前界面
    /// </summary>
    public void Hide()
    {
        CacheGameObject.SetActive(false);
    }

    protected virtual void OnShow(object param) { }
    protected virtual void OnHide() { }
    protected virtual void OnInit() { }
    protected virtual void OnAwake() { }
    protected virtual void OnDestroy() { }

    private void Awake()
    {
        OnAwake();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

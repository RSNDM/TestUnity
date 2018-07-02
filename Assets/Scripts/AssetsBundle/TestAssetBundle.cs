using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAssetBundle : MonoBehaviour {
    public string AssetBundleName = "cube1.assetbundle";

    private string dir = "";
    private AssetBundle bundle = null;
    private UnityEngine.Object asset = null;
    private GameObject go = null;

    
	// Use this for initialization
	void Start () {
        dir = Application.dataPath + "/StreamingAssets/";
        LoadBundle();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //加载AssetBundle
    private void LoadBundle()
    {
        bundle = AssetBundle.LoadFromFile(System.IO.Path.Combine(dir, AssetBundleName));
        if (bundle==null)
        {
            Debug.LogError("LoadBundle Failed       ");
        }
    }
    //从AssetBundle加载Asset
    private void LoadAsset()
    {
        if (bundle==null)
        {
            return;
        }
        asset = bundle.LoadAsset("Cube1");
        if (asset==null)
        {
            Debug.LogError("LoadAsset Failed");
        }
    }

    //根据Asset实例化GameObject
    private void Instantiate()
    {
        if (asset == null) return;
        go = GameObject.Instantiate(asset) as GameObject;
        if (go == null)
        {
            Debug.LogError("Instantiate Failed");
        }
    }

    //销毁GameObject
    private void Destroy()
    {
        if (go==null)
        {
            return;
        }
        GameObject.Destroy
            (go);
        go = null;
    }

    //弱卸载，释放AssetBundle本身的内存
    private void Unload()
    {
        if (bundle == null) return;

        //unload完了，bundle就不能再用了，置空
        bundle.Unload(false);
        asset = null;
        bundle = null;
    }

    //强卸载（无视引用的卸载），释放AssetBundle本身的内存，同时回收从AssetBundle抽取的Asset
    private void UnloadForce()
    {
        if (bundle == null) return;

        //unload完了，bundle不能再用了，置空
        bundle.Unload(true);
        asset = null;
        bundle = null; 
    }

    //全局弱卸载，回收无引用Asset
    private void UnloadUnusedAssets()
    {
        Resources.UnloadUnusedAssets();
    }
}

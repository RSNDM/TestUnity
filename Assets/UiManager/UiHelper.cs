﻿using System;
using UnityEngine;
/// <summary>
/// UI辅助类
/// </summary>
public static class UiHelper
{
    /// <summary>
    /// 加入UI窗口到父节点下
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    static public GameObject AddChild(GameObject parent, string name)
    {
        if (parent == null)
            throw new ArgumentNullException("parent");

        var go = new GameObject(name);
        go.layer = parent.layer;

        var tc = go.GetComponent<RectTransform>();
        var tp = parent.GetComponent<RectTransform>();
        tc.SetParent(tp, false);

        return go;
    }

    public static GameObject AddChild(GameObject parent, GameObject prefab)
    {
        if (parent == null)
            throw new ArgumentNullException("parent");
        if (prefab == null)
            throw new ArgumentNullException("prefab");

        var go = GameObject.Instantiate(prefab);
        go.layer = parent.layer;

        var tc = go.GetComponent<RectTransform>();
        var tp = parent.GetComponent<RectTransform>();
        tc.SetParent(tp, false);

        return go;
    }
}

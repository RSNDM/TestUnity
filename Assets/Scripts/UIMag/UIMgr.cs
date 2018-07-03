using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class UIMgr:EventNode
{
    private static UIMgr instance;
    public static UIMgr Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// 所有UI
    /// </summary>
    private Dictionary<string, BaseUI> mDicUI = new Dictionary<string, BaseUI>();

    /// <summary>
    /// 添加一个UI
    /// </summary>
    /// <param name="ui"></param>
    public void AddUI(BaseUI ui)
    {
        if (ui!=null)
        {
            mDicUI[ui.UIName] = ui;
        }
    }

    /// <summary>
    /// 移除一个UI
    /// </summary>
    /// <param name="ui"></param>
    public  void RemoveUI(BaseUI ui)
    {
        if (ui!=null&&mDicUI.ContainsKey(ui.name))
        {
            mDicUI.Remove(ui.UIName);
        }
    }


}
public interface ILoadUIListener
{
    void Finish(BaseUI uI);
    void Failure();
}
public class Command
{
    /// <summary>
    /// 命令类型
    /// </summary>
    public enum CmdType
    {
        /// <summary>
        /// 创建
        /// </summary>
        CreateAndShow,
        /// <summary>
        /// 创建
        /// </summary>
        Create,
        /// <summary>
        /// 显示或者刷新
        /// </summary>
        Show,
        /// <summary>
        /// 隐藏
        /// </summary>
        Hide,
        /// <summary>
        /// 删除
        /// </summary>
        Destroy,
    }
    public string uiName;
    public Type type;
    public ILoadUIListener listener;
    public object param;
    public CmdType cmdType;
    public bool createCanCall = true;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="_cmdtype">命令类型</param>
    /// <param name="_uiName">UI名称</param>
    /// <param name="_param">要传入的参数</param>
    public Command (CmdType _cmdtype,string _uiName,object _param)
    {
        uiName = _uiName;
        cmdType = _cmdtype;
        param = _param;
    }
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="_cmdType">命令类型</param>
    /// <param name="_uiName">UI名称</param>
    /// <param name="_type">要绑定的脚本</param>
    /// <param name="_listener">加载完成之后的回调</param>
    public Command(CmdType _cmdType,string _uiName,Type _type,ILoadUIListener _listener)
    {
        cmdType = _cmdType;
        type = _type;
        listener = _listener;
        uiName = _uiName;
    }
    public static Command CreateAndShowCMD(string uiName,Type type,ILoadUIListener listener,object param,bool createcanCall)
    {
        Command cmd = new Command(CmdType.CreateAndShow, uiName, type);
        cmd.createCanCall = createcanCall;
        cmd.listener = listener;
        cmd.type = type;
        cmd.param = param;
        return cmd;
    }
    public static Command ShowCMD(string uiName,ILoadUIListener listener,object _param,bool createcancall)
    {
        Command cmd = new Command(CmdType.Show, uiName, _param);
        cmd.createCanCall = createcancall;
        cmd.listener = listener;
        return cmd;
    }
    public static Command CreateCmd(Type _type,string _uiName,ILoadUIListener _listener)
    {
        return new Command(CmdType.Create, _uiName, _type, _listener);
    }
    public static Command HideCmd(string _uiname)
    {
        return new Command(CmdType.Hide, _uiname, null);

    }
    public static Command DestroyCmd(string _uiname)
    {
        return new Command(CmdType.Destroy, _uiname, null);
    }
}

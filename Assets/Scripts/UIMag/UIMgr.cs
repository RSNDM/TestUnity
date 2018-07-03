using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

/// <summary>
/// UIMgr只需要创建各种Command就可以实现各种操作
/// </summary>
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

    public List<Command> cmdList = new List<Command>();

    internal Transform UIROOT = null;
    private void Awake()
    {
        UIROOT = this.transform.Find("UIROOT");
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    /// <summary>
    /// 创建UI
    /// 外界方法将创建ui的命令加入命令列表 实际内部调用为_Create方法
    /// </summary>
    /// <param name="uiName">UI名称</param>
    /// <param name="type">要绑定的脚本</param>
    /// <param name="listener">创建完成的回调</param>
    public void CreateUI(String uiName,Type type,ILoadUIListener listener)
    {
        cmdList.Add(Command.CreateCmd(type, uiName, listener));
    }
    private void _Create(Command cmd)
    {
        BaseUI ui = null;
        mDicUI.TryGetValue(cmd.uiName, out ui);
        if (ui!=null)
        {
            if (cmd.listener!=null)
            {
                cmd.listener.Finish(ui);
            }
            else
            {
                //从资源管理中使用读取方法获取UI资源（prefab）
                //ResMgr.Instance.Load(cmd.uiName, new LoadResFinish(cmd));
            }
        }
    }
    public class lis : ILoadUIListener
    {
        void ILoadUIListener.Failure()
        {
            throw new NotImplementedException();
        }

        void ILoadUIListener.Finish(BaseUI uI)
        {
            throw new NotImplementedException();
        }
    }
    public void ShowUI(string uiName,Type type,ILoadUIListener lis,object param=null,bool createcancall=false)
    {
        BaseUI ui = null;
        mDicUI.TryGetValue(uiName, out ui);
        if (ui==null)
        {
            cmdList.Add(Command.CreateAndShowCMD(uiName, type, lis, param, createcancall));
        }
        else
        {
            cmdList.Add(Command.ShowCMD(uiName, lis, param, createcancall));
        }
    }
    /// <summary>
    /// 显示一个界面
    /// </summary>
    /// <param name="cmd"></param>
    private void _ShowUI(Command cmd)
    {
        BaseUI u = null;
        mDicUI.TryGetValue(cmd.uiName, out u);
        if (u!=null)
        {
            if (cmd.listener!=null)
            {
                cmd.listener.Finish(u);
            }
            u.Show();
        }
    }

    /// <summary>
    /// 外部调用隐藏ui
    /// UIMgr.Instance.HideUI("UINAME")
    /// </summary>
    /// <param name="uiName"></param>
    public void HideUI(string uiName)
    {
        cmdList.Add(Command.HideCmd(uiName));
    }

    private void _HideUI(Command cmd)
    {
        BaseUI u = null;
        mDicUI.TryGetValue(cmd.uiName, out u);
        if (u!=null)
        {
            u.Hide();
        }
    }

    /// <summary>
    /// 删除UI
    /// </summary>
    /// <param name="name">UI名称</param>
    public void DestroyUI(string name)
    {
        cmdList.Add(Command.DestroyCmd(name));
    }

    private void _DestroyUI(Command cmd)
    {
        BaseUI ui = null;
        mDicUI.TryGetValue(cmd.uiName, out ui);
        if (ui!=null)
        {
            mDicUI.Remove(ui.UIName);
            Destroy(ui.CacheGameObject);
            
        }
    }
    private void Update()
    {
        lis c = new lis();
        GameObject gam = new GameObject();
        ShowUI("123", typeof(GameObject), c, gam, true);
        if (cmdList.Count>0)
        {
            Command tempcmd = null;
            tempcmd = cmdList[0];
            if (tempcmd==null)
            {
                cmdList.RemoveAt(0);
            }
            else
            {
                switch (tempcmd.cmdType)
                {
                    case Command.CmdType.CreateAndShow:
                        _Create(tempcmd);
                        break;
                    case Command.CmdType.Create:
                        _Create(tempcmd);
                        break;
                    case Command.CmdType.Show:
                        _ShowUI(tempcmd);
                        break;
                    case Command.CmdType.Hide:
                        _HideUI(tempcmd);
                        break;
                    case Command.CmdType.Destroy:
                        _DestroyUI(tempcmd);
                        break;
                    default:
                        break;
                }
                cmdList.RemoveAt(0);
            }
        }
    }

}
public class LoadResFinish:IResLoadListener
{
    public Command cmd;
    public LoadResFinish(Command _cmd)
    {
        cmd = _cmd;
    }
    public void Finish(object asset)
    {
        if (cmd==null)
        {
            return;
        }
        GameObject gameObject = UnityEngine.GameObject.Instantiate<GameObject>(asset as GameObject);
        gameObject.SetActive(false);
        BaseUI ui = gameObject.AddComponent(cmd.type) as BaseUI;
        ui.UIInit();
        ui.UIName = cmd.uiName;
        gameObject.name = ui.UIName;
        ui.CacheTransform.SetParent(UIMgr.Instance.UIROOT, false);
        UIMgr.Instance.AddUI(ui);
        if (cmd.cmdType==Command.CmdType.CreateAndShow)
        {
            UIMgr.Instance.ShowUI(cmd.uiName, cmd.type, cmd.listener);
        }
        else if (cmd.createCanCall&&cmd.listener!=null)
        {
            cmd.listener.Finish(ui);
        }
    }
    public void Failure()
    {
        if (cmd.createCanCall&&cmd.listener!=null)
        {
            cmd.listener.Failure();
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

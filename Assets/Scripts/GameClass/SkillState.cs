using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SpriteController:MonoBehaviour
    {

        public ActionMng actionMng;
        public AIState.AIstate mAIState;
        public SpriteController()
        {
            actionMng = new ActionMng();        
        }
        public void AttackTo(SpriteController sprite,int damageValue)
        {

        }
    }

    public class ActionMng
    {
        static  Dictionary<string, float> _actionDic = new Dictionary<string, float>();
        //out可以返回方法中多余值，一个方法返回多个值
        public  bool  TryGetAnimationLength(string name,out float index)
        {
            foreach (var  time in _actionDic)
            {
                if (_actionDic.ContainsKey(name))
                {
                    index = _actionDic[name];
                return true;
                }
                else
                {
                    index = 0f;
                    return false;
                }
            }
        index = 0f;
        return false;
        }
    }
public abstract class SkillBase
{
    /// <summary>
    /// 角色技能处于状态
    /// </summary>
    public enum SkillState
    {
        Start,
        Execution,
        Interrupts,
        Finish,
        None

    }
    enum mAIState
    {
        Dead
    }

    //技能状态
    public SkillState State { get; protected set; }
    public int SkillID { get { return dataVo == null ? 0 : dataVo.ID; } }
    public SkillDataVo dataVo { get; protected set; }

    //特效
    protected GameObject effect;
    protected bool ifInitEffect = false;//是否已经播放了特效
    protected float effectBeginTime;
    protected float effectTime = 5f;    //特效时长

    //伤害
    protected bool damaged = false;     //是否已经计算了伤害


    //动画
    protected float curAniLength;//当前动画时长
    protected string aniName;
    protected float CurCD;
    protected SpriteController owner;
    protected float timer;

    //回收动作
    protected bool IfUseActionReturn;
    protected float ActionReturnTime;

    //攻击目标，角度范围，距离
    protected bool InForward(Transform trans,float _angle,float _dis)
    {
        if (null==trans)
        {
            return false;
        }

        //获得从当前Obj到目标Obj的方向，然后求和当前Obj的朝向夹角
        Vector2 tarPos = new Vector2(trans.position.x, trans.position.z);
        Vector2 myPos = new Vector2(owner.transform.position.x, owner.transform.position.z);
        Vector2 mydir = new Vector2(owner.transform.forward.x, owner.transform.forward.z);
        Vector2 dir = tarPos - myPos;
        float angle = Vector2.Angle(mydir.normalized, dir.normalized);

        if (angle<_angle/2f)
        {
            float toPosDis = Vector2.Distance(tarPos, myPos);
            if (toPosDis<=_dis)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public SkillBase(SpriteController obj, int skillID)
    {
        State = SkillState.None;
        this.owner = obj;
        this.CurCD = 0f;

        dataVo = ConfigManager.GetSkillDataById(skillID);
        if (owner.actionMng != null && owner.actionMng.TryGetAnimationLength(dataVo.actionName, out curAniLength))
        {
            curAniLength -= 0f;
        }
        else
        {
            curAniLength = 1f;
        }
        if (dataVo.BufferID.Length > 0)
        {
            dataVo.bufferEntity = new BufferDataVo[dataVo.BufferID.Length];
            for (int i = 0; i < dataVo.bufferEntity.Length; i++)
            {
                dataVo.bufferEntity[i] = ConfigManager.GetConfigBufferData(dataVo.BufferID[i]);
            }
        }
        aniName = dataVo.actionName;

        //加载特效
        GameObject effecPre = LoadCache.LoadEffect(dataVo.effectName);
        if (effecPre != null)
        {
            effect = GameObject.Instantiate(effecPre) as GameObject;
            effect.SetActive(false);
        }
    }

        //打断
    virtual public void SetInerrupts() { State = SkillState.Interrupts; }

    virtual public void ProcessBuffer(int index) { }

    virtual public bool IsCanInterrupts()
    {
        if (State==SkillState.None)
        {
            return true;
        }
        if (dataVo.minInterrupt!=-1&&timer<=dataVo.minInterrupt)
        {
            return true;
        }
        if (dataVo.maxInterrupt!=-1&&timer>=dataVo.maxInterrupt)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 施放技能
    /// </summary>
    virtual public void Execute()
    {
        //重写后每一个不同的Skill添加不同的事件
        switch (State)
        {
            case SkillState.Start:
                break;
            case SkillState.Execution:
                break;
            case SkillState.Interrupts:
                break;
            case SkillState.Finish:
                break;
            case SkillState.None:
                break;
            default:
                break;
        }
    }

    virtual public void Begin()
    {
        State = SkillState.Start;
    }

    virtual public void DoUpdate()
    {
        if (State==SkillState.None)
        {
            return;
        }
        Execute();
        CalcCd();
    }
    /// <summary>
    /// 计算CD
    /// </summary>
    public void CalcCd()
    {
        CurCD += Time.deltaTime;
        if (CurCD>=dataVo.SkillCD)
        {
            CurCD = 0f;
        }
    }
}

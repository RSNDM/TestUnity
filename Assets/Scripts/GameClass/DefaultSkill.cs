using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSkill : SkillBase {

    private bool isMove;
    Vector3 moveDir;
    
    public DefaultSkill(SpriteController obj,int skillID):base(obj,skillID)
    {

    }
    void ForAllEnemy()
    {
        Dictionary<long, SpriteController> dic = BattleDataManage.GetInstance().dicMonsters;
        foreach (SpriteController con in dic.Values)
        {
            if (isMove)
            {
                continue;
            }

            //技能的伤害判断和处理
            bool inforward = InForward(con.transform, dataVo.attackFanAngle, dataVo.attackFanRange);
            if (inforward)
            {
                int damageValue = dataVo.harm;
                owner.AttackTo(con, damageValue);
                //公共方法显示攻击面板，并且刷新血量
                //Util.CallMethod("FightingPanel", "UpdateHeroHpMp", BattlerDataManager.Instance.SelfPlayer.SpiritVO.CurHp, BattlerDataManager.Instance.SelfPlayer.SpiritVO.CurMp);   //刷新角色血条
                if (BattleDataManage.GetInstance().selfplayer.mAIState==AIState.AIstate.Dead)
                {
                    Debug.Log("Player Dead");
                }

            }
        }
        
    }
    public override void Execute()
    {
        switch (State)
        {
            case SkillState.Start:
                //moveDir = owner.MoveToPostion;
                //timer = 0f;
                //effectBeginTime = 0;
                //owner.MoveMng.StopMove();
                //isMove = false;
                //// IfUseActionReturn = false;
                //damaged = false;
                //ifInitEffect = false;
                ////执行
                //owner.mAIState = AIState.Attack;
                //owner.ActionMng.PlayAnimation(animalName);
                //State = SkillState.Execution;

                Debug.Log("skill ID " + dataVo.ID);
                break;
            case SkillState.Execution:
                timer += Time.deltaTime;

                //触发伤害
                if (!damaged&&timer>dataVo.damagedTime)
                {
                    damaged = true;
                    ForAllEnemy();//伤害计算

                }
                if (!ifInitEffect&&timer>dataVo.fxStartTime)
                {
                    if (effect!=null)
                    {
                        effect.transform.position = owner.transform.position + new Vector3(0, 0.5f, 0) + owner.transform.forward * 2;
                        effect.transform.rotation = owner.transform.rotation;
                        effect.SetActive(false);
                        effect.SetActive(true);
                    }
                    ifInitEffect = true;
                }
                //动画播放结束后，并且触发了特效，触发了伤害
                if (timer>=curAniLength&&damaged&&ifInitEffect)
                {
                    State = SkillState.Finish;
                }
                break;
            case SkillState.Interrupts:
                Debug.Log("被打断");
                State = SkillState.None;
                break;
            case SkillState.Finish:
                Debug.Log("finish");
                break;
            case SkillState.None:
                break;
            default:
                break;
        }
    }
}

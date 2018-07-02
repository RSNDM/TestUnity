using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataVo : MonoBehaviour {
    //属性表
    
    public int ID;
    public string SkillName;
    public string SkillAccount;
    public string actionName;
    public string effectName;
    public int[] BufferID;
    public int minInterrupt;
    public int maxInterrupt;
    public float SkillCD;
    public float attackFanAngle;
    public float attackFanRange;
    public int harm;//伤害量
    public float damagedTime;
    public float fxStartTime;
    public BufferDataVo[] bufferEntity;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class ConfigManager : MonoBehaviour {
    static Dictionary<int,SkillDataVo> _skillDic = new Dictionary<int, SkillDataVo>();
    static Dictionary<int, BufferDataVo> _bufferDic = new Dictionary<int, BufferDataVo>();
    public Dictionary<int,SkillDataVo>  Data
    {
        get { return _skillDic; }
    }
    public SkillDataVo[] GetArray()

    {
        SkillDataVo[] array = new SkillDataVo[_skillDic.Values.Count];
        _skillDic.Values.CopyTo(array, 0);
        
        return array;
    }
    
    void initSkillData(string jsonPath)
    {
        //
        JsonData json = JsonMapper.ToObject(jsonPath);
        foreach (string key in json.Keys)
        {
            SkillDataVo data = new SkillDataVo();
            data.ID = int.Parse(key);
            data.SkillAccount = json[key]["skillAccount"].ToString();
            data.SkillName = json[key]["skillName"].ToString();
            data.actionName=json[key]["ActionName"].ToString();
            if (((IDictionary)json[key]).Contains("BufferID"))
            {
                JsonData BufferID = json[key]["BufferID"];
                if (BufferID.IsArray)
                {
                    data.BufferID = new int[BufferID.Count];
                    for (int i = 0; i < BufferID.Count; i++)
                    {
                        data.BufferID[i] = int.Parse(BufferID[i].ToString());
                    }
                }
            }
            if (_skillDic.ContainsKey(data.ID)==false)
            {
                _skillDic.Add(data.ID, data);
            }

        }
    }
    public void ClearData()
    {
        _skillDic.Clear();
    }
    //读取json文件中的技能栏，用一个单例能通过ID获取技能
    public static SkillDataVo GetSkillDataById(int id)
    {
        if (_skillDic.ContainsKey(id))
        {
            if (_skillDic.ContainsKey(id))
            {
                return _skillDic[id];
            }
            
        }
        return null;
    }
    public static BufferDataVo GetConfigBufferData(int id)
    {
        if (_bufferDic.ContainsKey(id))
        {
            return _bufferDic[id];
        }
        return null;
    }
    
}

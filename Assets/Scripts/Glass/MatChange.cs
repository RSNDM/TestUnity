using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChange : MonoBehaviour {
    [SerializeField] Material material;
    [SerializeField] Texture probe;
 
    string PropertyName = "_Cubemap";
    // Use this for initialization
    void Start () {
        if (material!=null)
        {
            if(DoMatChange(PropertyName))
            {
                Debug.Log("Has");
            }
        }
        Hashtable hashtable = new Hashtable();
        hashtable.Add("神1", "Go1");
        hashtable.Add("神2", "Go2");

        
        CEventDispatcher.GetInstance().DispatchEvent(new CBaseEvent(CEventType.GAME_DATA,hashtable, this.gameObject));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public bool DoMatChange(string name)
    {
        if( material.HasProperty(name))
        {
            //aterial.SetBuffer(name,(ComputeBuffer)probe)
            material.SetTexture(name, probe);
            return true;
        }
        return false;
    }
}

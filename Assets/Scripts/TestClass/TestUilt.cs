using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUilt : MonoBehaviour {
    Text text;
    public string dialogStr = "yiled return 的作用是在执行这段代码之后";
    public float speed = 5f;
    int time=0;
	// Use this for initialization
	void Start () {
        text = transform.GetComponent<Text>();
        StartCoroutine(ShowDialog());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator ShowDialog()
    {
        float timeSum = 0.0f;
        while (text.text.Length<dialogStr.Length    )
        {
            timeSum += speed * Time.deltaTime;
            time++;
            //Debug.Log(time);
            text.text = dialogStr.Substring(0, (int)timeSum);
            yield return null;
        }

    }
    void RayToFindMethods(string Tags)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            if (hit.transform.CompareTag(Tags))
            {
                Debug.Log(hit.transform.name);
            }
        }
    }
    void test()
    {
        float dis=transform.position.sqrMagnitude; 
    }
}

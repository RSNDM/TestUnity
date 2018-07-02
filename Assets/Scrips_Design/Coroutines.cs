using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coroutines : MonoBehaviour {
    int start,cur;
    float time;
	// Use this for initialization
	void Start () {
        start = 0;
        time = Time.time;
        myGirls girls = new myGirls();
        IEnumerator enumerator = girls.GetEnumerator();
        while (enumerator.MoveNext())
        {
            Debug.Log(enumerator.Current);

        }
        //StartCoroutine("myCorotines");
        StartCoroutine("LoadImage");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator myCorotines()
    {
        while (start<10)
        {
            start++;
            float delay = Time.time - time;
            time = Time.time;
            Debug.Log(start);
            Debug.Log("Delay:"+ delay.ToString());
            yield return null;
        }
        Debug.Log("now start is over");
        WaitForSeconds wait = new WaitForSeconds(3);
        yield return wait;
        Debug.Log("over");
    }
    IEnumerator LoadImage()
    {
        WWW www = new WWW("http://pic89.nipic.com/file/20160211/22571617_214730734684_2.jpg");
        int a = 0;
        Image img = this.gameObject.GetComponent<Image>();

        Debug.Log("Before yield return: " + www.url + " is done? " + www.isDone + ", rf=" + Time.renderedFrameCount);
        Debug.Log(a);
        yield return www;

        Debug.Log("After yield return, " + www.url + " is done? " + www.isDone + ", rf=" + Time.renderedFrameCount);
        Rect spriteRect = new Rect(0, 0, www.texture.width, www.texture.height);
        Sprite imageSprite = Sprite.Create(www.texture, spriteRect, new Vector2(0.5f, 0.5f));
        img.sprite = imageSprite;
        
        yield return a = 5;
        Debug.Log(a);
    }
}
public class myGirls : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return "Jacy";
        yield return "Gucci";
        yield return "func";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using UnityEngine.UI;
public class DotweenTest : MonoBehaviour {
    int number = 0;
    private Vector3 mPosVector3;
    //DotweenAnimation
    public RectTransform mRectTransform;
    public Font mFont;
    private bool isFirst = false;
    private bool isSecond = false;

    private Canvas mCanvas;
    private Text mText;
    private Renderer mRenderer;
    private Sequence mSequence;
    private Tweener mTweener;

    //冷却时间
    public float coolingTimer = 2.0f;
    private float currentTime = 0f;
    //
    public Image coolingImage;
    private void Awake()
    {
        mCanvas = transform.GetComponentInParent<Canvas>();
    }
    // Use this for initialization
    void Start () {
        PlayImage();
        currentTime = coolingTimer;
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.localPosition = mPosVector3;
        //用fillamount做cd
        if (currentTime<coolingTimer)
        {
            currentTime += Time.deltaTime;
            coolingImage.fillAmount = currentTime / coolingTimer;
        }
	}
    /// <summary>
    /// 点击播放按钮播放动画
    /// </summary>
    public void OnClick()
    {
        if (isFirst==false)
        {
            mTweener.PlayForward();
            isFirst = true;
        }
        else
        {
            mTweener.PlayBackwards();
            isFirst = false;
        }
        if (mText==null)
        {
            mText = new GameObject("text",typeof(Text)).GetComponent<Text>();
            mText.gameObject.transform.SetParent(mCanvas.gameObject.transform);
            mText.font = mFont;
            mText.rectTransform.anchoredPosition3D = mRectTransform.anchoredPosition3D;
            mText.DOText("Cola Cola", 1f);
            mText.DOFade(1, 1);//(enable value,duration time)
        }
    }
    public void OnGUI()
    {
        if (GUI.Button(new  Rect(10,10,150,100),"SequenceAni"))
        {
            SequenceAni();
        }
    }
    void OnCompletePlay()
    {
        Debug.Log("动画播放完毕");
    }
    void PlayImage()
    {
        mTweener = mRectTransform.DOLocalMove(new Vector3(-6, 88, 0), 0.3f);
        mTweener.Pause();//避免一运行就自动播放
        mTweener.SetAutoKill(false);
        mTweener.OnComplete(OnCompletePlay);
    }
    void SequenceAni()
    {
        mSequence.Append(mTweener).Append(mRectTransform.DOBlendableMoveBy(new Vector3(6,88,0),1.5f));
        if (mText!=null)
        {
           Text tText = new GameObject("tText", typeof(Text)).GetComponent<Text>();
            tText.gameObject.transform.SetParent(mCanvas.gameObject.transform);
            tText.font = mFont;
            tText.rectTransform.anchoredPosition3D = mText.rectTransform.anchoredPosition3D;
            mSequence.Insert(0.3f, tText.DOText("Black Cola Cola",0.3f));
            mSequence.AppendCallback(() => { DestroyImmediate(tText); });
            //mSequence.Insert(0.3f,tText.color.DOColor(0.4f,Color.blue))
        }
        mSequence.AppendInterval(1f).Play();
    }
    void FadeAni()
    {
        mTweener= mRenderer.material.DOFade(1, 1).SetLoops(-1, LoopType.Yoyo);
    }
    //创建dotween实例
    #region 类方法
    private void Vector3Move()
    {
        DOTween.To(() => mPosVector3, x => mPosVector3 = x, new Vector3(5f, 0.5f, 0f), 2);
    }
    #endregion
    private void OnDestroy()
    {
        mTweener.Kill(true);
    }
    private void OnDisable()
    {
        mTweener.Kill(false);

    }
}

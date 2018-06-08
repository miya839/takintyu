using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 薪割りクラス
/// </summary>
public class ChoppingWood : MonoBehaviour
{
    /// <summary>
    /// 薪の数
    /// </summary>
    static public int woodCount = 0;

    public int WoodCount {
        get {
            return woodCount;
        }
        private set {
            woodCount = value;
        }
    }

    /// <summary>
    /// タイミングバー
    /// </summary>
    Slider timingSlider;

    /// <summary>
    /// 薪の数を表示するText
    /// </summary>
    [SerializeField]
    Text woodCountTex;

    /// <summary>
    /// 斧のスプライト
    /// </summary>
    [SerializeField]
    GameObject axe;

    /// <summary>
    /// 割れた薪のPrefab
    /// </summary>
    [SerializeField]
    GameObject brokenWood;

    /// <summary>
    /// スライダーのスピード
    /// </summary>
    const float SPEED = 100.0f;

    /// <summary>
    /// 斧のAnimator
    /// </summary>
    Animation axeAnimation;

    void Start()
    {
        axeAnimation = axe.GetComponent<Animation>();
        timingSlider = this.GetComponent<Slider>();
        woodCountTex.text = "薪 : 0";
        timingSlider.value = 90;
        sliderInit();
    }

    void Update()
    {
        timingSlider.value = Mathf.PingPong(Time.time * SPEED, 100);
    }

    /// <summary>
    /// タイミングスライダーの初期化
    /// </summary>
    void sliderInit()
    {
        var sliderRect = timingSlider.GetComponent<RectTransform>().rect;

        var successObject = GameObject.Find("SuccessArea");
        var sd = successObject.GetComponent<RectTransform>().sizeDelta;
        sd.x = sliderRect.width / 100 * 15;
        sd.y = sliderRect.height / 2;
        successObject.GetComponent<RectTransform>().sizeDelta = sd;
    }

    /// <summary>
    /// 薪を割る
    /// </summary>
    public void chopping()
    {
        // 斧または薪のアニメーションが再生中なら何もしない
        if (brokenWood.GetComponent<Animation>().isPlaying) { return; }
        // 斧のアニメーションを再生
        axeAnimation.Play("Axe");
        // 範囲内であるか？
        if (timingSlider.value >= 40 && timingSlider.value <= 60) {
            ++woodCount;
            // テキストの更新
            woodCountTex.text = "薪 : " + woodCount.ToString();
            // 薪のアニメーション
            brokenWood.GetComponent<Animation>().Play("BrokenWood");
            // 薪割りの音
            SoundManager.Instance.playOverapSE(1);
        }

    }

    /// <summary>
    /// 薪を使用
    /// </summary>
    /// <param name="useNum">使用する薪の数</param>
    public void useWood(int useNum)
    {
        woodCount -= useNum;
        woodCount = Mathf.Clamp(woodCount, 0, int.MaxValue);
        // テキストの更新
        woodCountTex.text = "薪 : " + woodCount.ToString();
    }
}

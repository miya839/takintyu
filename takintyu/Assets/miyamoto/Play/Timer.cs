using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float seconds;
	static private float oldSeconds;
	private Text timerText;

	void Start () {
		oldSeconds = seconds;
		timerText = GetComponentInChildren<Text> ();
	}

	void Update () {
		seconds -= Time.deltaTime;

		//　値が変わった時だけテキストUIを更新
		if((int)seconds != (int)oldSeconds) {
			timerText.text = "残り時間:" + ((int) seconds).ToString ("00");
		}
		oldSeconds = seconds;
	}
}

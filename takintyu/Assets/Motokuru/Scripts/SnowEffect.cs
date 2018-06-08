using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowEffect : SingletonBase<SnowEffect> {

	// Use this for initialization
	void Start () {
		
	}

	Image _Image;

	GameTimer _Timer = new GameTimer(3.0f);

	bool isDisp;

	public void Disp()
	{
		isDisp = true;
		_Timer.Reset();
		gameObject.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		gameObject.SetActive (isDisp);


		if (isDisp == false)
			return;
		
		if (_Timer.Update ()) {
			isDisp = false;
			gameObject.SetActive (false);
		}
	}
}

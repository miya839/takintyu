using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {
	// Update is called once per frame
	public void onClickL(){
		StartController.I.isStart = true;
	}
}

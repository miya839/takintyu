using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivideWoodValue : MonoBehaviour {
	public Text DevideWoodText;
	// Update is called once per frame
	void Update () {
		DevideWoodText.text = "薪の数：" + ChoppingWood.woodCount;
	}
}

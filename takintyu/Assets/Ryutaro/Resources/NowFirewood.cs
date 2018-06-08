using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowFirewood : MonoBehaviour {

	private Text FirewoodText;
	private int FirewoodNum;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		this.FirewoodText = this.GetComponent<Text>();
		this.FirewoodNum = GameObject.Find("FireManagement").GetComponent<FireManagement>().Firewood;
		this.FirewoodText.text = "薪の数：" + this.FirewoodNum;
	}
}

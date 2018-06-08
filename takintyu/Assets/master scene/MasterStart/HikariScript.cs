using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HikariScript : MonoBehaviour {
	public GameObject controller;
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			controller.GetComponent<StartController> ().RiceNum = 0;
		}
		if (Input.GetMouseButtonDown(1)) {
			controller.GetComponent<StartController> ().RiceNum = 0;
		}
	}
}

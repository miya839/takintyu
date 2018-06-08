using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using motokuru;


public class PlayController : MonoBehaviour {

	public GameObject Timer;
	public GameObject Finish;
	public int threshold = 1000;
	static public bool isSuccess = false;
	static public bool isFinish = false;
	private float time;
	private float wait = 5;

	private bool sideChange = false; 
	//false: Be Left 
	//true : Be Right


	void Awake()
	{
		isFinish = false;
	}

	// Update is called once per frame
	void Update () {
		time = Timer.GetComponent<Timer>().seconds;
		if(time <= 0.0f){
			Timer.SetActive (false);
			Finish.SetActive (true);
			isFinish = true;
			if (motokuru.KamadoManager.Score > threshold)
				isSuccess = true; 
			wait -= Time.deltaTime;
			if (wait <= 0.0f) {
				SceneManager.LoadScene ("MasterResult"); 
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			if (!sideChange) {
				var pos = Camera.main.transform.position;
				pos.x = 500.0f;
				Camera.main.transform.position = pos;
				sideChange = !sideChange;
			} else {
				var pos = Camera.main.transform.position;
				pos.x = 0.0f;
				Camera.main.transform.position = pos;
				sideChange = !sideChange;
			}
		}
	}
}

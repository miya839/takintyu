using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultController : SingletonBase<ResultController> {
	public GameObject success;
	public GameObject miss; 
	public bool isReset = false;
	// Update is called once per frame
	void Update () {
		if (PlayController.isSuccess) {
			success.SetActive(true);
		} else {
			miss.SetActive(true);
		}
		if (isReset) {
			PlayController.isSuccess = false;
			success.SetActive (false);
			miss.SetActive (false);
			SceneManager.LoadScene ("RankingScene");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingMaster : SingletonBase<RankingMaster> {
	public bool moveStart = false;
	
	// Update is called once per frame
	void Update () {
		if (moveStart) {
			SceneManager.LoadScene ("MasterStart");
		}
	}
}

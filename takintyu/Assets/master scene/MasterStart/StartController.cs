using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : SingletonBase<StartController> {
	// Update is called once per frame
	private enum Rice{Akitakomachi,Koshihikari,Kapcommai};
	public int RiceNum = -1;
	public int water = -1;
	public bool isStart = false;
	public bool moveRank = false;
 
	public GameObject caution;

	void Update(){
		//if (RiceNum == -1 && water == -1 && !isStart) {
			
		//} else {
		if(isStart){
			SceneManager.LoadScene ("MasterPlay");
		}
		if (moveRank) {
			SceneManager.LoadScene ("RankingScene");
		}

	}

}

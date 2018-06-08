using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankButton : MonoBehaviour {
	public void onClick(){
		StartController.I.moveRank = true;
	}
}

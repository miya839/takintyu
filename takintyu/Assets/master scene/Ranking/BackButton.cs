﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {
	public void onClickL(){
		RankingMaster.I.moveStart = true;
	}
}

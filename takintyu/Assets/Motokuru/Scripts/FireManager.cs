using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour 
{
	[SerializeField]
	List<GameObject> _FireList = new List<GameObject>();

	public void SetNum(int num)
	{
		var len = _FireList.Count;
		for (var i = 0; i <len; i++) {
			var isActive = i < num;
			_FireList [i].SetActive (isActive);
		}
	}

	[ContextMenu("SetFireListForEditor")]
	void SetFireListForEditor()
	{
		_FireList.Clear ();
		foreach (Transform child in transform) {
			_FireList.Add (child.gameObject);
		}
	}
}

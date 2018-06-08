using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakiManager : MonoBehaviour {

	[SerializeField]
	List<GameObject> _MakiList = new List<GameObject>();

	public void SetNum(int num)
	{
		var len = _MakiList.Count;
		for (var i = 0; i <len; i++) {
			var isActive = i < num;
			_MakiList [i].SetActive (isActive);
		}
	}

	[ContextMenu("SetMakiListForEditor")]
	void SetMakiListForEditor()
	{
		_MakiList.Clear ();
		foreach (Transform child in transform) {
			_MakiList.Add (child.gameObject);
		}
	}
}

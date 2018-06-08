using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
			var pos = Camera.main.transform.position;
			pos.x = 0.0f;
			Camera.main.transform.position = pos;
		}
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			var pos = Camera.main.transform.position;
			pos.x = 500.0f;
			Camera.main.transform.position = pos;
        }
    }

	public void Right()
	{
		var pos = Camera.main.transform.position;
		pos.x = 500.0f;
		Camera.main.transform.position = pos;
	}

	public void Left()
	{

		var pos = Camera.main.transform.position;
		pos.x = 0.0f;
		Camera.main.transform.position = pos;
	}
}

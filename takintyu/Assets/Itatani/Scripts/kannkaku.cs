using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kannkaku : MonoBehaviour {
    //図る
    public float rainStart = 10;
    //カウント
    private float rainTime;
    private float destroytime;
    public float checkDestroyTime;

    public GameObject water;
    public Vector3 pos;

	void Start(){
		rainTime = rainStart;
	}

	void Update () {
        rainTime += Time.deltaTime;
        if (rainTime >= rainStart)
        {
            GameObject obj = Instantiate(water);
            obj.transform.position = pos;
            rainTime = 0;
        }
        destroytime += Time.deltaTime;
        if(destroytime >= checkDestroyTime)
        {
            Destroy(this.gameObject);
        }


    }
}

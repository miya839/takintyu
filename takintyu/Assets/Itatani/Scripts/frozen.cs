using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class frozen : MonoBehaviour
{
	public Vector3 moveVolume;


    void Update()
    {
		transform.position -= moveVolume * Time.deltaTime;
    }
    public void OnCollisionEnter2D(Collision2D col)
    {

        //雪があたるとパラメータが減る,雨を消す   
        if (col.gameObject.tag == "kamado")
        {
            //減る
			FindObjectOfType<FireManagement>().ModelateFire(-0.4f);
            //消す
            Destroy(this.gameObject);

        }

    }
}

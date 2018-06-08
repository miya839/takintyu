using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain : MonoBehaviour
{
    void Update()
    {
        transform.position -= new Vector3(0,0.1f,0);
        
    }

     public void OnCollisionEnter2D(Collision2D col)
    {

        //雨があたるとパラメータが減る,雨を消す   
            if (col.gameObject.tag == "kamado")
            {
            //減る

			FindObjectOfType<FireManagement>().ModelateFire(-10.0f);
            //消す
            Destroy(this.gameObject);
            //もう一度
           
        }
        

    }
}
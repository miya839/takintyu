using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dorobou : MonoBehaviour {
    bool doro = true;
    float emerge;
    float doroCount;
	bool isDeath;

	GameTimer _Timer = new GameTimer(0.1f);

	void Update () {
		if (!_Timer.Update ())
			return;

		if (isDeath) {
			transform.position += new Vector3(-128, 0, 0) * Time.deltaTime;
			return;
		}

        if(doro == true)
        {
			transform.position += new Vector3(64, 0, 0) * Time.deltaTime;
        } else {
			transform.position += new Vector3(-64f, 0, 0) * Time.deltaTime * 2.0f;
            //位置が初期位置（8,0,0）以降に戻った時
            if(this.transform.localPosition.x <= -168){
				doroCount ++;
				if(doroCount >= 2)
				{
					isDeath = true;
					return;
				}
                var localScale = transform.localScale;
                localScale.x = 50;
                transform.localScale = localScale;
                doro = true;
			}
		}
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "kamado")
        {
			var localScale = transform.localScale;
			localScale.x = -50;
			transform.localScale = localScale;

            doro = false;


			FindObjectOfType<FireManagement>().ReducedFireWood(1);
        }
    }


}

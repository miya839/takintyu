using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventRandam : MonoBehaviour
{
    //eventが発生する時間.何秒にするかは要相談
    public float Start;
    //時間を図る
    private float eventTime;
    //イベントを実施したカウント
    float count;
    //イベントをランダムで選ぶ
    int eventType;
    GameObject obj;
    //雨の時間間隔
    public float rainTime;

    public GameObject rain;
    public GameObject snow;
    public GameObject doro;
    bool isplay = true;

	List<int> RandomTable = new List<int>(){1,3,1,2};

    public void Play()
    {
        isplay = true;
    }
    public void Stop()
    {
        isplay = false;
    }

	void Awake()
	{
		// ランダムテーブルシャッフルする
		for (var i = 0; i < 100; i++) {
			var a = Random.Range(0,RandomTable.Count);
			var b = Random.Range(0,RandomTable.Count);
			var tmp = RandomTable [a];
			RandomTable [a] = RandomTable [b];
			RandomTable [b] = tmp;
		}

		SnowEffect.I.gameObject.SetActive (false);

	}

	int EventCount = 0;

    void Update()
    {
        if (isplay == false)
        {
            return;
        }

        eventTime += Time.deltaTime;//時間図る

        //イベントを発生させる
        if (eventTime >= Start)
        {

			// 参照外エラーを回避するため
			if (EventCount >= RandomTable.Count)
				return;

            //ランダムにイベントを選ぶ
			eventType = RandomTable[EventCount];
            //Random.Range(0, 4);

			EventCount++;

            switch (eventType)
            {
                case 3://store→薪泥棒、薪の数が減る
                    obj = Instantiate(doro);
                    break;

                case 2://rain→バーを減らす
                    obj = Instantiate(rain);
                    break;

                case 1://frozen→バーを減らす
                    obj = Instantiate(snow);
					SnowEffect.I.Disp();
					
                    break;

                case 0:

                    break;

            }

            //戻す?
            eventTime = 0.0f;
        }
    }
}





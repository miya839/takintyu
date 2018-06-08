using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace motokuru
{
	public class KamadoManager : SingletonBase<KamadoManager> 
	{
		//----------------------------------
		// 定数
		//----------------------------------
		/// <summary>
		/// かまどの状態
		/// </summary>
		public enum Status
		{
			None,
			Bad,
			Good,
		}

		//----------------------------------
		// public static 変数
		//----------------------------------
		public static int Score = 0;

		//----------------------------------
		// 変数
		//----------------------------------
		[SerializeField]
		Kamado _Kamado;
		[SerializeField]
		FireManager _Fire;
		[SerializeField]
		MakiManager _Maki;

		float _FireCount = 0.0f;

		[SerializeField]
		float ScoreRate = 100.0f;

		//----------------------------------
		// Unity関数
		//----------------------------------
		private void Awake()
		{
			Score = 0;
		}

        private void Start()
        {
            SoundManager.Instance.playBGM(0);
        }

        private void OnGUI()
		{
			GUI.Label(new Rect(30,30,1000,1000),"<size=100><color=red>" + Score + "</color></size>");
		}

		//----------------------------------
		// public 関数
		//----------------------------------
		public void SetMaki(int num)
		{
			// 終了していたら更新しない.
			if (PlayController.isFinish)
				return;
			
			_Maki.SetNum(num);
		}

		public void SetFire(int num)
		{
			// 終了していたら更新しない.
			if (PlayController.isFinish)
				return;

			_Fire.SetNum(num);
		}

		public void SetKamadoJudge(bool ok)
		{
			// 終了していたら更新しない.
			if (PlayController.isFinish)
				return;

			if (ok) {
				_Kamado.Special ();
				_FireCount += ScoreRate * Time.deltaTime;
				Score = (int)_FireCount;
			}
			else
				_Kamado.Taku ();
		}

		public void SetKamadoStatus(Status status)
		{
			switch (status) {
			case Status.None:
				_Kamado.Stop ();
				return;
			case Status.Bad:
				_Kamado.Taku ();
				return;
			case Status.Good:
				_Kamado.Special ();
				return;
			}
		}

	}
}

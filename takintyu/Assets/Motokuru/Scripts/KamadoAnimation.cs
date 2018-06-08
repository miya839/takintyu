using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace motokuru
{
	public class KamadoAnimation : MonoBehaviour 
	{
		//---------------------------------
		// 変数
		//---------------------------------
		GameTimer _Timer = new GameTimer(1.0f);

		[SerializeField]
		AnimationCurve _AnimationCurve;

		[SerializeField]
		AnimationCurve _RotAnimationCurve;

		private float _Speed = 1.0f;



		//---------------------------------
		// Unity関数
		//---------------------------------
		void Start () 
		{
			
		}

		void Update () 
		{
			_Timer.Update(_Speed);
			UpdateScale ();
			UpdateRotation ();

			if (_Timer.IsTimeUp()) {
				_Timer.Reset ();
			}
		}

		//---------------------------------
		// public 関数
		//---------------------------------
		public void SetSpeed(float speed)
		{
			_Speed = speed;
		}

		//---------------------------------
		// private 関数
		//---------------------------------
		void UpdateScale()
		{
			var rate = _AnimationCurve.Evaluate (_Timer.timeRate);
			transform.localScale = Vector3.one * rate * 1.75f;
		}


		void UpdateRotation()
		{
			var rate = _RotAnimationCurve.Evaluate (_Timer.timeRate);
			var rot = transform.eulerAngles;
			rot.z = rate * 360.0f;
			transform.eulerAngles = rot;
		}
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace motokuru
{
	public class Kamado : MonoBehaviour 
	{
		//---------------------------------
		// 定数
		//---------------------------------
		enum State
		{
			Init,
			Wait,
			Taku,
			SpecialTaku,
		}


		//---------------------------------
		// private 変数
		//---------------------------------
		StateMachine<State> _StateMachine = new StateMachine<State>();

		[SerializeField]
		KamadoAnimation _Animation;
		//---------------------------------
		// Unity関数
		//---------------------------------
		void Start () 
		{
			SetupState ();
		}

		void Update () 
		{
			// ステートマシンを用意したがまだ使わない.
			//_StateMachine.UpdateState ();
		}

		//---------------------------------
		// public 関数
		//---------------------------------
		public void Stop(){
			_Animation.SetSpeed (0.0f);
		}

		public void Taku(){
			_Animation.SetSpeed (0.1f);
		}

		public void Special(){
			_Animation.SetSpeed (1.0f);
		}

		//---------------------------------
		// state 系 関数
		//---------------------------------
		private void SetupState()
		{
			SetupStateInit ();
			SetupStateWait ();
			SetupStateTaku ();
			SetupStateSpecialTaku ();
			_StateMachine.SetupState (this, State.Init);
		}

		private void SetupStateInit()
		{
			var state = State.Init;
			Action enter = () => {
			};
			Action update = () => {
			};
			Action exit = () => {
			};
			_StateMachine.Add (state,enter,update,exit);
		}

		private void SetupStateWait()
		{
			var state = State.Wait;
			Action enter = () => {
				_Animation.SetSpeed(0);
			};
			Action update = () => {
			};
			Action exit = () => {
			};
			_StateMachine.Add (state,enter,update,exit);
		}

		private void SetupStateTaku()
		{
			var state = State.Taku;
			Action enter = () => {
				_Animation.SetSpeed(0.1f);
			};
			Action update = () => {
			};
			Action exit = () => {
			};
			_StateMachine.Add (state,enter,update,exit);
		}

		private void SetupStateSpecialTaku()
		{
			var state = State.SpecialTaku;
			Action enter = () => {
				_Animation.SetSpeed(1.0f);
			};
			Action update = () => {
			};
			Action exit = () => {
			};
			_StateMachine.Add (state,enter,update,exit);
		}
		//---------------------------------
		// private 関数
		//---------------------------------

	}
}

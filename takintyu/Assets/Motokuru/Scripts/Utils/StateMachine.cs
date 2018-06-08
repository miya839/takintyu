using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// ステートのセットアップの関数用の属性
/// </summary>
public class SetupStateAttribute : Attribute { }

/// <summary>
/// ステートマシン
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class StateMachine<T> where T : IConvertible
{
	/// <summary>
	/// ステート
	/// </summary>
	[Serializable]
	private class State
	{
		public T _Value;
		private Action<T> _Enter = null;
		private Action _Update = null;
		private Action<T> _Exit = null;

		public State(T state = default(T), Action<T> enter = null, Action update = null, Action<T> exit = null)
		{
			_Value = state;
			_Enter = enter ?? delegate { };
			_Update = update ?? delegate { };
			_Exit = exit ?? delegate { };
		}

		public void Enter(T preState)
		{
			_Enter(preState);
		}

		public void Update()
		{
			_Update();
		}

		public void Exit(T nextState)
		{
			_Exit(nextState);
		}

		public T GetValue()
		{
			return _Value;
		}
	}

	private Dictionary<T, State> _StateDic = new Dictionary<T, State>();
	private State _CurrentState = default(State);
	private Action _ReturnParentStateAction = null;
	private bool _IsFromChild = false;
	private bool _IsOneFrameLate = false;
	private T NextOneFrameState;
#pragma warning disable 0414
	public string _StateName
	{
		get;
		private set;
	}

	public void Add(T state, Action enter = null, Action update = null, Action exit = null)
	{
		Action<T> argEnter = (s) => enter();
		Action<T> argExit = (s) => exit();
		Add(state, argEnter, update, argExit);
	}

	public void Add(T state, Action<T> enter, Action update, Action<T> exit)
	{
		var st = new State(state, enter, update, exit);
		_StateDic.Add(state, st);
	}

	public void Add(T state, Action enter, Action update, Action<T> exit)
	{
		Action<T> argEnter = (s) => enter();
		Add(state, argEnter, update, exit);
	}

	public void Add(T state, Action<T> enter, Action update, Action exit)
	{
		Action<T> argExit = (s) => exit();
		Add(state, enter, update, argExit);
	}

	public void UpdateState()
	{
		if (_IsOneFrameLate)
		{
			_IsOneFrameLate = false;
			Set(NextOneFrameState);
			return;
		}
		_CurrentState.Update();
	}

	public void SetupState<U>(U instance, T nextState)
    {
        var methods = typeof(U).GetMethods();
        foreach (var m in methods)
        {
            var a = m.GetCustomAttributes(typeof(SetupStateAttribute),false);
            if (a.Length != 0)
            {
                m.Invoke(instance, null);
            }
        }

		_CurrentState = _StateDic[nextState];
		_StateName = nextState.ToString();
		_CurrentState.Enter(nextState);
    }



	/// <summary>
	/// ステートをセットする
	/// enter は 呼ばれますが exit は 呼ばれません
	/// 初期化以外での使用はお勧めできません
	/// ステートの変更にはSetではなくChangeをお使いください
	/// </summary>
	/// <param name="nextState"></param>
	/// <param name="isEnter"></param>
	private void Set(T nextState, bool isEnter = true)
	{
		_CurrentState = _StateDic[nextState];
		_StateName = nextState.ToString();
		if (isEnter)
		{
			_CurrentState.Enter(nextState);
		}
	}

	/// <summary>
	/// ステートを変更する
	/// enter と exit 共に呼ばれます
	/// ステートの変更にはSetではなくChangeをお使いください
	/// </summary>
	/// <param name="nextState"></param>
	/// <param name="isEnter"></param>
	/// <param name="isExit"></param>
	public void Change(T nextState,bool isEnter = true, bool isExit = true)
	{
		if (isExit)
		{
			_CurrentState.Exit(nextState);
		}
		Set(nextState, isEnter);
	}

	public void ChangeOneFrameLate(T nextState)
	{
		_IsOneFrameLate = true;
		NextOneFrameState = nextState;
		_CurrentState.Exit(nextState);
	}

	/// <summary>
	/// 現在のステートから現在のステートに変更する
	/// exit と enter が 呼ばれます
	/// </summary>
	/// <param name="isEnter"></param>
	/// <param name="isExit"></param>
	public void ReloadState(bool isEnter = true, bool isExit = true)
	{
		Change(_CurrentState._Value, isEnter, isExit);
	}

	public T GetState()
	{
		if (_CurrentState == null) { return default(T); }
		return _CurrentState._Value;
	}

	//////////////////////////////////////////////
	/// Node系関数
	//////////////////////////////////////////////
	public void AddNode<U>(T state, T returnState, StateMachine<U> sub, U openState) where U : IConvertible
	{
		Action enter = () =>
		{
			sub.Set(openState);
		};
		Action update = () =>
		{
			sub.UpdateState();
		};
		Action exit = () =>
		{
		};
		this.Add(state, enter, update, exit);

		Action returnParent = () =>
		{
			this.ChangeFromChild(returnState);
		};
		sub.SetReturnParentAction(returnParent);
	}

	public void AddNode<U>(T state, StateMachine<U> sub, U openState, U closeState) where U : IConvertible
	{
		AddNode<U>(state, state, sub, openState, closeState);
	}

	public void AddNode<U>(T state, T returnState, StateMachine<U> sub, U openState, U closeState) where U : IConvertible
	{
		Action enter = () =>
		{
			sub.Set(openState);
		};
		Action update = () =>
		{
			sub.UpdateState();
		};
		Action exit = () =>
		{
			sub.Change(closeState);
		};
		this.Add(state, enter, update, exit);

		Action returnParent = () =>
		{
			this.ChangeFromChild(returnState);
		};
		sub.SetReturnParentAction(returnParent);
	}

	public void ReturnParent()
	{
		if (_ReturnParentStateAction == null)
		{
			UnityEngine.Debug.LogWarning("親に戻るアクションが設定されていません");
		}
		_ReturnParentStateAction();
	}

	private void SetReturnParentAction(Action action)
	{
		_ReturnParentStateAction = action;
	}

	private void ChangeFromChild(T nextState)
	{
		_IsFromChild = true;
		Change(nextState);
		_IsFromChild = false;
	}

	/// <summary>
	/// 子供から帰ってきたかどうか
	/// </summary>
	/// <returns></returns>
	public bool IsFromChild()
	{
		return _IsFromChild;
	}
}
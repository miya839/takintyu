
////////////////////////////
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
	static private T _Instance;

	static public T I
	{
		get
		{
			if (_Instance == null)
			{
				var c = FindObjectsOfType<T>();
				 if(c.Length == 1)
				{
					_Instance = c[0];
				}
				else
				{
					//Debug.LogError(typeof(T).Name + "が複数存在します。");
				}
			}
			return _Instance;
		}
	}
}
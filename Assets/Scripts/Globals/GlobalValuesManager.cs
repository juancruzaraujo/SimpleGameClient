using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValuesManager<T> : MonoBehaviour where T : GlobalValuesManager<T>
{

	private static T _mInstance;
	protected static GlobalValuesManager<T> GlobalValuesManagerInstance
	{
		get
		{
			if (!_mInstance)
			{
				T[] managers = GameObject.FindObjectsOfType(typeof(T)) as T[];

				if (managers.Length != 0)
				{
					if (managers.Length == 1)
					{
						_mInstance = managers[0];
						_mInstance.gameObject.name = typeof(T).Name;
						return _mInstance;
					}
					else
					{
						//Debug.LogError("You have more than one " + typeof(T).Name + " in the scene. You only need 1, it's a singleton!");
						foreach (T manager in managers)
						{
							Destroy(manager.gameObject);
						}
					}
				}
				GameObject gO = new GameObject(typeof(T).Name, typeof(T));
				_mInstance = gO.GetComponent<T>();
				DontDestroyOnLoad(gO);
			}
			return _mInstance;
		}
		set
		{
			_mInstance = value as T;
		}
	}
}

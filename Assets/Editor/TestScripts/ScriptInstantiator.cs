using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

/**
 * Instantiates a script for testing.
 * Taken from example on (http://www.gamasutra.com/view/news/164363/Indepth_Unit_testing_in_Unity.php)
 */
public class ScriptInstantiator
{
	private List<GameObject> GameObjects { get; set; }

	public ScriptInstantiator()
	{
		GameObjects = new List<GameObject>();
	}

	public T InstantiateScript<T>() where T : MonoBehaviour
	{
		GameObject gameObject;
		object prefab = Resources.Load("Prefabs/" + typeof(T).Name);

		// If there is no prefab with the same name, just use an empty object
		if (prefab == null)
		{
			gameObject = new GameObject();
		}
		else
		{
			gameObject = GameObject.Instantiate(Resources.Load("Prefabs/"
				+ typeof(T).Name)) as GameObject;
		}

		gameObject.name = typeof(T).Name + " (Test)";

		// Prefabs should already have the component
		T inst = gameObject.GetComponent<T>();

		if (inst == null)
		{
			inst = gameObject.AddComponent<T>();
		}

		// Call the start method to initialize the object
		MethodInfo startMethod = typeof(T).GetMethod("Start");

		if (startMethod != null)
		{
			startMethod.Invoke(inst, null);
		}
		GameObjects.Add(gameObject);

		return inst;
	}

	public void CleanUp()
	{
		foreach (GameObject gameObject in GameObjects)
		{
			// Destroy() does not work in edit mode
			GameObject.DestroyImmediate(gameObject);
		}
		GameObjects.Clear();
	}
}

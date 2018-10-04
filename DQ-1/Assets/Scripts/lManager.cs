using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class lManager {

	public static List<GameObject> _ddolObjs = new List<GameObject>();

	public static void DontDestroyOnLoad(this GameObject go) {
		UnityEngine.Object.DontDestroyOnLoad (go);
		_ddolObjs.Add (go);
	}

	public static void DestroyAll() {
		foreach (var go in _ddolObjs) {
			if (go != null) {
				UnityEngine.Object.Destroy (go);
			}

			_ddolObjs.Clear ();
		}
	}


}

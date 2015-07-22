using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

/*
[CustomEditor(typeof(ResourceManager))]
public class ResoureceManagerEditor : Editor {

	GameObject add;

	public override void OnInspectorGUI ()
	{
		ResourceManager target_obj = target as ResourceManager;

		//表示
		for(int i = 0; i < target_obj.HierarkyList.Count; i++) {
			GameObject obj = target_obj.HierarkyList [i];
	
			if (obj != null)
				EditorGUILayout.LabelField ("list", obj.name);
			else {
				target_obj.HierarkyList.RemoveAt (i);
			}
		}

		if (GUILayout.Button ("Clear List")) {
			target_obj.HierarkyList.Clear ();
		}

		add = EditorGUILayout.ObjectField ("Add Hierarchy Object", add,typeof(GameObject), true)as GameObject;
		if (GUILayout.Button ("Add")) {
			target_obj.HierarkyList.Add (add);
			add = null;
		}


		add = EditorGUILayout.ObjectField ("Add Hierarchy Object", null,typeof(GameObject), true)as GameObject;

		if (add != null) {
			Debug.Log ("In" + add.name);
			target_obj.HierarkyList.Add (add);
		}

	}

}
*/
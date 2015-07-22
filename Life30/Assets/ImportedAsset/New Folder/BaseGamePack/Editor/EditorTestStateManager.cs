using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestStateManager))]
public class EditorTestStateManager : Editor {

	public TestStateManager instance;

	public override void OnInspectorGUI ()
	{
		instance = target as TestStateManager;

		GUIStyle StringStyle = new GUIStyle();
		StringStyle.alignment = TextAnchor.MiddleCenter;
		StringStyle.fontSize = 15;

		//現在の登録さている状態を表示
		EditorGUILayout.LabelField("---Current States---",StringStyle);
		EditorGUILayout.Space();

		base.OnInspectorGUI();

		//状態を追加
		if(GUILayout.Button("Add New State")){
			AddStateWindow window = (AddStateWindow)EditorWindow.GetWindow (typeof(AddStateWindow));
			window.position = new Rect(500,250,250,200);
			window.ShowUtility();
		}

		EditorGUILayout.LabelField(name);
	}

}

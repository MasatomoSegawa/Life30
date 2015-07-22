using UnityEngine;
using UnityEditor;

public class mySceneEdit : EditorWindow
{

	string SceneName;
	string ManagersPath;
	string ScriptsPath;
	string GameStatesPath;
	string StateInitScriptsName;
	string StatePlayScriptsName;
	string StateEndScriptsName;
	StateManager scripts;
	bool flag = false;

	[MenuItem ("Custom/Add BaseGameSystem")]
	public static void Create ()
	{
		mySceneEdit window = (mySceneEdit)EditorWindow.GetWindow (typeof(mySceneEdit));
		window.position = new Rect (500, 250, 250, 100);
		window.ShowUtility ();
	}

	void OnGUI ()
	{

		EditorGUILayout.HelpBox ("ゲームシステムを追加します。任意の名前を入力", MessageType.Info, true);
		SceneName = EditorGUILayout.TextArea (SceneName);

		if (flag == false && GUILayout.Button ("Create!") && SceneName != null) {
			CreateFolder ();
			CreatePrefabs ();
			flag = true;
		}

		if (flag == true) {
			if (GUILayout.Button ("End!")) {
				/*scripts.State_Initalizer.AddComponent (StateInitScriptsName);
				scripts.State_Play.AddComponent (StatePlayScriptsName);
				scripts.State_End.AddComponent (StateEndScriptsName);*/
				this.Close ();
			}
		}

	}

	void CreatePrefabs ()
	{
	
		GameObject StateManager = Instantiate (Resources.Load ("BaseGamePack/StateManager"))as GameObject;
		StateManager.name = SceneName + "_GameManager";
		StateManager.AddComponent<ResourceManager> ();
		StateManager.AddComponent<SoundManager> ();
		StateManager.AddComponent<TransitionManager> ();
		StateManager.AddComponent<FadeManager> ();
		StateManager.AddComponent<GameModel> ();

		TransitionManager TM = StateManager.GetComponent<TransitionManager> ();
		GameObject EffectClone = Resources.Load ("BaseGamePack/TransitionEffect")as GameObject;
		TM.EffectObj = EffectClone;

		scripts = StateManager.GetComponent<StateManager> ();

		StateInitScriptsName = SceneName + "_Initialize";
		StatePlayScriptsName = SceneName + "_Play";
		StateEndScriptsName = SceneName + "_End";
	
		GenerateCode (StateInitScriptsName);
		GenerateCode (StatePlayScriptsName);
		GenerateCode (StateEndScriptsName);

		GameObject obj1 = new GameObject (StateInitScriptsName);
		GameObject obj2 = new GameObject (StatePlayScriptsName);
		GameObject obj3 = new GameObject (StateEndScriptsName);

		/*
		scripts.State_Initalizer = PrefabUtility.CreatePrefab (GameStatesPath + "/" + StateInitScriptsName + ".prefab", obj1);
		scripts.State_Play = PrefabUtility.CreatePrefab (GameStatesPath + "/" + StatePlayScriptsName + ".prefab", obj2);
		scripts.State_End = PrefabUtility.CreatePrefab (GameStatesPath + "/" + StateEndScriptsName + ".prefab", obj3);
		*/

		DestroyImmediate (obj1);
		DestroyImmediate (obj2);
		DestroyImmediate (obj3);

		PrefabUtility.CreatePrefab (ManagersPath + "/" + SceneName + "_StateManager.prefab", StateManager);
		AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);
	}

	void CreateFolder ()
	{

		string id = AssetDatabase.CreateFolder ("Assets", SceneName + "_BaseGameSystem");
		string newFolderPath = AssetDatabase.GUIDToAssetPath (id);

		ManagersPath = AssetDatabase.GUIDToAssetPath (AssetDatabase.CreateFolder (newFolderPath, "Managers"));
		GameStatesPath = AssetDatabase.GUIDToAssetPath (AssetDatabase.CreateFolder (newFolderPath, "GameStates"));
		ScriptsPath = AssetDatabase.GUIDToAssetPath (AssetDatabase.CreateFolder (newFolderPath, "Scripts"));

		string ResourcesId = AssetDatabase.GUIDToAssetPath (AssetDatabase.CreateFolder (newFolderPath, "Resources"));
		AssetDatabase.CreateFolder (ResourcesId, "PrefabResources");

		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh ();

	}

	void GenerateCode (string name)
	{
		
		System.Text.StringBuilder builder = new System.Text.StringBuilder ();
		builder.AppendLine ("using UnityEngine;");
		builder.AppendLine ("using System.Collections;");
		builder.AppendLine ("");
		builder.AppendLine ("public class " + name + " : State {");
		builder.AppendLine ("");
		builder.AppendLine ("public override void StateStart ()");
		builder.AppendLine ("{");
		builder.AppendLine ("	Debug.Log (this.ToString() + \" Start!\");");
		builder.AppendLine ("");
		builder.AppendLine ("}");
		builder.AppendLine ("");
		builder.AppendLine ("public override void StateUpdate ()");
		builder.AppendLine ("{");
		builder.AppendLine ("");	
		builder.AppendLine ("	if(Input.GetKeyDown(KeyCode.A)){");
		builder.AppendLine ("		this.EndState();");
		builder.AppendLine ("	}");
		builder.AppendLine ("	");
		builder.AppendLine ("}");
		builder.AppendLine ("");
		builder.AppendLine ("public override void StateDestroy ()");
		builder.AppendLine ("{");
		builder.AppendLine ("	Debug.Log (this.ToString() + \" Destroy!\");");
		builder.AppendLine ("}");
		builder.AppendLine ("");
		builder.AppendLine ("}");
		
		System.IO.File.WriteAllText (ScriptsPath + "/" + name + ".cs", builder.ToString (), System.Text.Encoding.UTF8);
		AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);
		
	}
}

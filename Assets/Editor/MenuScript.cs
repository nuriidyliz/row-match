using UnityEditor;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

	[MenuItem("GameObject/RM_UI/RMButton", false, 10)]
	static void CreateRMButton(MenuCommand menuCommand)
	{
		GameObject go = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/UI Components/RMButton.prefab", typeof(GameObject));
		go = PrefabUtility.InstantiatePrefab(go) as GameObject;
		PrefabUtility.UnpackPrefabInstance(go, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);

		GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
		Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);

		Selection.activeObject = go;
	}

	[MenuItem("GameObject/RM_UI/RMPopup", false, 10)]
	static void CreatePopup(MenuCommand menuCommand)
	{
		GameObject go = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/UI Components/RMButton.prefab", typeof(GameObject));
		go = PrefabUtility.InstantiatePrefab(go) as GameObject;
		PrefabUtility.UnpackPrefabInstance(go, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);

		GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
		Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);

		Selection.activeObject = go;
	}
}

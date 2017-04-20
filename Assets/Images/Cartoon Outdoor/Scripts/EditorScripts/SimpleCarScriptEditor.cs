using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SimpleCarScript))]
[CanEditMultipleObjects]
public class SimpleCarScriptEditor : Editor {

	void OnEnable() {
	}
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI();

		if(GUILayout.Button("Set Current Pos as Start Point")) {

			foreach(SimpleCarScript obj in targets) {
				obj.startPoint = (Vector3)obj.GetComponent<Transform>().position;
			}
		}

		if(GUILayout.Button("Set Current Pos as End Point")) {
			foreach(SimpleCarScript obj in targets) {
				obj.endPoint = (Vector3)obj.GetComponent<Transform>().position;
			}
		}

	}
}
#endif

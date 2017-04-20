using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SimplePlantGrowing))]
[CanEditMultipleObjects]
public class SimplePlantGrowingEditor : Editor {
	[SerializeField]
	private SimplePlantGrowing plantGrowing;

	void OnEnable() {
		plantGrowing = (SimplePlantGrowing)target;
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		//int newLevel = EditorGUILayout.IntField ("Growing Level", plantGrowing.GetCurrentGrowingState ());
		int newLevel = EditorGUILayout.IntSlider ("Growing Level", plantGrowing.GetCurrentGrowingLevel(), 0, plantGrowing.PlantGrowingStages.Length);
		if(GUI.changed) {
			if(newLevel != plantGrowing.GetCurrentGrowingLevel())
				plantGrowing.SetCurrentGrowingLevel (newLevel);
		}

		if (GUILayout.Button ("Grow to Next Level >")) {
			plantGrowing.GrowToNextLevel();
		}

		EditorUtility.SetDirty (plantGrowing);
	}
}

#endif

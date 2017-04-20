using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SimplePlantGrowing : MonoBehaviour {
	
	public GameObject[] PlantGrowingStages; //Each stage corresponds to a gameobject which is showed on the empty plowed ground. At each stages the corresponding gameobject will be made instantiated and make child of the main object, and the others will be removed

	[SerializeField][HideInInspector]
	private int CurrentGrowingLevel;
	private Transform growRoot;

	void Awake() {
		growRoot = transform.FindChild ("GrowRoot"); //The GrowRoot Empty object is the root which will contains the instantiated grown plant levels
	}
	// Use this for initialization
	void Start () {
		growRoot = transform.FindChild ("GrowRoot");
	}
	
	// Update is called once per frame
	void Update () {
	}

	public int GetCurrentGrowingLevel() 
	{
		return CurrentGrowingLevel;
	}

	public void SetCurrentGrowingLevel(int state) 
	{
		CurrentGrowingLevel = state;
		refreshGrowingState();
	}

	//Remove all the instantiated plant stages
	private void removeAllTheInstantiatedPlants() {
		//Remove the old instantiated game objects (to hide the old growing level), which are all the children of the GrowRoot Empty object
		for(int i=0;i<growRoot.childCount;i++)
		{
			GameObject grownPlant = growRoot.GetChild(i).gameObject;
			if(grownPlant != null)
				GameObject.DestroyImmediate(grownPlant);
		}
	}

	//instantiate the gameobject in the PlantGrowingStages array, corresponding to the CurrentGrowingLevel, and remove the others.
	private void refreshGrowingState() {

		if(CurrentGrowingLevel == 0) //Level 0 is the base level (empty plowed ground, so remove all the children plants and leave the plowed ground only
			removeAllTheInstantiatedPlants();
		else {
			//Get the prefab in the PlantGrowingStage array at the CurrentGrowingLevel
			GameObject curStagePrefab = PlantGrowingStages[CurrentGrowingLevel-1];
			if(curStagePrefab != null) {
				removeAllTheInstantiatedPlants(); //remove the previous instantiated plants from the last growing level changes

				//Instantiate the prefab and set it as child of the GrowRoot Empty (which is a child of the main prefab), moving it at 0,0,0 on the GrowRoot local coordinates.
				GameObject instantiatedGameObj = (GameObject)GameObject.Instantiate(curStagePrefab);
				instantiatedGameObj.transform.parent = growRoot; //Make child of the main obj's grow root
				instantiatedGameObj.transform.localPosition = Vector3.zero;

			}
		}
	}


	//Grow the crop to the next stage
	//Return trues if the plant has reach the full grown stage, false otherwise.
	public bool GrowToNextLevel() {

		if (CurrentGrowingLevel >= 0 && CurrentGrowingLevel-1 < PlantGrowingStages.Length - 1) {
			CurrentGrowingLevel++;

			refreshGrowingState();
		}

		if(CurrentGrowingLevel-1 == PlantGrowingStages.Length -1) { //The plant has reach the full grown state
			return true;
		}
		else
			return false;
	}

	//Return true if the crops has reach it's full grown state (last level), false otherwise
	public bool IsFullGrown() {
		if(CurrentGrowingLevel-1 == PlantGrowingStages.Length -1) { //The plant has reach the full grown state
			return true;
		}
		else
			return false;
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class uGUIHealthController : MonoBehaviour {

    private List<Image> HealthSprites;
	public List<Image> healthSprites {
        get {
            if(HealthSprites == null) {
                HealthSprites = new List<Image>();
                GameObject[] sprites = GameObject.FindGameObjectsWithTag("HealthSprite");
                foreach(GameObject obj in sprites) {
                    HealthSprites.Add(obj.GetComponent<Image>());
                }
            }
            return HealthSprites;
        }
        set { HealthSprites = value; }
    }

    private List<Image> ManaSprites;
	public List<Image> manaSprites {
        get {
            if(ManaSprites == null) {
                ManaSprites = new List<Image>();
                GameObject[] sprites = GameObject.FindGameObjectsWithTag("ManaSprite");
                foreach(GameObject obj in sprites) {
                    ManaSprites.Add(obj.GetComponent<Image>());
                }
            }
            return ManaSprites;
        }
        set { ManaSprites = value; }
    }

	public PlayerHealth player { get; set; }

    private static uGUIHealthController instance;
    public static uGUIHealthController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<uGUIHealthController> ();
                if (instance == null) {
                    GameObject obj = new GameObject ();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    instance = obj.AddComponent<uGUIHealthController> ();
                }
            }
            return instance;
        }
    }

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}

	public void UpdateVitals() {
		for(int i = 0; i < healthSprites.Count; i++) {
			healthSprites[i].fillAmount = player.curHealth/player.maxHealth;
		}
		for(int i = 0; i < manaSprites.Count; i++) {
			manaSprites[i].fillAmount = player.curMana/player.maxMana;
		}
	}
}

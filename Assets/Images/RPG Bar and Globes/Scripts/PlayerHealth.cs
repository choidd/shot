using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    private float CurHealth;
	public float curHealth {
        get { return CurHealth; }
        set {
        	CurHealth = value;
            if(CurHealth > maxHealth) {
                CurHealth = maxHealth;
            }
            if(CurHealth < 0) {
                Die();
                curHealth = 0;
            }
        }
    }

    [SerializeField]
    private float MaxHealth;
	public float maxHealth {
        get { return MaxHealth; }
        set { MaxHealth = value; }
    }

    private float CurMana;
	public float curMana {
        get { return CurMana; }
        set {
        	CurMana = value;
            if(CurMana > maxMana) {
                CurMana = maxMana;
            }
            if(CurMana < 0) {
                CurMana = 0;
            }
        }
    }

    [SerializeField]
    private float MaxMana;
	public float maxMana {
        get { return MaxMana; }
        set { MaxMana = value; }
    }

    [SerializeField]
    private float HealthRegenSpeed;
	public float healthRegenSpeed {
        get { return HealthRegenSpeed; }
        set { HealthRegenSpeed = value; }
    }

    [SerializeField]
    private float ManaRegenSpeed;
	public float manaRegenSpeed {
        get { return ManaRegenSpeed; }
        set { ManaRegenSpeed = value; }
    }


	// Use this for initialization
	void Start () {
        curHealth = maxHealth;
        curMana = maxMana;
	}
	
	// Update is called once per frame
	void Update () {

		curHealth += Time.deltaTime * healthRegenSpeed;

		curMana += Time.deltaTime * manaRegenSpeed;

		uGUIHealthController.Instance.UpdateVitals();
	}

	public void CastSpell() {
		curMana -= Random.Range(10,20);
        uGUIHealthController.Instance.UpdateVitals();
	}

	public void TakeDamage() {
		curHealth -= Random.Range(10,20);
        uGUIHealthController.Instance.UpdateVitals();
	}

    /*
    Put your player die code in this function
     */
    public void Die() {
        Debug.Log("Player died.");
    }
}

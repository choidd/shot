using UnityEngine;
using System.Collections;

public class AttackPlayer : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.name);
        if (coll.name.Equals("Enemy"))
        {

        }
    }
}

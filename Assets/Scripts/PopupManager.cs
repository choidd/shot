using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour {

	public static PopupManager Instance { set; get; }

    private Transform PopupBackground;

    static private PopupManager instance;

    void Start()
    {
        instance = this;
    }

    public void showPopup(string ObjName)
    {
        PopupBackground = GameObject.Find(ObjName).transform;
        StartCoroutine(StartShowPopup());
    }

    public IEnumerator StartShowPopup()
    {
        float timeStart = Time.time;
        while (true)
        {
            float timePassed = Time.time - timeStart;
            float rate = timePassed / 0.5f;

            PopupBackground.localPosition = new Vector3(0f, 1500f - 1500f * rate, 0f);

            if (timePassed > 0.5f)
            {
                PopupBackground.localPosition = new Vector3(0f, 0f, 0f);
                break;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
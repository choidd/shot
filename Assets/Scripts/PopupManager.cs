using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour {

	public static PopupManager Instance { set; get; }

    public CanvasGroup canvasGroup;
    private Transform uiroot;
    public void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

    }

    public void showPopup(string title, string message)
    {
        if(uiroot == null)
        {
            uiroot = GameObject.FindGameObjectWithTag("UIRoot").transform;

            transform.SetParent(uiroot);

            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
    }

    public void OnClick()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        transform.SetParent(uiroot.parent);
    }
}

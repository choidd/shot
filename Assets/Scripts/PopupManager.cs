using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour {

    public static PopupManager Instance
    {
        get { return instance; }
        set { }
    }
    static private PopupManager instance;

    public CanvasGroup canvasGroup;
    public Text titleText;
    public Text descriptionText;

    private Transform uiRoot;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    
    public void ShowPopup(string title, string message)
    {
        Debug.Log("call show popup");
        if (uiRoot == null)
            uiRoot = GameObject.FindGameObjectWithTag("UIRoot").transform;

        transform.SetParent(uiRoot);

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        titleText.text = title;
        descriptionText.text = message;

    }

    public void OnClick()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        transform.SetParent(uiRoot.parent);
    }
}
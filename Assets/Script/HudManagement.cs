using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HudManagement : MonoBehaviour
{
    [SerializeField]
    Image panelMovementCanvas;
    [SerializeField]
    GameObject playerObj;
    [SerializeField]
    ClickMovement playerScript;
    [SerializeField]
    Image panelMouseImage;
    [SerializeField]
    GameObject guiFeed;
    [SerializeField]
    TextMeshProUGUI guiFeedText;

    void Start()
    {
        playerObj = GameObject.Find("Player");
        playerScript = playerObj.GetComponent<ClickMovement>();
        panelMouseImage = gameObject.GetComponentInChildren<Image>();
        guiFeed = GameObject.Find("TextoUI");
        guiFeedText = guiFeed.GetComponent<TextMeshProUGUI>();


    }
    void Update()
    {
        panelMethod();
        guiFeedText.text = playerScript.nomeTileMap;
    }
    void panelMethod()
    {
        float panelWidth = panelMovementCanvas.rectTransform.sizeDelta.x;
        float panelHeight = panelMovementCanvas.rectTransform.sizeDelta.y;
        panelMovementCanvas.transform.position = new Vector2(Input.mousePosition.x + (panelWidth / 2f) + 20f,
        Input.mousePosition.y + (panelHeight / 2f) + 20f);
    }
}
   
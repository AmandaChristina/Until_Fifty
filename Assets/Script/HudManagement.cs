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
        

        if (!playerScript.isVisible)
        {
            //panelMouseImage.enabled = false;
            playerScript.tile.SetActive(false);
            guiFeedText.text = "Não posso\nme mexer!";
            //guiFeedText.enabled = false;

        }
        else
        {
            panelMouseImage.enabled = true;
            playerScript.tile.SetActive(true);
            guiFeedText.enabled = true;
            //guiFeedText.text = "" + playerScript.custoTotal + "\n" + playerScript.textTileMap;
            guiFeedText.text = 
                "\n"+ playerScript.textTileMap+ "" +
                "\nMover" +
                "Energia: " + playerScript.lances + "/50" +
                "\nCusto: " + playerScript.custoTotal;
        }
    }
    void panelMethod()
    {
        float panelWidth = panelMovementCanvas.rectTransform.sizeDelta.x;
        float panelHeight = panelMovementCanvas.rectTransform.sizeDelta.y;
        panelMovementCanvas.transform.position = new Vector2(Input.mousePosition.x + (panelWidth / 2f) + 20f,
        Input.mousePosition.y + (panelHeight / 2f) + 20f);
    }
}
   
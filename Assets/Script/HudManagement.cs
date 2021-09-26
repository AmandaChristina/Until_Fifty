using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudManagement : MonoBehaviour
{
    public Image panelMovementCanvas;

    void Update()
    {
        panelMethod();
    }
    void panelMethod()
    {
        float panelWidth = panelMovementCanvas.rectTransform.sizeDelta.x;
        float panelHeight = panelMovementCanvas.rectTransform.sizeDelta.y;
        panelMovementCanvas.transform.position = new Vector2(Input.mousePosition.x + (panelWidth / 2f) + 20f,
            Input.mousePosition.y + (panelHeight / 2f) + 20f);
    }
}
   
using TMPro;
using UnityEngine;

public class DynamicTextBoundary : MonoBehaviour
{
    public TMP_Text textComponent;
    public RectTransform panelRect;

    void Update()
    {
        if (textComponent != null && panelRect != null)
        {
            float textHeight = textComponent.preferredHeight;
            float currentHeight = panelRect.rect.height;
            float scaleRatio = (textHeight / currentHeight) * 2.1f;
            Vector3 currentScale = panelRect.localScale;
            panelRect.localScale = new Vector3(currentScale.x, scaleRatio * currentHeight, currentScale.z);
        }
    }
}
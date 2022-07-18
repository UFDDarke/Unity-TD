using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text header;
    public Text content;
    public Text footer;

    [SerializeField] private GameObject popupCanvasObject;
    [SerializeField] public RectTransform popupObject;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float padding;
    


    private Canvas popupCanvas;
    [SerializeField] public Canvas masterCanvas;

    // Update is called once per frame
    void Update()
    {
        FollowCursor();
    }

	private void Awake()
	{
        popupCanvas = popupCanvasObject.GetComponent<Canvas>();
        HidePopup();
	}

	public void FollowCursor()
	{
        // TODO: Follows the cursor, but stays constrained within screen bounds
        if(!popupCanvasObject.activeSelf)
		{
            return;
		}

        Vector3 newPos = Input.mousePosition + offset;
        newPos.z = 0f;
        float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * masterCanvas.scaleFactor / 2) - padding;
        if (rightEdgeToScreenEdgeDistance < 0)
        {
            newPos.x += rightEdgeToScreenEdgeDistance;
        }
        float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * masterCanvas.scaleFactor / 2) + padding;
        if (leftEdgeToScreenEdgeDistance > 0)
        {
            newPos.x += leftEdgeToScreenEdgeDistance;
        }
        float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * masterCanvas.scaleFactor) - padding;
        if (topEdgeToScreenEdgeDistance < 0)
        {
            newPos.y += topEdgeToScreenEdgeDistance;
        }
        popupObject.transform.position = newPos;
    }

    public void DisplayInfo(string _header, string _content, string _footer)
	{
        header.text = _header;
        content.text = _content;
        footer.text = _footer;

        popupCanvasObject.SetActive(true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
	}

    public void HidePopup()
	{
        // TODO: Hides the tooltip if nothing is being hovered.
        popupCanvasObject.SetActive(false);
	}

}

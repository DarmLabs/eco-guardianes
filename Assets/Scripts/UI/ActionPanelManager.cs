using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionPanelManager : MonoBehaviour
{
    public Button[] actionButtons;
    PlayerMovement playerMovement;
    [HideInInspector] public bool isOpened;
    void Awake()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    public void SetListeners(Transform target, bool mode)
    {
        foreach (var actionButton in actionButtons)
        {
            actionButton.onClick.RemoveAllListeners();
            if (!mode)
            {
                actionButton.onClick.AddListener(delegate { playerMovement.TravelToDestination(target); });
            }
            actionButton.onClick.AddListener(DisableActionPanel);
        }
    }
    public void ClampToWindow(Vector3 MyMouse, RectTransform panelRectTransform, RectTransform parentRectTransform)
    {
        panelRectTransform.transform.position = MyMouse;
        Vector3 pos = panelRectTransform.localPosition;
        Vector3 minPosition = parentRectTransform.rect.min - panelRectTransform.rect.min;
        Vector3 maxPosition = parentRectTransform.rect.max - panelRectTransform.rect.max;
        pos.x = Mathf.Clamp(panelRectTransform.localPosition.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(panelRectTransform.localPosition.y, minPosition.y, maxPosition.y);
        panelRectTransform.localPosition = pos;
    }
    public void DisableActionPanel(){
        isOpened = false;
        gameObject.SetActive(false);
    }
}

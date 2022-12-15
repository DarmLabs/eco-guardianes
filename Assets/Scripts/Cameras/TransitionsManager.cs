using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TransitionsManager : MonoBehaviour
{
    [Header("Needed GameObjects & Others")]
    [SerializeField] GameObject viewCamera;
    [SerializeField] Cinemachine.CinemachineVirtualCamera playerCamera;
    [SerializeField] Cinemachine.CinemachineBrain brain;
    Camera mainCamera;
    LayerMask playerLayer = 7;
    int oldMask;
    bool isFromInsideObject;
    public static TransitionsManager SharedInstance;
    [Header("Changeable Variables")]
    [SerializeField] Vector3 objOffset;
    [SerializeField] Vector3 trashCanOffset;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        mainCamera = brain.GetComponent<Camera>();
    }
    public void ViewAction(InteractableObjectBase targetObject)
    {
        PlayerMovement.SharedInstance.enabled = false;
        Cinemachine.CinemachineVirtualCamera virtualCamera = viewCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();

        Vector3 cameraPosition = new Vector3();
        Transform lookAt = null;

        ObjectType targetType = targetObject.Type;
        switch (targetType)
        {
            case ObjectType.Closed:
                InteractableContainer targetContainer = null;
                targetContainer = targetObject.GetComponent<InteractableContainer>();
                cameraPosition = targetContainer.ViewPoint.position;
                lookAt = targetContainer.LookAt;
                isFromInsideObject = true;
                break;
            case ObjectType.Open:
                lookAt = targetObject.LookAt;
                cameraPosition = targetObject.transform.position + objOffset;
                break;
            case ObjectType.TrashCan:
                lookAt = targetObject.transform;
                cameraPosition = targetObject.transform.position + trashCanOffset;
                break;
        }

        viewCamera.transform.position = cameraPosition;
        virtualCamera.LookAt = lookAt;

        playerCamera.m_Priority = 9;
        StartCoroutine(WaitForViewTransition());
    }
    public void CloseViewAction()
    {
        playerCamera.m_Priority = 11;
        mainCamera.cullingMask = oldMask;
        if (isFromInsideObject)
        {
            StartCoroutine(WaitForPlayerTransitionFromInsideObject());
            isFromInsideObject = false;
        }
        else
        {
            StartCoroutine(WaitForPlayerTransitionFromActionPanel());
        }
    }
    IEnumerator WaitForViewTransition()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => brain.IsBlending == false);
        ActionPanelManager.SharedInstance.EnableInfo();
        oldMask = mainCamera.cullingMask;
        mainCamera.cullingMask &= ~(1 << playerLayer);
    }
    IEnumerator WaitForPlayerTransitionFromActionPanel()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => brain.IsBlending == false);
        PlayerMovement.SharedInstance.enabled = true;
        ActionPanelManager.SharedInstance.EnableActionPanel(null);
    }
    IEnumerator WaitForPlayerTransitionFromInsideObject()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => brain.IsBlending == false);
        PlayerMovement.SharedInstance.enabled = true;
        ActionPanelManager.SharedInstance.SearchIntoPanelSwitcher(true);
    }
}

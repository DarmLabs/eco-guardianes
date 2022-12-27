using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TransitionsManager : MonoBehaviour
{
    [Header("Needed GameObjects & Others")]
    [SerializeField] GameObject viewCamera;
    Cinemachine.CinemachineVirtualCamera virtualCamera;
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
        virtualCamera = viewCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }
    public void ViewAction(InteractableObjectBase targetObject, bool isFromInsideObject = false)
    {
        PointAndClickMovement.SharedInstance.enabled = false;

        ObjectType targetType = targetObject.Type;
        switch (targetType)
        {
            case ObjectType.Closed:
                InteractableContainer targetContainer = targetObject.GetComponent<InteractableContainer>();
                SetCamera(targetContainer.LookAt, targetContainer.ViewPoint.position);
                break;
            case ObjectType.Open:
                InteractableObject targetOpenObj = targetObject.GetComponent<InteractableObject>();
                SetCamera(targetObject.LookAt, targetObject.transform.position + targetOpenObj.ViewOffset);
                this.isFromInsideObject = isFromInsideObject;
                break;
            case ObjectType.TrashCan:
                SetCamera(targetObject.LookAt, targetObject.transform.position + trashCanOffset);
                break;
        }

        playerCamera.m_Priority = 9;
        StartCoroutine(WaitForViewTransition());
    }
    void SetCamera(Transform lookAt, Vector3 position)
    {
        viewCamera.transform.position = position;
        virtualCamera.LookAt = lookAt;
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
        PointAndClickMovement.SharedInstance.enabled = true;
        ActionPanelManager.SharedInstance.EnableActionPanel(null);
    }
    IEnumerator WaitForPlayerTransitionFromInsideObject()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => brain.IsBlending == false);
        PointAndClickMovement.SharedInstance.enabled = true;
        ActionPanelManager.SharedInstance.SearchIntoPanelSwitcher(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TransitionsManager : MonoBehaviour
{
    public static TransitionsManager SharedInstance;
    [Header("Needed GameObjects & Others")]
    Camera mainCamera;
    [SerializeField] Cinemachine.CinemachineVirtualCamera viewCamera;
    [SerializeField] Cinemachine.CinemachineVirtualCamera playerCamera;
    [SerializeField] Cinemachine.CinemachineBrain brain;
    LayerMask playerLayer = 7;
    int oldMask;
    bool isFromInsideObject;

    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        mainCamera = brain.GetComponent<Camera>();
    }
    void SetPosAndLookAtFromType(InteractableObjectBase targetObject)
    {
        ObjectType targetType = targetObject.Type;
        switch (targetType)
        {
            case ObjectType.Closed:
                InteractableContainer targetContainer = targetObject.GetComponent<InteractableContainer>();
                SetCameraPosAndLookAt(targetContainer.LookAt, targetContainer.ViewPoint.position);
                break;
            case ObjectType.Open:
                InteractableObject targetOpenObj = targetObject.GetComponent<InteractableObject>();
                SetCameraPosAndLookAt(targetObject.LookAt, targetObject.transform.position + targetOpenObj.ViewOffset);

                break;
            case ObjectType.TrashCan:
                SetCameraPosAndLookAt(targetObject.LookAt, targetObject.transform.position);
                break;
        }
    }
    public void HintAction(InteractableObjectBase targetObject, bool isFromInsideObject = false)
    {
        this.isFromInsideObject = isFromInsideObject;
        SetPosAndLookAtFromType(targetObject);
        StartCoroutine(ViewTransition());
    }
    void SetCameraPosAndLookAt(Transform lookAt, Vector3 position)
    {
        viewCamera.transform.position = position;
        viewCamera.LookAt = lookAt;
    }
    public void CloseHintAction()
    {
        mainCamera.cullingMask = oldMask;
        if (isFromInsideObject)
        {
            StartCoroutine(PlayerTransitionFromInsideObject());
            isFromInsideObject = false;
        }
        else
        {
            StartCoroutine(PlayerTransitionFromActionPanel());
        }
    }

    //General transitioner
    IEnumerator WaitForTransition(Cinemachine.CinemachineVirtualCamera vCamera, int priority)
    {
        vCamera.m_Priority = priority;
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => brain.IsBlending == false);
    }
    //Especific transitioners
    IEnumerator ViewTransition()
    {
        yield return WaitForTransition(playerCamera, 9);
        ActionPanelManager.SharedInstance.EnableInfo();
        oldMask = mainCamera.cullingMask;
        mainCamera.cullingMask &= ~(1 << playerLayer);
    }
    IEnumerator PlayerTransitionFromActionPanel()
    {
        yield return WaitForTransition(playerCamera, 11);
        PointAndClickMovement.SharedInstance.enabled = true;
        ActionPanelManager.SharedInstance.EnableActionPanel(null);
    }
    IEnumerator PlayerTransitionFromInsideObject()
    {
        yield return WaitForTransition(playerCamera, 11);
        PointAndClickMovement.SharedInstance.enabled = true;
        ActionPanelManager.SharedInstance.SearchIntoPanelSwitcher(true);
    }
}

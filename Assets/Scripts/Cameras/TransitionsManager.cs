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
        InteractableContainer targetContainer = null;
        if (targetObject.GetComponent<InteractableContainer>() != null)
        {
            targetContainer = targetObject.GetComponent<InteractableContainer>();
        }
        PlayerMovement.SharedInstance.enabled = false;
        Cinemachine.CinemachineVirtualCamera virtualCamera = viewCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        if (targetObject.OptionalLookAt != null && targetContainer != null) //Used for open objects inside closed ones
        {
            virtualCamera.LookAt = targetObject.OptionalLookAt;
            viewCamera.transform.position = targetContainer.ViewPoint.position;
            isFromInsideObject = true;
        }
        else //Used for open objects outside of closed ones
        {
            virtualCamera.LookAt = targetObject.transform;
            if (targetObject.Type == ObjectType.TrashCan)
            {
                viewCamera.transform.position = targetObject.transform.position + trashCanOffset;
            }
            else
            {
                viewCamera.transform.position = targetObject.transform.position + objOffset;
            }
            isFromInsideObject = false;
        }
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

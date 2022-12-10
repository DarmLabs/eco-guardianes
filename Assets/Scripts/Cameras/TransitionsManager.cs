using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TransitionsManager : MonoBehaviour
{
    [Header("Needed GameObjects & Others")]
    [SerializeField] GameObject closeView;
    [SerializeField] GameObject viewCamera;
    [SerializeField] Cinemachine.CinemachineVirtualCamera playerCamera;
    [SerializeField] Cinemachine.CinemachineBrain brain;
    Camera mainCamera;
    LayerMask playerLayer = 7;
    int oldMask;
    public static TransitionsManager SharedInstance;
    [Header("Changeable Variables")]
    [SerializeField] Vector3 offset;
    bool viewTransition;
    bool playerTransitions;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        mainCamera = brain.GetComponent<Camera>();
    }
    void FixedUpdate()
    {
        CheckTransitions();
    }
    void CheckTransitions()
    {
        if (viewTransition && !closeView.activeSelf)
        {
            WaitForViewTransition();
        }
        if (playerTransitions)
        {
            WaitForPlayerTransition();
        }
    }
    public void ViewAction(InteractableObjectBase targetObject)
    {
        PlayerMovement.SharedInstance.enabled = false;
        Cinemachine.CinemachineVirtualCamera virtualCamera = viewCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        if (targetObject.OptionalLookAt != null)
        {
            virtualCamera.LookAt = targetObject.OptionalLookAt;
            viewCamera.transform.position = targetObject.OptionalLookAt.position + offset;
        }
        else
        {
            virtualCamera.LookAt = targetObject.transform;
            viewCamera.transform.position = targetObject.transform.position + offset;
        }
        playerCamera.m_Priority = 9;
        viewTransition = true;
    }
    public void CloseViewAction()
    {
        playerCamera.m_Priority = 11;
        mainCamera.cullingMask = oldMask;
        playerTransitions = true;
    }
    void WaitForViewTransition()
    {
        if (!brain.IsBlending)
        {
            ActionPanelManager.SharedInstance.EnableInfo();
            oldMask = mainCamera.cullingMask;
            mainCamera.cullingMask &= ~(1 << playerLayer);
            viewTransition = false;
        }
    }
    void WaitForPlayerTransition()
    {
        if (!brain.IsBlending)
        {
            PlayerMovement.SharedInstance.enabled = true;
            playerTransitions = false;
            ActionPanelManager.SharedInstance.EnableActionPanel(null);
        }
    }
}

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
    public static TransitionsManager SharedInstance;
    [Header("Changeable Variables")]
    [SerializeField] Vector3 offset;
    bool viewTransition;
    bool playerTransitions;
    GameObject targetObject;
    void Awake()
    {
        SharedInstance = this;
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
    public void ViewAction(GameObject targetObject)
    {
        this.targetObject = targetObject;
        InteractableObject targetIO = targetObject.GetComponent<InteractableObject>();
        PlayerMovement.SharedInstance.enabled = false;
        Cinemachine.CinemachineVirtualCamera virtualCamera = viewCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        if (targetIO.OptionalLookAt != null)
        {
            virtualCamera.LookAt = targetIO.OptionalLookAt;
            viewCamera.transform.position = targetIO.OptionalLookAt.position + offset;
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
        playerTransitions = true;
    }
    void WaitForViewTransition()
    {
        if (!brain.IsBlending)
        {
            ActionPanelManager.SharedInstance.EnableInfo();
            viewTransition = false;
        }
    }
    void WaitForPlayerTransition()
    {
        if (!brain.IsBlending)
        {
            PlayerMovement.SharedInstance.enabled = true;
            playerTransitions = false;
            ActionPanelManager.SharedInstance.EnableActionPanel();
        }
    }
}

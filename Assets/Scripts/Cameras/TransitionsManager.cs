using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TransitionsManager : MonoBehaviour
{
    [Header("Imported Scripts")]
    [SerializeField] PlayerMovement playerMovement;
    [Header("Needed GameObjects & Others")]
    [SerializeField] GameObject closeView;
    [SerializeField] GameObject viewCamera;
    [SerializeField] Cinemachine.CinemachineVirtualCamera playerCamera;
    [SerializeField] Cinemachine.CinemachineBrain brain;
    [Header("Changeable Variables")]
    [SerializeField] Vector3 offset;
    bool viewTransition;
    bool playerTransitions;
    GameObject targetObject;
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
        playerMovement.enabled = false;
        viewCamera.transform.position = targetObject.transform.position + offset;
        Cinemachine.CinemachineVirtualCamera virtualCamera = viewCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        virtualCamera.LookAt = targetObject.transform;
        playerCamera.m_Priority = 9;
        viewTransition = true;
    }
    [SerializeField]
    void CloseViewAction()
    {
        playerCamera.m_Priority = 11;
        playerTransitions = true;
    }
    void WaitForViewTransition()
    {
        if (!brain.IsBlending)
        {
            closeView.SetActive(true);
            viewTransition = false;
        }
    }
    void WaitForPlayerTransition()
    {
        if (!brain.IsBlending)
        {
            playerMovement.enabled = true;
            playerTransitions = false;
            targetObject.GetComponent<InteractableObject>().CanInteract(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] GameObject closeView;
    [SerializeField] GameObject viewCamera;
    [SerializeField] Cinemachine.CinemachineVirtualCamera playerCamera;
    [SerializeField] Cinemachine.CinemachineBrain brain;
    [SerializeField] Vector3 offset;
    bool viewTransition;
    bool playerTransitions;
    GameObject targetObject;
    void Awake()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
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
        playerMovement.enabled = false;
        viewCamera.transform.position = targetObject.transform.position + offset;
        Cinemachine.CinemachineVirtualCamera virtualCamera = viewCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        virtualCamera.LookAt = targetObject.transform;
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
            targetObject.GetComponent<InteractableObject>().canInteract = true;
        }
    }
}

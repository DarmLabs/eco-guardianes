using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerMovement : MonoBehaviour
{
    float speed = 6f;
    Vector3 forward, right, heading, rightMovement, upMovement;
    Rigidbody rb;
    Camera mainCamera;
    [HideInInspector] public bool wallAhed;
    NavMeshAgent navMesh;
    [SerializeField] ActionPanelManager actionPanel;
    [HideInInspector] public bool isMoving;
    void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        navMesh = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        TakeOrientation();
        navMesh.enabled = false;
    }
    void Update()
    {
        Controls();
    }
    void Controls()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            ClearNavMeshPath();
            if (actionPanel.targetObject != null)
            {
                actionPanel.targetObject.GetComponent<InteractableObject>().beingCollected = false;
                actionPanel.targetObject.GetComponent<InteractableObject>().canInteract = true;
                actionPanel.DisableInfoPanel();
                actionPanel.DisableActionPanel();
            }
            if (!wallAhed)
            {
                Movement();
            }
            Rotation();
        }
    }
    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        else
        {
            speed = 6f;
        }
        rb.position += heading;
    }
    void Rotation()
    {
        rightMovement = right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        upMovement = forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
        heading = rightMovement + upMovement;
        transform.rotation = Quaternion.LookRotation(heading);
    }
    public void TakeOrientation()
    {
        if (mainCamera != null)
        {
            forward = mainCamera.gameObject.transform.forward;
            forward.y = 0;
            forward = Vector3.Normalize(forward);
            right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        }
    }
    public void TravelToDestination(Transform target)
    {
        isMoving = true;
        navMesh.enabled = true;
        navMesh.SetDestination(target.position);
    }
    public void ClearNavMeshPath()
    {
        if (navMesh.enabled)
        {
            navMesh.ResetPath();
            navMesh.enabled = false;
            isMoving = false;
        }
    }
}

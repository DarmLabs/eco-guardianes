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
    bool wallAhed;
    NavMeshAgent navMesh;
    [SerializeField] ActionPanelManager actionPanel;
    [HideInInspector] public bool isMoving { get; private set; }
    Animator anim;
    void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        TakeOrientation();
        navMesh.enabled = false;
    }
    void Update()
    {
        Controls();
        ObjectDetector();
        Debug.DrawRay(transform.position + new Vector3(0, -0.6f, 0), transform.forward);
    }
    void Controls()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            ClearNavMeshPath();
            if (actionPanel.targetObject != null)
            {
                actionPanel.targetObject.GetComponent<InteractableObject>().BeingCollected(false);
                actionPanel.targetObject.GetComponent<InteractableObject>().CanInteract(true);
                actionPanel.DisableInfoPanel();
                actionPanel.DisableActionPanel();
            }
            if (!wallAhed)
            {
                Movement();
            }
            Rotation();
        }
        else
        {
            float remainSpeed;
            remainSpeed = anim.GetFloat("speed");
            if (remainSpeed >= 0)
            {
                anim.SetFloat("speed", remainSpeed - 0.5f);
            }
        }
    }
    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8f;
        }
        else
        {
            speed = 5f;
        }
        anim.SetFloat("speed", speed);
        rb.position += heading;
    }
    void Rotation()
    {
        rightMovement = right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        upMovement = forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
        heading = rightMovement + upMovement;
        transform.rotation = Quaternion.LookRotation(heading);
    }
    void TakeOrientation()
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

        InteractableObject targetScript = target.GetComponent<InteractableObject>();
        if (targetScript.isClose)//If it's close to the object, the player will take it
        {
            targetScript.InteractWithObject();
        }
        else //If not, it will travel to take it
        {
            isMoving = true;
            navMesh.enabled = true;
            navMesh.SetDestination(target.position);
        }

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
    void ObjectDetector()
    {
        RaycastHit hit;
        Ray forwardRay = new Ray(transform.position + new Vector3(0, -0.6f, 0), transform.forward);
        if (Physics.Raycast(forwardRay, out hit))
        {
            if (hit.distance < 1 && !hit.collider.isTrigger)
            {
                wallAhed = true;
            }
            else
            {
                wallAhed = false;
            }
        }
        else
        {
            wallAhed = false;
        }
    }
}

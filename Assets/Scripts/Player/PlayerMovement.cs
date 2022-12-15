using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerMovement : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] float runSpeed;
    [Range(0, 10)]
    [SerializeField] float walkSpeed;
    float speed;
    Vector3 forward, right, heading, rightMovement, upMovement;
    Rigidbody rb;
    Camera mainCamera;
    bool wallAhed;
    NavMeshAgent navMesh;
    [HideInInspector] public bool isMoving { get; private set; }
    [Range(0, 1)]
    Animator anim;
    public static PlayerMovement SharedInstance;
    void Awake()
    {
        SharedInstance = this;
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
        //Debug.DrawRay(transform.position + new Vector3(0, -0.6f, 0), transform.forward);
    }
    void Controls()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            ClearNavMeshPath();
            if (ActionPanelManager.SharedInstance.TargetObjectBase != null)
            {
                ActionPanelManager.SharedInstance.TargetObjectBase.BeingTargeted = false;
                ActionPanelManager.SharedInstance.TargetObjectBase.CanInteract(true);
                ActionPanelManager.SharedInstance.DisableInfoPanel();
                ActionPanelManager.SharedInstance.DisableActionPanel();
            }
            if (!wallAhed)
            {
                Movement();
            }
            Rotation();
        }
        else
        {
            if (!isMoving)
            {
                anim.SetFloat("speed", 0);
            }
            if (!isMoving && WheelChairMovement.SharedInstance != null)
            {
                WheelChairMovement.SharedInstance.isMoving = false;
            }
        }
    }
    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            if (WheelChairMovement.SharedInstance != null)
            {
                WheelChairMovement.SharedInstance.isRuning = false;
            }
        }
        else
        {
            speed = walkSpeed;
            if (WheelChairMovement.SharedInstance != null)
            {
                WheelChairMovement.SharedInstance.isRuning = false;
            }
        }
        if (WheelChairMovement.SharedInstance != null)
        {
            WheelChairMovement.SharedInstance.isMoving = true;
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
        InteractableObjectBase targetScript = target.GetComponent<InteractableObjectBase>();
        if (targetScript.IsClose)//If it's close to the object, the player will take it
        {
            targetScript.InteractWithObject();
        }
        else //If not, it will travel to take it
        {
            isMoving = true;
            if (WheelChairMovement.SharedInstance != null)
            {
                WheelChairMovement.SharedInstance.isMoving = true;
            }
            anim.SetFloat("speed", 0.2f);
            navMesh.enabled = true;
            if (targetScript.LookAt != null)
            {
                navMesh.SetDestination(targetScript.LookAt.position);
            }
            else
            {
                navMesh.SetDestination(target.position);
            }

        }

    }
    public void ClearNavMeshPath()
    {
        if (navMesh.enabled)
        {
            navMesh.ResetPath();
            navMesh.enabled = false;
            isMoving = false;
            if (WheelChairMovement.SharedInstance != null)
            {
                WheelChairMovement.SharedInstance.isMoving = false;
            }
            anim.SetFloat("speed", 0);
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

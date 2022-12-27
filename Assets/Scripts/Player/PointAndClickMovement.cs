using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PointAndClickMovement : MonoBehaviour
{
    public static PointAndClickMovement SharedInstance;
    [HideInInspector] public bool isMoving { get; private set; }
    NavMeshAgent navMesh;
    Animator anim;
    [SerializeField] GameObject destinationObj;
    [SerializeField] Vector3 startingDestPos;
    void Awake()
    {
        SharedInstance = this;
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    public void TravelToPoint(Vector3 destination)
    {
        destinationObj.transform.position = destination + Vector3.up * 2;
        navMesh.SetDestination(destination);
        MovingSwithcer(true);
    }
    public void TravelToTrash(Transform target)
    {
        InteractableObjectBase targetScript = target.GetComponent<InteractableObjectBase>();
        if (targetScript.IsClose)//If it's close to the object, the player will take it
        {
            targetScript.InteractWithObject();
        }
        else //If not, it will travel to take it
        {
            MovingSwithcer(true);
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
    void MovingSwithcer(bool state)
    {
        isMoving = state;
        if (WheelChairMovement.SharedInstance != null)
        {
            WheelChairMovement.SharedInstance.isMoving = state;
        }
        anim.SetBool("isMoving", state);
        if (!state)
        {
            destinationObj.transform.position = startingDestPos;
        }
    }
    public void DestinationReached()
    {
        navMesh.ResetPath();
        MovingSwithcer(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == destinationObj)
        {
            DestinationReached();
            destinationObj.transform.position = startingDestPos;
        }
    }
}

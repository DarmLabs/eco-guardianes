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
        NavMeshPath navMeshPath = new NavMeshPath();
        if (navMesh.CalculatePath(destination, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            destinationObj.transform.position = destination;
            navMesh.SetDestination(destination);
            MovingSwithcer(true);
        }
        else
        {
            DialogManager.SharedInstance.SetDialog("No puedo llegar ahí");
        }
    }
    public void TravelToTarget(Transform target)
    {
        InteractableBase targetScript = target.GetComponent<InteractableBase>();
        if (targetScript.IsClose)//If it's close to the object, the player will take it
        {
            LookAt(targetScript);
            targetScript.InteractWithObject();
        }
        else //If not, it will travel to take it
        {
            MovingSwithcer(true);
            navMesh.enabled = true;
            navMesh.SetDestination(target.position);
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
    public IEnumerator LookAt(InteractableBase interactable)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(interactable.transform.position.x - transform.position.x, 0, interactable.transform.position.z - transform.position.z));

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * 2f;

            yield return null;
        }
        interactable.InteractWithObject();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectDetector : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerInteraction playerInteraction;
    GameObject targetObject;
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerInteraction = GetComponentInParent<PlayerInteraction>();
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray forwardRay = new Ray(transform.position, transform.parent.forward);
        if (Physics.Raycast(forwardRay, out hit))
        {
            if (hit.distance < 1 && !hit.collider.isTrigger)
            {
                playerMovement.wallAhed = true;
            }
            else
            {
                playerMovement.wallAhed = false;
            }
        }
        else
        {
            playerMovement.wallAhed = false;
        }
    }
}

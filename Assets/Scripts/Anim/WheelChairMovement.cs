using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WheelChairMovement : MonoBehaviour
{
    public static WheelChairMovement SharedInstance;
    public bool isMoving { get; set; }
    public bool isRuning { get; set; }
    [SerializeField] float rotationPerMinute = 50f;
    void Awake()
    {
        SharedInstance = this;
    }
    void Update()
    {
        if (isMoving)
        {
            RotateWheel();
        }
    }
    public void RotateWheel()
    {
        if (isRuning)
        {
            rotationPerMinute = 70f;
        }
        else
        {
            rotationPerMinute = 50f;
        }
        transform.Rotate(6 * rotationPerMinute * Time.deltaTime, 0, 0);
    }
}

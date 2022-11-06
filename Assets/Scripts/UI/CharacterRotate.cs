using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterRotate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject character;
    [SerializeField] float senstivity;
    [SerializeField] bool isMouseDown;
    void FixedUpdate()
    {
        ApplyRotation();
    }
    void ApplyRotation()
    {
        if (!isMouseDown)
        {
            return;
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            character.transform.Rotate(0, -(Input.GetAxis("Mouse X")) * Time.deltaTime * senstivity, 0);
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            character.transform.Rotate(0, -(Input.GetAxis("Mouse X")) * Time.deltaTime * senstivity, 0);
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        isMouseDown = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        isMouseDown = false;
    }
}

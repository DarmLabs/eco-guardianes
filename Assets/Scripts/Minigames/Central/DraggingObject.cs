using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Outline))]
public class DraggingObject : MonoBehaviour
{
    [SerializeField] SeparatorType type;
    public SeparatorType Type => type;
    GameObject parentObj;
    private Vector3 screenPoint;
    Vector3 curPosition;
    Outline _outline;
    public bool Spawned { get; set; }
    float initialY;
    void Awake()
    {
        _outline = GetComponent<Outline>();
        parentObj = transform.parent.gameObject;
    }
    void Update()
    {
        if (Spawned)
        {
            MoveParent();
        }
    }
    public void RecordInitialY()
    {
        initialY = transform.position.y;
    }
    void MoveParent()
    {
        parentObj.transform.Translate(new Vector3(0, 0, -1 * Time.deltaTime));
    }
    public void ResetToPoint()
    {
        transform.position = new Vector3(curPosition.x, initialY, curPosition.z);
        if (transform.localPosition.x > 1.5 || transform.localPosition.x < -1.3)
        {
            transform.localPosition = new Vector3(0.2f, transform.localPosition.y, transform.localPosition.z);
        }
    }
    public void ResetToPool()
    {
        transform.localPosition = Vector3.zero;
        parentObj.transform.position = Vector3.zero;
    }
    void Glow(bool state)
    {
        _outline.enabled = state;
    }
    void OnMouseEnter()
    {
        Glow(true);
    }
    void OnMouseExit()
    {
        Glow(false);
    }
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        CentralManager.SharedInstance.DraggingObject = this;
    }

    void OnMouseUp()
    {
        CentralManager.SharedInstance.CheckForObjectThrow();
        CentralManager.SharedInstance.DraggingObject = null;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6);

        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = curPosition;

    }
}

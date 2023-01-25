using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSync : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_Player_Position");
    public static int SizeID = Shader.PropertyToID("_Size");
    [SerializeField] Material wallMaterial;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask mask;
    void Update()
    {
        var dir = mainCamera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);

        if (Physics.Raycast(ray, 100, mask))
        {
            wallMaterial.SetFloat(SizeID, 1);
        }
        else
        {
            wallMaterial.SetFloat(SizeID, 0);
        }
        var view = mainCamera.WorldToViewportPoint(transform.position);
        wallMaterial.SetVector(PosID, view);
    }
}

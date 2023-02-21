using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CintaAnim : MonoBehaviour
{
    Vector3 initialPos;
    void Awake()
    {
        initialPos = transform.position;
    }
    void Update()
    {
        transform.Translate(0, 1 * Time.deltaTime, 0);
        if (transform.localPosition.z <= -7.98)
        {
            transform.position = initialPos;
        }
    }
}

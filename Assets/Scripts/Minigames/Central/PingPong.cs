using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    public float speed = 2.3f;
    [SerializeField] private Transform posicionFlecha;
    //private GameObject globalaux;
    //private GlobalVariables gv;

    private void Start() 
    {
       // globalaux = GameObject.Find("GlobalVariables");
       // gv = globalaux.GetComponent<GlobalVariables>();
    }

    public void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, 1) * 6 - 3;
        this.gameObject.transform.position = new Vector3(posicionFlecha.position.x,posicionFlecha.position.y + y,posicionFlecha.position.z);        
    }
}

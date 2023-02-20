using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCinta : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;
    private bool sleeping;
    private bool crece;
    private bool nocrece;
    private bool roto;
    private bool roto1;
    private float fallTime = 0f;
    private Vector3 scaleChange;
    private Vector3 scaleChange1;
     
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        tr = gameObject.GetComponent<Transform>();
    }
    void Start()
    {
        sleeping = false;
        fallTime = 0f;
        crece = false; 
        nocrece = false;
        roto = false; 
        roto1 = false;        
        scaleChange = new Vector3(+0.12f, +0.12f, 0f);
        scaleChange1 = new Vector3(+0.08f, +0.08f, 0f);        

    }    
    void Update()
    {       
        fallTime += Time.deltaTime;
        if (crece)
        {
            tr.localScale = tr.localScale + (scaleChange*Time.deltaTime);            
        }    
        if (nocrece)
        {
            tr.localScale = tr.localScale + (scaleChange1*Time.deltaTime);
        }    
    }
     
    void OnTriggerEnter2D(Collider2D other) 
    {        
        if (other.tag == "Destructor")
        {
            Generador.bloqueaMovimiento = false;
            Destroy(this.gameObject);  
                     
        }
        if (other.tag == "PuntoDireccion")
        {              
            if(other.name == "base")
            {
                if (!roto)
                {
                    tr.Rotate(0.0f, 0.0f, +10f, Space.Self);
                    roto = true;
                }                
                if (fallTime > 1f)
                {               
                    if (sleeping)
                    {
                        rb.WakeUp();                                               
                    }
                    else
                    {
                        rb.Sleep();                
                    }
                    sleeping = !sleeping;            
                    fallTime = 0.00f;
                }
            }            
            if(other.name == "crece")
            {
                if(!roto1)
                {
                    tr.Rotate(0.0f, 0.0f, +5f, Space.Self);
                    roto1 = true;
                }
               
                crece = true;
                nocrece = false;
                
            }
            if(other.name == "nocrece")
            {
                crece = false;
                nocrece=true; 
                Generador.bloqueaMovimiento = true;               
                tr.Rotate(0.0f, 0.0f, +10f, Space.Self);
            }
        }
    }
}

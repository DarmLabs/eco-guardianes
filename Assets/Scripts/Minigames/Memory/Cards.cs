using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public bool facedUp, locked;
    public static bool coroutineAllowed;
    private Cards firstInPair, secondInPair;
    private Cards auxiliar;
    private string firstInPairName, secondInPairName;

    public static Queue<Cards> sequence;
    public static int pairsFound;
    
    [SerializeField] private GameObject PanelVictoria;
    [SerializeField] private GameObject PanelDerrota;
    public Text textCantidadVidas;
    public Text textCantidadPares;        
    
   
    private Collider colisionAux;    
   
    public static int CantidadPares = 9;
    //AudioManager audioManager;
       

    void Start()
    {        
        textCantidadPares.text = "0";
        textCantidadVidas.text = (Grilla.vidas/2).ToString();
        //audioManager = GameObject.FindObjectOfType<AudioManager>(); 
        facedUp=false;
        coroutineAllowed=true;
        locked=false;
        
        sequence=new Queue<Cards>();
        pairsFound=0;               
    }
    
    void Update() 
    {
        if (Grilla.vidas <= 0)
        {
            PanelDerrota.SetActive(true);
            //audioManager.PlayAudio("Lost");            
            Destroy(this.gameObject);
            Time.timeScale = 0f;         
        }
        if (pairsFound == CantidadPares)
        {        
            PanelVictoria.SetActive(true);
            //audioManager.PlayAudio("Win");                
            Destroy(this.gameObject); 
            Time.timeScale = 0f;           
        }
    }

    void  OnMouseDown() 
    {
        //si el movimiento no esta bloqueado se voltea la carta
        if (!locked && coroutineAllowed)
        {
            StartCoroutine(RotateCard());            
        }
        
    }

    public IEnumerator RotateCard()
    {
        
        if(Time.timeScale != 0)
        {
            coroutineAllowed=false;
            if(!facedUp)
            {
                sequence.Enqueue(this);
                colisionAux = this.GetComponent<Collider>();
                colisionAux.enabled = !colisionAux.enabled;                
                for (float i = 0f; i < 190f; i+=10)
                {
                    //audioManager.PlayAudio("Voltear_Sound");
                    transform.rotation = Quaternion.Euler(0f,i,0f);
                    yield return new WaitForSeconds(0.04f* Time.deltaTime);
                }              
                              
            }
            else if (facedUp)
            {
                for (float i = 190f; i >= 0f; i-=10)
                {
                    transform.rotation=Quaternion.Euler(0f,i,0f);
                    yield return new WaitForSeconds(0.04f* Time.deltaTime);
                    sequence.Clear();
                }
                                                
            }
            
            coroutineAllowed=true;        
            facedUp=!facedUp;           

            if(sequence.Count ==2)
            {                                             
                CheckResults();
            }
        }        
    }

    //al dar vuelta un par de cartas se comprueba si tienen el mismo nombre, lo que quiere decir que son pares
    //caso contrario se vuelven a voltear
    void CheckResults()
    {
        firstInPair=sequence.Dequeue();
        secondInPair=sequence.Dequeue();

        firstInPairName= firstInPair.name.Substring(0,firstInPair.name.Length-7);
        secondInPairName= secondInPair.name.Substring(0, secondInPair.name.Length-7);
        
        if (firstInPairName==secondInPairName)
        {
            firstInPair.locked=true;
            secondInPair.locked=true;
            pairsFound+=1; 
            textCantidadPares.text = pairsFound.ToString();          
            if (firstInPair.tag == "Recuperable")
            {
                if (firstInPairName == "VasoVidrio" || firstInPairName== "BotellaVidrio" || firstInPairName == "FrascoVidrio")
                {                    
                    Grilla.vidas = Grilla.vidas +2;
                    textCantidadVidas.text = (Grilla.vidas/2).ToString();                    
                    //audioManager.PlayAudio("Acierto_Sound");
                } 
                if (firstInPairName == "BotellaPlastico" || firstInPairName== "BidonPlastico" || firstInPairName == "CubiertosPlastico")
                {                    
                    Grilla.vidas = Grilla.vidas +2;
                    textCantidadVidas.text = (Grilla.vidas/2).ToString();
                    //audioManager.PlayAudio("Acierto_Sound");                    
                }
                if (firstInPairName == "Diario" || firstInPairName== "CajaCarton" || firstInPairName == "CajaHuevos")
                {
                    
                    Grilla.vidas = Grilla.vidas +2;
                    textCantidadVidas.text = (Grilla.vidas/2).ToString();
                    //audioManager.PlayAudio("Acierto_Sound");

                }       
                if (firstInPairName == "TapaFrasco" || firstInPairName== "LataAluminio")
                {                   
                    Grilla.vidas = Grilla.vidas +2;
                    textCantidadVidas.text = (Grilla.vidas/2).ToString();
                    //audioManager.PlayAudio("Acierto_Sound");
                }        
            }            
            if (firstInPair.tag == "Organico")
            {                
                Grilla.vidas = Grilla.vidas +2; 
                textCantidadVidas.text = (Grilla.vidas/2).ToString();
                //audioManager.PlayAudio("Acierto_Sound");             
            }
        }
        else
        {
            firstInPair.StartCoroutine("RotateBack");
            secondInPair.StartCoroutine("RotateBack");          
            
        }
        /*if (pairsFound == CantidadPares)
        {
               
        }*/


        
    }

    //vuelve a voltear la carta
    public IEnumerator RotateBack()
    {
        coroutineAllowed =false; 
        colisionAux.enabled = !colisionAux.enabled;
        Grilla.vidas = Grilla.vidas -1;
        textCantidadVidas.text = (Grilla.vidas/2).ToString();
        //audioManager.PlayAudio("Error_Sound");  
        yield return new WaitForSeconds(40f* Time.deltaTime);
        for (float i=190f; i>=0f; i-=10)
        {
            //audioManager.PlayAudio("Voltear_Sound");
            transform.rotation =Quaternion.Euler(0f,i,0f);
            yield return new WaitForSeconds(1f* Time.deltaTime);
            sequence.Clear();            
        }
        facedUp=false;
        coroutineAllowed=true;
    }
    
    IEnumerator LateCall(GameObject objeto)
    {
        yield return new WaitForSeconds(50.0f*Time.deltaTime);
        objeto.SetActive(false);
    }
    
    
           
   
}

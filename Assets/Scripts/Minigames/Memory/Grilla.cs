using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grilla : MonoBehaviour
{
    [SerializeField] private GameObject[] UbicacionCartas24;
    [SerializeField] private GameObject[] UbicacionCartas22;
    [SerializeField] private GameObject[] UbicacionCartas20;
    [SerializeField] private GameObject[] UbicacionCartas18;
    [SerializeField] private GameObject[] UbicacionCartas16;
    [SerializeField] private GameObject[] UbicacionCartas14;
    [SerializeField] private GameObject[] UbicacionCartas12;
    [SerializeField] private GameObject[] UbicacionCartas;    
    [SerializeField] private GameObject[] Cartas; //16 distintas
    [SerializeField] private GameObject[] CartasGrilla24;
    [SerializeField] private GameObject[] CartasGrilla22;
    [SerializeField] private GameObject[] CartasGrilla20;
    [SerializeField] private GameObject[] CartasGrilla18;
    [SerializeField] private GameObject[] CartasGrilla16;
    [SerializeField] private GameObject[] CartasGrilla14;
    [SerializeField] private GameObject[] CartasGrilla12;
    [SerializeField] private GameObject[] CartasGrilla;
    //private int indexUbicacionCartas = 17;
    //private int indexCartas = 0;   
    private GameObject globalaux;
    //private GlobalVariables gv; 
    [SerializeField] private GameObject tutorialParte1;

    public static int vidas = 20;    
    //public static int refinadosDestruidos = 0;

    private int randomIndex;
    private int cantidadRandoms = 10;
    List<int> listaRandoms = new List<int>();
    public int auxiliarGrilla = 0;  
    //AudioManager audioManager;

   

    void Start()
    {        
        /*auxiliarGrilla = Random.Range(0,7);
        Debug.Log("auxiliarGrilla"+auxiliarGrilla);
        switch (auxiliarGrilla)
        {
            case 24:
            cantidadRandoms = 13;
            vidas = 60; //30
            Cards.CantidadPares = 12;
            break;
            case 22:
            cantidadRandoms = 12;
            vidas = 50; //25 
            Cards.CantidadPares = 11;
            break;
            case 20:
            cantidadRandoms = 11;
            vidas = 40; //20  
            Cards.CantidadPares = 10;  
            break;
            case 18:
            cantidadRandoms = 10;
            vidas = 30;  //15    
            Cards.CantidadPares = 9; 
            break;
            case 16:
            cantidadRandoms = 9; 
            vidas = 24; //12  
            Cards.CantidadPares = 8;   
            break;
            case 14:
            cantidadRandoms = 8; 
            vidas = 22; //11   
            Cards.CantidadPares = 7;  
            break;
            case 12:
            cantidadRandoms = 7; 
            vidas = 20; //10  
            Cards.CantidadPares = 6;    
            break;        
        }  */        
        globalaux = GameObject.Find("GlobalVariables");
        //gv = globalaux.GetComponent<GlobalVariables>();
        //audioManager = GameObject.FindObjectOfType<AudioManager>();
        Grilla.vidas = 20;    
        
        /*if (gv.tutorialMemoria == true)
        {
            Time.timeScale = 1f;
            tutorialParte1.gameObject.SetActive(false);
            //audioManager.StopMusic();
            //audioManager.PlayMusic("Minigame_Theme"); 
            //Debug.Log("TIEMPO EN   "+Time.timeScale); 
        }
        if (gv.tutorialMemoria == false)
        {
            Time.timeScale = 0f;
            tutorialParte1.gameObject.SetActive(true);            
            //audioManager.StopMusic();
            //audioManager.PlayMusic("Tutorial_Theme");
            //Debug.Log("TIEMPO EN   "+Time.timeScale); 
        } */    
        listaRandoms = new List<int>(new int[cantidadRandoms]); 
        for (int i = 1; i < cantidadRandoms; i++)
        {
            randomIndex = Random.Range(0,17);           
            while (listaRandoms.Contains(randomIndex))
            {
                randomIndex = Random.Range(0,17);
            } 
            listaRandoms[i] = randomIndex;                    
        } 

        /*switch (auxiliarGrilla)
        {
            case 0:
                for (int i = 0; i < 12; i++)
                {
                    CartasGrilla24[i] = Cartas[listaRandoms[i+1]];                   
                    CartasGrilla24[i+12] = Cartas[listaRandoms[i+1]];         
                }
                for (int t = 0; t < CartasGrilla.Length; t++)
                {
                    GameObject temporal = CartasGrilla24[t]; 
                    int r = Random.Range(t, CartasGrilla24.Length);
                    CartasGrilla24[t] = CartasGrilla24[r];
                    CartasGrilla24[r] = temporal;
                }
            break;
            case 1:
                for (int i = 0; i < 11; i++)
                {
                    CartasGrilla22[i] = Cartas[listaRandoms[i+1]];                   
                    CartasGrilla22[i+11] = Cartas[listaRandoms[i+1]];         
                }
                for (int t = 0; t < CartasGrilla.Length; t++)
                {
                    GameObject temporal = CartasGrilla22[t]; 
                    int r = Random.Range(t, CartasGrilla22.Length);
                    CartasGrilla22[t] = CartasGrilla22[r];
                    CartasGrilla22[r] = temporal;
                }
            break;
            case 2:
                for (int i = 0; i < 10; i++)
                {
                    CartasGrilla20[i] = Cartas[listaRandoms[i+1]];                   
                    CartasGrilla20[i+10] = Cartas[listaRandoms[i+1]];         
                }
                for (int t = 0; t < CartasGrilla.Length; t++)
                {
                    GameObject temporal = CartasGrilla20[t]; 
                    int r = Random.Range(t, CartasGrilla20.Length);
                    CartasGrilla20[t] = CartasGrilla20[r];
                    CartasGrilla20[r] = temporal;
                }
            break;
            case 3:
                for (int i = 0; i < 9; i++)
                {
                    CartasGrilla18[i] = Cartas[listaRandoms[i+1]];                   
                    CartasGrilla18[i+9] = Cartas[listaRandoms[i+1]];         
                }
                for (int t = 0; t < CartasGrilla.Length; t++)
                {
                    GameObject temporal = CartasGrilla18[t]; 
                    int r = Random.Range(t, CartasGrilla18.Length);
                    CartasGrilla18[t] = CartasGrilla18[r];
                    CartasGrilla18[r] = temporal;
                }
            break;
            case 4:
                for (int i = 0; i < 8; i++)
                {
                    CartasGrilla16[i] = Cartas[listaRandoms[i+1]];                   
                    CartasGrilla16[i+8] = Cartas[listaRandoms[i+1]];         
                }
                for (int t = 0; t < CartasGrilla.Length; t++)
                {
                    GameObject temporal = CartasGrilla16[t]; 
                    int r = Random.Range(t, CartasGrilla16.Length);
                    CartasGrilla16[t] = CartasGrilla16[r];
                    CartasGrilla16[r] = temporal;
                }
            break;
            case 5:
                for (int i = 0; i < 7; i++)
                {
                    CartasGrilla14[i] = Cartas[listaRandoms[i+1]];                   
                    CartasGrilla14[i+7] = Cartas[listaRandoms[i+1]];         
                }
                for (int t = 0; t < CartasGrilla.Length; t++)
                {
                    GameObject temporal = CartasGrilla14[t]; 
                    int r = Random.Range(t, CartasGrilla14.Length);
                    CartasGrilla14[t] = CartasGrilla14[r];
                    CartasGrilla14[r] = temporal;                    
                }
            break;
            case 6:
                for (int i = 0; i < 6; i++)
                {
                    CartasGrilla12[i] = Cartas[listaRandoms[i+1]];                   
                    CartasGrilla12[i+6] = Cartas[listaRandoms[i+1]];         
                }
                for (int t = 0; t < CartasGrilla.Length; t++)
                {
                    GameObject temporal = CartasGrilla12[t]; 
                    int r = Random.Range(t, CartasGrilla12.Length);
                    CartasGrilla12[t] = CartasGrilla12[r];
                    CartasGrilla12[r] = temporal;                    
                }
            break;        
        }*/
        for (int i = 0; i < 9; i++)
        {
            CartasGrilla[i] = Cartas[listaRandoms[i+1]];                   
            CartasGrilla[i+9] = Cartas[listaRandoms[i+1]];         
        }
        for (int t = 0; t < CartasGrilla.Length; t++)
        {
            GameObject temporal = CartasGrilla[t]; 
            int r = Random.Range(t, CartasGrilla.Length);
            CartasGrilla[t] = CartasGrilla[r];
            CartasGrilla[r] = temporal;
            //Debug.Log("TOTAL ARRAY"+CartasGrilla.Length); 
        }
        /*switch (auxiliarGrilla)
        {
            case 0:
               for (int i = 0; i < UbicacionCartas24.Length; i++)
                {
                    Instantiate(CartasGrilla24[i],UbicacionCartas24[i].transform.position,transform.rotation); 
                }
            break;
            case 1:
                for (int i = 0; i < UbicacionCartas22.Length; i++)
                {
                    Instantiate(CartasGrilla22[i],UbicacionCartas22[i].transform.position,transform.rotation); 
                }
            break;
            case 2:
                for (int i = 0; i < UbicacionCartas20.Length; i++)
                {
                    Instantiate(CartasGrilla20[i],UbicacionCartas20[i].transform.position,transform.rotation); 
                }
            break;
            case 3:
                for (int i = 0; i < UbicacionCartas18.Length; i++)
                {
                    Instantiate(CartasGrilla18[i],UbicacionCartas18[i].transform.position,transform.rotation); 
                }
            break;
            case 4:
                for (int i = 0; i < UbicacionCartas16.Length; i++)
                {
                    Instantiate(CartasGrilla16[i],UbicacionCartas16[i].transform.position,transform.rotation); 
                }
            break;
            case 5:
                for (int i = 0; i < UbicacionCartas14.Length; i++)
                {
                    Instantiate(CartasGrilla14[i],UbicacionCartas14[i].transform.position,transform.rotation); 
                }
            break;
            case 6:
                for (int i = 0; i < UbicacionCartas12.Length; i++)
                {
                    Instantiate(CartasGrilla12[i],UbicacionCartas12[i].transform.position,transform.rotation); 
                }
            break;        
        }*/
        for (int i = 0; i < UbicacionCartas.Length; i++)
        {
            Instantiate(CartasGrilla[i],UbicacionCartas[i].transform.position,transform.rotation); 
        }
            
        
    }
    
   
}

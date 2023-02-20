using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generador : MonoBehaviour
{
    #region Arrays en uso
    [SerializeField] private GameObject[] Residuos;
    [SerializeField] private GameObject[] NoRecuperables;
    [SerializeField] private GameObject[] Organicos;
    [SerializeField] private GameObject[] Vidrios;
    [SerializeField] private GameObject[] Plasticos;
    [SerializeField] private GameObject[] Cartones;
    [SerializeField] private GameObject[] Metales; 

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject Tachos;  
    [SerializeField] private GameObject[] PosicionesTachos;
    #endregion
    [SerializeField] private GameObject tutorialParte1;
    #region Indices de arrays
    private int indexNoRecuperables = 4;
    private int indexOrganicos = 5;
    private int indexVidrios = 2;
    private int indexPlasticos = 2;
    private int indexCartones = 2;
    private int indexMetales = 1;
    #endregion
    #region Contadores
    private int cantidadResiduos = 0;    
    private int cantidadNoRecuperables = 0;
    private int cantidadOrganicos = 0;
    private int cantidadVidrios = 0;
    private int cantidadPlasticos = 0;
    private int cantidadCartones = 0;
    private int cantidadMetales = 0;
    public static int contadorBasura = 0;
    private float Tiempo = 0f;
    private float intervalo = 0;    
    private float fraction = 10f;
    #endregion
    #region Sorting Layer Config
    public const string PrincipalLayer = "Principal";
    public const string SecundarioLayer = "Secundario";
    public int sortingOrder = 0;
    private SpriteRenderer TachoNoRecSprite;
    private SpriteRenderer TachoRecSprite;
    private SpriteRenderer TachoOrgSprite;
    #endregion   
    #region Banderas
    public static bool Tacho1 = false;
    public static bool Tacho2 = false;
    public static bool Tacho3 = false;
    public static bool Terminaste = false;
    private bool PrimeraVuelta = true;
    #endregion
    public static bool bloqueaMovimiento = false;    

    public static int cantidadReiniciar = 2;  
    [SerializeField] private Button BotonReiniciarNivel;
    
    
    private Vector3 velocity = Vector3.zero;        
    private GameObject globalaux;
    //private GlobalVariables gv; 
    //AudioManager audioManager;
    
    void Start () 
    {       
        Tacho1 = false;
        Tacho2 = false;
        Tacho3 = false;
        Terminaste = false;
        bloqueaMovimiento = false;
        PrimeraVuelta = true;
        contadorBasura = 0;
        intervalo = 4;
        globalaux = GameObject.Find("GlobalVariables");
        //gv = globalaux.GetComponent<GlobalVariables>();
        //audioManager = GameObject.FindObjectOfType<AudioManager>();
        /*
        if (gv.tutorialCentral == true)
        {
            Time.timeScale = 1f;
            tutorialParte1.gameObject.SetActive(false);
            //audioManager.StopMusic();
            //audioManager.PlayMusic("Minigame_Theme"); 
            Debug.Log("TIEMPO EN   "+Time.timeScale); 
        }
        if (gv.tutorialCentral == false)
        {
            Time.timeScale = 0f;
            tutorialParte1.gameObject.SetActive(true);
           // audioManager.StopMusic();
            //audioManager.PlayMusic("Tutorial_Theme");
            Debug.Log("TIEMPO EN   "+Time.timeScale); 
        }
        */
        TachoNoRecSprite = Tachos.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        TachoRecSprite = Tachos.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        TachoOrgSprite = Tachos.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();            
        cantidadNoRecuperables = 8;
        cantidadOrganicos = 10;
        cantidadVidrios = 3;
        cantidadPlasticos = 3;
        cantidadCartones = 3;
        cantidadMetales = 3;
        cantidadResiduos = cantidadNoRecuperables+cantidadOrganicos+cantidadVidrios+cantidadPlasticos+cantidadCartones+cantidadMetales;              
        Residuos = new GameObject[cantidadResiduos];       
        CreateResiduos();           
    }

    void Update()
    {
        Controls();
        Tiempo += Time.deltaTime;               
        InstanceIntervalo();         
        /*if (gv.cantidadReiniciar<=0)
        {
            BotonReiniciarNivel.interactable = false;
        } */
          
    }  
    
  
    void CreateResiduos()
    {       
        for (int i = 0; i < (cantidadNoRecuperables); i++)
        {
            Residuos[i] = NoRecuperables[Random.Range(0,indexNoRecuperables+1)];            
        }
        for (int i = 0; i < (cantidadOrganicos); i++)
        {
            Residuos[cantidadNoRecuperables+i]= Organicos[Random.Range(0,indexOrganicos+1)];               
        }
        for (int i = 0; i < (cantidadVidrios); i++)
        {
            Residuos[cantidadNoRecuperables+cantidadOrganicos+i] = Vidrios[Random.Range(0,indexVidrios+1)];            
        }
        for (int i = 0; i < (cantidadPlasticos); i++)
        {
            Residuos[cantidadNoRecuperables+cantidadOrganicos+cantidadVidrios+i] = Plasticos[Random.Range(0,indexPlasticos+1)];            
        }
        for (int i = 0; i < (cantidadCartones); i++)
        {
            Residuos[cantidadNoRecuperables+cantidadOrganicos+cantidadVidrios+cantidadPlasticos+i]= Cartones[Random.Range(0,indexCartones+1)];               
        }
        for (int i = 0; i < (cantidadMetales); i++)
        {
            Residuos[cantidadNoRecuperables+cantidadOrganicos+cantidadVidrios+cantidadPlasticos+cantidadCartones+i] = Metales[Random.Range(0,indexMetales+1)];            
        }

        for (int t = 0; t < Residuos.Length; t++)
        {
            GameObject temporal = Residuos[t]; 
            int r = Random.Range(t, Residuos.Length);
            Residuos[t] = Residuos[r];
            Residuos[r] = temporal;            
        }
    }
    void Controls()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.timeScale != 0f)
            {
                Time.timeScale = 2.5f;
            }                
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(Time.timeScale != 0f)
            {
                Time.timeScale = 1f;
            }
        }
            
        if (Tacho1)
        {
            Tachos.transform.GetChild(0).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(0).gameObject.transform.position, PosicionesTachos[0].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(1).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(1).gameObject.transform.position, PosicionesTachos[1].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(2).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(2).gameObject.transform.position, PosicionesTachos[2].transform.position, fraction*Time.deltaTime);
            TachoNoRecSprite.sortingOrder = sortingOrder;
            TachoNoRecSprite.sortingLayerName = PrincipalLayer;
            TachoRecSprite.sortingOrder = sortingOrder;
            TachoRecSprite.sortingLayerName = SecundarioLayer;
            TachoOrgSprite.sortingOrder = sortingOrder;
            TachoOrgSprite.sortingLayerName = SecundarioLayer;
        }
        if (Tacho2)
        {
            Tachos.transform.GetChild(1).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(1).gameObject.transform.position, PosicionesTachos[0].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(2).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(2).gameObject.transform.position, PosicionesTachos[1].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(0).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(0).gameObject.transform.position, PosicionesTachos[2].transform.position, fraction*Time.deltaTime); 
            TachoNoRecSprite.sortingOrder = sortingOrder;
            TachoNoRecSprite.sortingLayerName = SecundarioLayer;
            TachoRecSprite.sortingOrder = sortingOrder;
            TachoRecSprite.sortingLayerName = PrincipalLayer;
            TachoOrgSprite.sortingOrder = sortingOrder;
            TachoOrgSprite.sortingLayerName = SecundarioLayer;
        }
        if (Tacho3)
        {
            Tachos.transform.GetChild(2).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(2).gameObject.transform.position, PosicionesTachos[0].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(0).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(0).gameObject.transform.position, PosicionesTachos[1].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(1).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(1).gameObject.transform.position, PosicionesTachos[2].transform.position, fraction*Time.deltaTime);
            TachoNoRecSprite.sortingOrder = sortingOrder;
            TachoNoRecSprite.sortingLayerName = SecundarioLayer;
            TachoRecSprite.sortingOrder = sortingOrder;
            TachoRecSprite.sortingLayerName = SecundarioLayer;
            TachoOrgSprite.sortingOrder = sortingOrder;
            TachoOrgSprite.sortingLayerName = PrincipalLayer;
        }      
        
    }
    void InstanceIntervalo()    
    {
        if (PrimeraVuelta)
        {
            Instantiate(Residuos[contadorBasura],spawnPoint.position,transform.rotation);
            contadorBasura += 1;
            PrimeraVuelta = false;
        }
        if (Mathf.Round(Tiempo) == intervalo)
        {            
            if  (contadorBasura < cantidadResiduos)
            {
                Instantiate(Residuos[contadorBasura],spawnPoint.position,transform.rotation);
                contadorBasura += 1;
                Tiempo = 0f;
            }
            else
            {
                Terminaste = true;
            }
            
        }
        
    }
    

   
}

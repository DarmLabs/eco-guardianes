using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Tachos : MonoBehaviour
{
    private GameObject globalaux;
    //private GlobalVariables gv;
    private GameObject saveaux;
    //private SaveLoadSystem saveSystem;
    public static int errores = 0;
    public static int errorRecuperableVidrio = 0;
    public static int errorRecuperablePlastico = 0;
    public static int errorRecuperableCarton = 0;
    public static int errorRecuperableMetal = 0;
    public static int errorNoRecuperable = 0;
    public static int errorOrganico = 0;
    public static int errorRecuperable = 0;
    public static int cantidadAyudas = 3;
    private int ContadorTotal = 0;
    private string nombreRecuperable;
    private string nombreAuxiliar;
    public Text residuosNoRecuperables;
    public Text residuosRecuperables;
    public Text residuosOrganicos;
    public Text erroresText;

    [SerializeField] private GameObject PanelVictoria;
    [SerializeField] private GameObject PanelDerrota;
    [SerializeField] private Button BotonAyudas;
    [SerializeField] private GameObject flechita;
    public TMP_Text CantidadAyudas_Text;
    //AudioManager audioManager;     

    void Start()
    {       
        Generador.contadorBasura = 0;
        errores = 0;
        errorRecuperableVidrio = 0;
        errorRecuperablePlastico = 0;
        errorRecuperableCarton = 0;
        errorRecuperableMetal = 0;
        errorNoRecuperable = 0;
        errorOrganico = 0;
        errorRecuperable = 0;
        cantidadAyudas = 3;        
        globalaux = GameObject.Find("GlobalVariables");
        //gv = globalaux.GetComponent<GlobalVariables>();
        saveaux = GameObject.Find ("SaveLoadSystem");
        //saveSystem = saveaux.GetComponent<SaveLoadSystem>();
        //audioManager = GameObject.FindObjectOfType<AudioManager>(); 
        //gv.divisionNoRec = 0;
        //gv.divisionCompostables = 0;
        //gv.divisionRec = 0;
        errores = 0;  
        
    }

    void Update()
    {
        //ContadorTotal = gv.divisionRec+gv.divisionNoRec+gv.divisionCompostables+errores;
        CantidadAyudas_Text.text = cantidadAyudas.ToString();
        if (errores == 6)
        {            
            //audioManager.PlayAudio("Lost");           
            PanelDerrota.SetActive(true);           
            Time.timeScale = 0f;
            //saveSystem.Save();  
        }
        if (Generador.Terminaste && errores < 7 && ContadorTotal == Generador.contadorBasura)
        {            
            //audioManager.PlayAudio("Win");                       
            PanelVictoria.SetActive(true); /*           
            gv.divisionCarton += gv.cartonTrash-(3*errorRecuperableCarton);
            gv.divisionMetal += gv.metalTrash-(3*errorRecuperableMetal);
            gv.divisionPlastico += gv.plasticoTrash-(3*errorRecuperablePlastico);
            gv.divisionVidrio +=  gv.vidrioTrash-(3*errorRecuperableVidrio);
            gv.divisionNoRec += gv.noRecTrash-(3*errorNoRecuperable);
            gv.divisionOrganic += gv.organicTrash-(3*errorOrganico);
            gv.noRecTrash=0;
            gv.organicTrash=0;
            gv.vidrioTrash=0;
            gv.plasticoTrash=0;
            gv.cartonTrash=0;
            gv.metalTrash=0;
            Time.timeScale = 0f;            
            Generador.Terminaste = false;
            gv.memoriaAccesible = true;
            //saveSystem.Save();*/
        }
        if(cantidadAyudas<=0)
        {
            BotonAyudas.interactable = false;
            //CantidadAyudas_Text;
            Destroy(flechita);
        }        
        
            
    }
    void  OnMouseDown() 
    {        
        if (!Generador.bloqueaMovimiento)
        {
            if (this.gameObject.name == "NoRecuperable")
            {
                Generador.Tacho1 = true;
                Generador.Tacho2 = false;
                Generador.Tacho3 = false;
            }
            if (this.gameObject.name == "Recuperable")
            {
                Generador.Tacho1 = false;
                Generador.Tacho2 = true;
                Generador.Tacho3 = false; 
            }
            if (this.gameObject.name == "Organico")
            {
                Generador.Tacho1 = false;
                Generador.Tacho2 = false;
                Generador.Tacho3 = true;
            }
        }                  
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (this.gameObject.tag == other.gameObject.tag)
        {
            switch (other.gameObject.tag)
            {
                case "Recuperable":
                    //gv.divisionRec +=1;
                    //audioManager.PlayAudio("Acierto_Sound");
                    //residuosRecuperables.text = gv.divisionRec.ToString();
                    switch (other.transform.GetChild(0).gameObject.tag)
                    {
                        case "Vidrio":
                            //gv.divisionVidrio +=1;
                            //Debug.Log("dividió vidrio"+gv.divisionVidrio);
                        break;
                        case "Plastico":
                            //gv.divisionPlastico +=1; 
                            //Debug.Log("dividió plastico"+gv.divisionPlastico);
                        break;
                        case "Metal":
                            //gv.divisionMetal +=1;
                            //Debug.Log("dividió metal"+gv.divisionMetal);
                        break;
                        case "Carton":
                            //gv.divisionCarton +=1;
                            //Debug.Log("dividió carton"+gv.divisionCarton);
                        break;
                    }
                 
                break;
                case "NoRecuperable":                   
                    //gv.divisionNoRec +=1; 
                    //audioManager.PlayAudio("Acierto_Sound");                     
                    //residuosNoRecuperables.text = gv.divisionNoRec.ToString();
                    break;
                case "Organico":                    
                    //gv.divisionOrganic+=1;
                    //gv.divisionCompostables+=1;
                    //audioManager.PlayAudio("Acierto_Sound");                    
                    //residuosOrganicos.text = gv.divisionCompostables.ToString();
                    break;
            } 
             
            //saveSystem.Save();           
        }
        else
        {
            errores = errores + 1;
            //audioManager.PlayAudio("Error_Sound");
            erroresText.text = errores.ToString();
            if(cantidadAyudas>=1)
            {
                flechita.SetActive(true);
            } 
            switch (other.gameObject.tag)
            {
                case "Recuperable":
                    switch (other.transform.GetChild(0).gameObject.tag)
                    {
                        case "Vidrio":
                            errorRecuperableVidrio +=1;                            
                        break;
                        case "Plastico":
                            errorRecuperablePlastico +=1;
                        break;
                        case "Metal":
                            errorRecuperableMetal +=1;
                        break;
                        case "Carton":
                            errorRecuperableCarton +=1;
                        break;
                    }
                    errorRecuperable+=1;
                    break;
                case "NoRecuperable":
                    errorNoRecuperable+=1;                   
                    break;
                case "Organico":                    
                    errorOrganico+=1;                 
                    break;
            } 
            //saveSystem.Save(); 
        }       
    }
    
}

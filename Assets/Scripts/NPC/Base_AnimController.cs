using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_AnimController : MonoBehaviour
{
    Animator anim;
    public const string idleId = "Idle_0";
    public const string sitId = "Down_0";
    [Header("Numero maximo de animacion por tipo")]
    [Range(5, 7)] public int idleMaxIndex;
    [Range(3, 4)] public int sitMaxIndex;

    [Header("Indicar que animacion y como debe estar el NPC")]
    [Range(1, 7)][SerializeField] int idleAnimStyle;
    [Range(1, 4)][SerializeField] int sitAnimStyle;
    [SerializeField] bool isSitting;
    bool isMoving;
    public int IdleAnimStyle => idleAnimStyle;
    public int SitAnimStyle => sitAnimStyle;
    public bool IsSitting => isSitting;
    public virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void PlayAnimation(string animName)
    {
        anim.Play(animName);
    }
    public virtual void ChooseStartAnimation()
    {
        if (isSitting)
        {
            if (sitMaxIndex < sitAnimStyle)
            {
                Debug.LogError($"El valor de indice ingresado en el tipo de animacion 'sit' es mayor al indice maximo de animaciones existentes: {sitMaxIndex}. Revise el numero ingresado en Sit Anim Style");
                return;
            }
            PlayAnimation($"{sitId}{SitAnimStyle}");
            return;
        }
        if (idleMaxIndex < idleAnimStyle)
        {
            Debug.LogError($"El valor de indice ingresado en el tipo de animacion 'Idle' es mayor al indice maximo de animaciones existentes: {idleMaxIndex}. Revise el numero ingresado en Idle Anim Style");
            return;
        }
        PlayAnimation($"{idleId}{IdleAnimStyle}");
    }
}

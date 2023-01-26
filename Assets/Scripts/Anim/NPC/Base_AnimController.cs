using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Base_AnimController : MonoBehaviour
{
    Animator anim;
    NavMeshObstacle obstacle;
    BoxCollider mainCollider;
    Vector3 originalObstacleCenter;
    Vector3 originalObstacleSize;
    public const string idleId = "Idle_";
    public const string sitId = "Down_";
    [Header("Numero maximo de animacion por tipo")]
    [Range(5, 13)] public int idleMaxIndex;
    [Range(3, 4)] public int sitMaxIndex;

    [Header("Indicar que animacion y como debe estar el NPC")]
    [Range(1, 13)][SerializeField] int idleAnimStyle;
    [Range(1, 4)][SerializeField] int sitAnimStyle;
    [SerializeField] bool isSitting;
    bool isMoving;
    public int IdleAnimStyle => idleAnimStyle;
    public int SitAnimStyle => sitAnimStyle;
    public bool IsSitting => isSitting;
    public virtual void Awake()
    {
        anim = GetComponent<Animator>();
        obstacle = GetComponent<NavMeshObstacle>();
        mainCollider = GetComponent<BoxCollider>();
        originalObstacleCenter = obstacle.center;
        originalObstacleSize = obstacle.size;
    }
    public void PlayAnimation(string animName)
    {
        anim.Play(animName);
    }
    public virtual void ChooseStartAnimation()
    {
        ApplyObstacleChange();
        if (isSitting)
        {
            if (sitMaxIndex < sitAnimStyle)
            {
                Debug.LogError($"El valor de indice ingresado en el tipo de animacion 'sit' es mayor al indice maximo de animaciones existentes: {sitMaxIndex}. Revise el numero ingresado en Sit Anim Style");
                return;
            }
            if (SitAnimStyle == 2)
            {
                ApplyObstacleChange(true);
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
    void ApplyObstacleChange(bool must = false)
    {
        obstacle.center = must ? new Vector3(obstacle.center.x, obstacle.center.y, obstacle.center.z - 0.8f) : originalObstacleCenter;
        obstacle.size = must ? new Vector3(obstacle.size.x, obstacle.size.y, obstacle.size.z + 0.5f) : originalObstacleSize;
        mainCollider.center = obstacle.center;
        mainCollider.size = obstacle.size;
    }
}

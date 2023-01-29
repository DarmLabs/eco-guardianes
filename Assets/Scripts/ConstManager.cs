using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstManager
{
    #region SaveDataNamePaths
    public const string mainMenuFlags = "mainMenuFlags";
    public const string starsData = "starsData_";
    public const string tutoFlags = "mainMenuFlags";
    #endregion
    #region ScenesNames
    public const string mainMenuSceneName = "MainMenu";
    public const string mapSceneName = "MapScene";
    public const string characterCreationSceneName = "CharacterCreation";
    public const string loadingSceneName = "LoadingScene";
    public const string cocinaSceneName = "Casa";
    public const string escuelaSceneName = "Escuela";
    public const string plazaSceneName = "Plaza";
    public const string plantaDentro = "Planta de reciclado (Adentro)";
    public const string plantaFuera = "Planta de reciclado (Afuera)";
    #endregion
    #region Action Messeges
    public const string actionQuestionTrashCan = "¿Quieres tirar residuos?";
    public const string actionQuestionOpenObject = "¿Quieres juntarlo/a?";
    public const string actionQuestionClosedObject = "¿Quieres abrirlo/a?";
    #endregion
    #region Tuto Messeges
    public const string tuto_firstStageMessege = "¡Bienvenido a Eco Guardianes!\nEn este turorial aprenderas los controles y objetivos que debes alcanzar.\n¡Vamos!";
    public const string tuto_secondStageMessege = "Tu objetivo es encontrar y recolectar todos los residuos del mapa, clasificandolos en los tachos.\n Ahora vamos a ver como hacer eso.";
    public const string tuto_thirdStageMessege_PC = "Para moverte debes hacer click sobre el piso.";
    public const string tuto_thirdStageMessege_MOBILE = "Para moverte debes tocar el piso con tu dedo.";
    public const string tuto_forthStageMessege = "Estos objetos son los residuos que debes juntar, hay un maximo de 9 por mapa, debes tocarlos para recolectarlos.";
    #endregion
}

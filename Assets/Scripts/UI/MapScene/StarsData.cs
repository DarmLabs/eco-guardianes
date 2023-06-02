using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class StarsData
{
    public int starsCasaCount;
    public int starsPlazaCount;
    public int starsEscuelaCount;
    public bool newMapMarkPassed;
    public StarsData(int starsCasaCount, int starsPlazaCount, int starsEscuelaCount, bool newMapMarkPassed)
    {
        this.starsCasaCount = starsCasaCount;
        this.starsPlazaCount = starsPlazaCount;
        this.starsEscuelaCount = starsEscuelaCount;
        this.newMapMarkPassed = newMapMarkPassed;
    }
}

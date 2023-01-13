using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class MainMenuData
{
    public bool isCharacterCreated;
    public MainMenuData(bool isCharacterCreated)
    {
        this.isCharacterCreated = isCharacterCreated;
    }
}

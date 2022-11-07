using System;
[Serializable]
public class CharacterData
{
    public int tonesIndex;
    public int shoesIndex;
    public int pantsIndex;
    public int shirtIndex;
    public int headIndex;
    public string characterName;
    public CharacterData(int tonesIndex, int shoesIndex, int pantsIndex, int shirtIndex, int headIndex, string characterName)
    {
        this.tonesIndex = tonesIndex;
        this.shoesIndex = shoesIndex;
        this.pantsIndex = pantsIndex;
        this.shirtIndex = shirtIndex;
        this.headIndex = headIndex;
        this.characterName = characterName;
    }
}

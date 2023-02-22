using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
public class MemoryManager : MonoBehaviour
{
    //Tool variables
    public static MemoryManager SharedInstance;
    [HideInInspector] public UnityEvent<Card> cardFlipped;
    int flippedCount;
    [HideInInspector] public UnityEvent<bool> isCardFlipping;
    Card firstCard, secondCard;
    [Header("Required Sprites & Objects")]
    [SerializeField] List<Sprite> cardSprites;
    List<Card> cards;
    [SerializeField] Sprite backSprite;
    public Sprite BackSprite => backSprite;
    [Header("Lives")] //And scores
    [SerializeField] int maxLives;
    int maxScore;
    int score;
    public int Score => score;
    int lives;
    public int Lives => lives;

    void Awake()
    {
        SharedInstance = this;
        cardFlipped.AddListener(CardFlipped);

    }
    void Start()
    {
        cards = GetComponentsInChildren<Card>().ToList();
        lives = maxLives;
        maxScore = cards.Count / 2;
        RandomlySetCardsSprites();
        MemoryUIManager.SharedInstance.UpdateLives();
    }
    public void RandomlySetCardsSprites()
    {
        int cardsCount = cards.Count;
        for (int i = 0; i < cardsCount; i++)
        {
            if (cards.Count == 0)
            {
                return;
            }
            int randomSpriteIndex = Random.Range(0, cardSprites.Count);
            for (int ci = 0; ci < 2; ci++)
            {
                int randomCardIndex = Random.Range(0, cards.Count);
                cards[randomCardIndex].CardSprite = cardSprites[randomSpriteIndex];
                cards.RemoveAt(randomCardIndex);
            }
            cardSprites.RemoveAt(randomSpriteIndex);
        }
    }
    void CardFlipped(Card card)
    {
        flippedCount += 1;
        if (flippedCount == 1)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            if (firstCard.CardSprite == secondCard.CardSprite)
            {
                score++;
                CheckScore();
                ResetCards();
            }
            else
            {
                StartCoroutine(WaitAndRotateBackCards(0.5f));
            }
        }
    }
    void ResetCards()
    {
        flippedCount = 0;
        firstCard = null;
        secondCard = null;
    }
    void CheckScore()
    {
        MemoryUIManager.SharedInstance.UpdateScore();
        if (score == maxScore)
        {
            MemoryUIManager.SharedInstance.ActivateEndPanel();
        }
    }
    IEnumerator CheckLives()
    {
        yield return new WaitForSeconds(0.5f);
        lives--;
        MemoryUIManager.SharedInstance.UpdateLives();
        if (lives == 0)
        {
            MemoryUIManager.SharedInstance.ActivateEndPanel();
        }
    }
    IEnumerator WaitAndRotateBackCards(float secs)
    {
        yield return new WaitForSeconds(secs);
        StartCoroutine(firstCard.RotateToBack());
        StartCoroutine(secondCard.RotateToBack());
        StartCoroutine(CheckLives());
        ResetCards();
    }
}

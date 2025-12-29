using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform boardParent;
    public int rows = 2;
    public int cols = 3;
    public float spacing = 1.5f;

    private List<Card> cards = new List<Card>();
    private List<Card> flippedCards = new List<Card>();
    public int score = 0;

    private void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        cards.Clear();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject obj = Instantiate(cardPrefab, boardParent);
                obj.transform.localPosition = new Vector3(j * spacing, -i * spacing, 0);
                Card card = obj.GetComponent<Card>();
                card.id = (i * cols + j) / 2; // pair matching
                cards.Add(card);
            }
        }
    }

    public void CardFlipped(Card card)
    {
        flippedCards.Add(card);
        if (flippedCards.Count == 2)
        {
            if (flippedCards[0].id == flippedCards[1].id)
            {
                score += 10; // simple scoring
                flippedCards.Clear();
                // play match sound here
            }
            else
            {
                // mismatch, flip back
                StartCoroutine(FlipBack(flippedCards[0], flippedCards[1]));
            }
        }
    }

    private IEnumerator FlipBack(Card c1, Card c2)
    {
        yield return new WaitForSeconds(0.5f);
        c1.Flip();
        c2.Flip();
        flippedCards.Clear();
        // play mismatch sound here
    }
}
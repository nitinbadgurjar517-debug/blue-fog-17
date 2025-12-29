using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
    public int id;
    public Sprite front;
    public Sprite back;
    private SpriteRenderer spriteRenderer;
    private bool isFlipped = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = back;
    }

    public void Flip()
    {
        StartCoroutine(FlipAnimation());
    }

    IEnumerator FlipAnimation()
    {
        float duration = 0.3f;
        float time = 0;
        while (time < duration)
        {
            float scale = Mathf.Lerp(1, 0, time / duration);
            transform.localScale = new Vector3(scale, 1, 1);
            time += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.sprite = isFlipped ? back : front;
        isFlipped = !isFlipped;

        time = 0;
        while (time < duration)
        {
            float scale = Mathf.Lerp(0, 1, time / duration);
            transform.localScale = new Vector3(scale, 1, 1);
            time += Time.deltaTime;
            yield return null;
        }

        FindObjectOfType<GameManager>().CardFlipped(this);
    }
}

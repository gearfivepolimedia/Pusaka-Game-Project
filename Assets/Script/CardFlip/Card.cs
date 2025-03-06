using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    private SpriteRenderer rend;
    [SerializeField] private Sprite faceSprite, backSprite;

    private bool coroutineAllowed = true;
    private bool facedUp = false;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = backSprite;
    }

    private void OnMouseDown()
    {
        if (coroutineAllowed && !facedUp)
        {
            StartCoroutine(RotateCard());
            GameManager.Instance.CardSelected(this);
        }
    }

    private IEnumerator RotateCard()
    {
        coroutineAllowed = false;
        for (float i = 0f; i <= 180f; i += 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {
                rend.sprite = faceSprite;
            }
            yield return new WaitForSeconds(0.01f);
        }

        facedUp = true;
        coroutineAllowed = true;
    }

    public void FlipBack()
    {
        StartCoroutine(RotateBack());
    }

    private IEnumerator RotateBack()
    {
        coroutineAllowed = false;
        for (float i = 180f; i >= 0f; i -= 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {
                rend.sprite = backSprite;
            }
            yield return new WaitForSeconds(0.01f);
        }

        facedUp = false;
        coroutineAllowed = true;
    }

    public Sprite GetSprite()
    {
        return faceSprite;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ScorePopup : MonoBehaviour
{
    public TextMeshPro textMesh;
    private float moveSpeed = 1f;
    private float fadeSpeed = 1f;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        textMesh.text = "+100";
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color color = textMesh.color;
        Vector3 startPos = transform.position;

        for (float t = 0; t < 1; t += Time.deltaTime * fadeSpeed)
        {
            transform.position = startPos + Vector3.up * moveSpeed * t;
            color.a = Mathf.Lerp(1, 0, t);
            textMesh.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }
}

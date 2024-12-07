using TMPro;
using UnityEngine;

public class PointsBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    TextMeshProUGUI spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spriteRenderer.color.a >= 0)
        {
            spriteRenderer.color -= new Color(0, 0, 0, 1f * Time.deltaTime);
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

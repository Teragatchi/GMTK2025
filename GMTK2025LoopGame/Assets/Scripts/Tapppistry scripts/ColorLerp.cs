using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private Color endColor = Color.red;
    private float speed = 0.01f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        speed += Time.deltaTime;
        spriteRenderer.material.color = Color.Lerp(defaultColor, endColor, speed);
    }
}

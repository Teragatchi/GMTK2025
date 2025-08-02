using System.Collections;
using System.Threading;
using UnityEngine;

public class RecolorStitchLine_Slow : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private Color32 defaultColor = new Color32(63, 63, 63, 103);
    private Color32 brownColor = new Color32(142, 121, 128, 200);
    private Color32 roseColor = new Color32(182, 102, 115, 200);
    private float speed = 0.01f;

    private bool isPlayerOnTop = false;
    private bool isColorBrown = false;
    private bool isColorRose = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isColorBrown)
        {
            Debug.Log("Change color to brown");
            speed += Time.deltaTime;
            mySpriteRenderer.color = Color32.Lerp(defaultColor, brownColor, speed);
        }
        if (isColorRose)
        {
            Debug.Log("Change color to rose");
            speed += Time.deltaTime;
            mySpriteRenderer.color = Color32.Lerp(defaultColor, roseColor, speed);
        }
    }

    private void OnTriggerStay2D(Collider2D collision )
    {
        if (collision.gameObject.name == "PlayerBody")
        {
            if (this.gameObject.tag == "Brown")
            {
                isColorBrown = true;
                isColorRose = false;
            }
            else if (this.gameObject.tag == "Rose")
            {
                isColorRose = true;
                isColorBrown = false;
            }
        }
    }
}

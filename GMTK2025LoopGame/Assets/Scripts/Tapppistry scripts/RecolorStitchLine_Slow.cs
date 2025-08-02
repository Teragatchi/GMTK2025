using System.Collections;
using UnityEngine;

public class RecolorStitchLine_Slow : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private Color defaultColor;
    private Color brownColor = new Color32(142, 121, 128, 200);
    private Color roseColor = new Color32(182, 102, 115, 200);
    private float duration = 0.01f;

    private bool isPlayerOnTop = false;
    private bool isColorBrown = false;
    private bool isColorRose = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = mySpriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerOnTop == true)
        {
            if (isColorBrown)
            {
                Debug.Log("Change color to brown");
               mySpriteRenderer.color = Color.Lerp(brownColor,defaultColor, duration);
            }
            if (isColorRose)
            {
                Debug.Log("Change color to rose");

                mySpriteRenderer.color = Color.Lerp(roseColor, defaultColor, duration);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision )
    {
        if (collision.gameObject.name == "PlayerBody")
        {
            isPlayerOnTop = true;
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
        else
        {
            isPlayerOnTop = false;
        }
    }
}

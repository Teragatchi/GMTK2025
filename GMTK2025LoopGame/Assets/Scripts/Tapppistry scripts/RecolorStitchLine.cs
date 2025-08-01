using UnityEngine;

public class RecolorStitchLine : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision )
    {
        if (collision.gameObject.name == "PlayerBody")
        {
            if (this.gameObject.tag == "Brown")
            {
                mySpriteRenderer.color = new Color32(142, 121, 128, 200);
            }
            else if (this.gameObject.tag == "Rose")
            {
                mySpriteRenderer.color = new Color32(182, 102, 115, 200);

            }
            //mySpriteRenderer.color = Color.red;
            //Debug.Log(this.gameObject.name + "changed color to red via OnTriggerEnter2D");
        }
    }
}

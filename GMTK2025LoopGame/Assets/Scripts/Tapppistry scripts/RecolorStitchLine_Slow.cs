
using UnityEngine;

public class RecolorStitchLine_Slow : MonoBehaviour
{
    [SerializeField] UIThread PlayerThread;
    [SerializeField] Color HeartColor;
    [SerializeField] Color EyeColor;
    [SerializeField] Color LeafColor;


    private SpriteRenderer mySpriteRenderer;
    private Color32 defaultColor = new Color32(63, 63, 63, 103);
    private Color32 brownColor = new Color32(142, 121, 128, 200);
    private Color32 roseColor = new Color32(182, 102, 115, 200);
    private float speed = 0.01f;

    private bool isColorHeart = false;
    private bool isColorEye = false;
    private bool isColorLeaf = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color ActiveColor = PlayerThread.GetCurrentActiveColor();

        if (isColorHeart)
        {
            if(ActiveColor == HeartColor)
            {
            Debug.Log("Change color to red");
            speed += Time.deltaTime;
            mySpriteRenderer.color = Color32.Lerp(defaultColor, HeartColor, speed);
            }

        }
        if (isColorEye)
        {
            if(ActiveColor ==  EyeColor)
            {
            Debug.Log("Change color to rose");
            speed += Time.deltaTime;
            mySpriteRenderer.color = Color32.Lerp(defaultColor, EyeColor, speed);

            }
        }
        if (isColorLeaf)
        {
            if(ActiveColor == LeafColor)
            {
                Debug.Log("Change color to rose");
                speed += Time.deltaTime;
                mySpriteRenderer.color = Color32.Lerp(defaultColor, LeafColor, speed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Set flags once when entering
            if (gameObject.CompareTag("Heart"))
            {
                isColorHeart = true;
            }
            else if (gameObject.CompareTag("Eye"))
            {
                isColorEye = true;
            }
            else if (gameObject.CompareTag("Leaf"))
            {
                isColorLeaf = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reset all flags
            isColorHeart = false;
            isColorEye = false;
            isColorLeaf = false;
            speed = 0.01f;
        }
    }
}

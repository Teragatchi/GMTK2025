using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIThread : MonoBehaviour
{
    public static UIThread Instance;

    [SerializeField] private Image[] threadSlots = new Image[3]; // Top to bottom: [0]=Top, [1]=Mid, [2]=Bottom
    private readonly Queue<Color> colorQueue = new();

    private void Awake()
    {
        Instance = this;
    }

    public void AddColor(Color color)
    {
        // If color already exists, remove it so it can move to bottom
        //colorStack.Remove(color);

        // Add newest to the end (bottom slot)
        colorQueue.Enqueue(color);

        // Remove oldest if over 3
        if (colorQueue.Count > 3)
            colorQueue.Dequeue();

        UpdateUI();
    }

    private void UpdateUI()
    {
        var colors = new List<Color>(colorQueue);
        int colorCount = colors.Count;

        for (int i = 0; i < threadSlots.Length; i++)
        {
            int colorIndex = colorCount - (threadSlots.Length - i);
            if (colorIndex >= 0)
            {
                threadSlots[i].color = colors[colorIndex];
                //threadSlots[i].gameObject.SetActive(true);
            }
            else
            {
                //threadSlots[i].gameObject.SetActive(false);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIThread : MonoBehaviour
{
    public static UIThread Instance;

    [SerializeField] private Image[] threadSlots = new Image[3]; // Top to bottom: [0]=Top, [1]=Mid, [2]=Bottom
    private readonly List<Color> colorStack = new();

    private void Awake()
    {
        Instance = this;
    }

    public void AddColor(Color color)
    {
        // If color already exists, remove it so it can move to bottom
        colorStack.Remove(color);

        // Add to end (bottom)
        colorStack.Add(color);

        // Limit to 3 colors max
        if (colorStack.Count > 3)
            colorStack.RemoveAt(0); // Remove top-most

        UpdateUI();
    }

    private void UpdateUI()
    {
        // Fill all slots with current colorStack (top-down)
        for (int i = 0; i < threadSlots.Length; i++)
        {
            if (i < colorStack.Count)
            {
                threadSlots[i].color = colorStack[i];
                threadSlots[i].gameObject.SetActive(true);
            }
            else
            {
                threadSlots[i].gameObject.SetActive(false); // Hide empty slots
            }
        }
    }
}

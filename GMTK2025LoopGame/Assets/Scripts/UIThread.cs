using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class UIThread : MonoBehaviour
{
    public static UIThread Instance;

    [SerializeField] public Image[] threadSlots = new Image[3]; // Top to bottom: [0]=Top, [1]=Mid, [2]=Bottom
    [SerializeField] private PlayerThreadInventory playerInv;
    [SerializeField] private Image threadMeterFill;

    private readonly Queue<Color> colorQueue = new();
    private readonly List<Color> colorList = new();
    private Color placeholderColor = Color.white;

    private void Awake()
    {
        Instance = this;

        while (colorList.Count < 3)
            colorList.Add(placeholderColor);

        UpdateUI();
    }

    private void UpdateThreadMeter()
    {
        Color activeColor = GetCurrentActiveColor();

        if (activeColor == Color.white)
        {
            threadMeterFill.fillAmount = 0f;
            threadMeterFill.color = Color.white;
            return;
        }

        float current = playerInv.GetThreadAmount(activeColor);
        float max = 100f; // Or set dynamically if you prefer

        threadMeterFill.fillAmount = Mathf.Clamp01(current / max);
        threadMeterFill.color = activeColor;
    }

    //public void AddColor(Color color)
    //{
    //    // If color already exists, remove it so it can move to bottom
    //    //colorStack.Remove(color);

    //    // Add newest to the end (bottom slot)
    //    colorQueue.Enqueue(color);

    //    // Remove oldest if over 3
    //    if (colorQueue.Count > 3)
    //        colorQueue.Dequeue();

    //    UpdateUI();
    //}

    //private void UpdateUI()
    //{
    //    var colors = new List<Color>(colorQueue);
    //    int colorCount = colors.Count;

    //    for (int i = 0; i < threadSlots.Length; i++)
    //    {
    //        int colorIndex = colorCount - (threadSlots.Length - i);
    //        if (colorIndex >= 0)
    //        {
    //            threadSlots[i].color = colors[colorIndex];
    //            //threadSlots[i].gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            //threadSlots[i].gameObject.SetActive(false);
    //        }
    //    }
    //}




    public Color GetCurrentActiveColor()
    {
        return colorList.Count > 0 ? colorList[^1] : Color.clear;
    }
    public void AddColor(Color color)
    {
        // Remove if already exists (re-push to bottom)
        colorList.Remove(color);

        colorList.Add(color); // Add to end (bottom)

        if (colorList.Count > 3)
            colorList.RemoveAt(0); // Remove oldest (top)

        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < threadSlots.Length; i++)
        {
            if (i < colorList.Count)
            {
                threadSlots[i].color = colorList[i];
                //threadSlots[i].gameObject.SetActive(true);
            }
            else
            {
                //threadSlots[i].gameObject.SetActive(false);
            }
        }
        UpdateThreadMeter();
    }

    public void RemoveColorIfEmpty(Color colorToCheck)
    {
        if (!colorList.Contains(colorToCheck)) return;

        float remaining = playerInv.GetThreadAmount(colorToCheck);

        if (remaining < 5f)
        {
            colorList.Remove(colorToCheck);
            while (colorList.Count < 3)
                colorList.Insert(0, placeholderColor);
        }
        UpdateUI();
    }


}

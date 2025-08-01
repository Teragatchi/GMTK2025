using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIThreadDisplay : MonoBehaviour
{
    public static UIThreadDisplay Instance;

    [SerializeField] private Transform container;
    [SerializeField] private GameObject threadUIPrefab;
    [SerializeField] private int maxColors = 4;

    private Dictionary<Color, Slider> colorSlots = new();
    private List<Color> colorOrder = new();

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateColor(Color color, float amount)
    {
        if (colorSlots.ContainsKey(color))
        {
            colorSlots[color].value = amount / 100f;
            return;
        }

        if (colorSlots.Count >= maxColors)
            return; // UI full — ignore new colors (or replace oldest if you want)

        var ui = Instantiate(threadUIPrefab, container);
        var slider = ui.GetComponentInChildren<Slider>();
        var image = slider.fillRect.GetComponent<Image>();

        image.color = color;
        slider.value = amount / 100f;

        colorSlots[color] = slider;
        colorOrder.Add(color);
    }
}

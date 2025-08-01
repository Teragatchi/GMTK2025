using System.Collections.Generic;
using UnityEngine;

public class PlayerThreadInventory : MonoBehaviour
{
    private Dictionary<Color, float> threadInventory = new();

    public void AddThread(Color color, float amount)
    {
        if (threadInventory.ContainsKey(color))
            threadInventory[color] += amount;
        else
            threadInventory[color] = amount;

        UIThreadDisplay.Instance.UpdateColor(color, threadInventory[color]);
    }

    public bool UseThread(Color color, float amount)
    {
        if (!threadInventory.ContainsKey(color) || threadInventory[color] < amount)
            return false;

        threadInventory[color] -= amount;
        UIThreadDisplay.Instance.UpdateColor(color, threadInventory[color]);
        return true;
    }

    public float GetThreadAmount(Color color) => threadInventory.ContainsKey(color) ? threadInventory[color] : 0f;
}

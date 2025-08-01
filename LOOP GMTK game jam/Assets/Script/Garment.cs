using UnityEngine;

public class Garment : MonoBehaviour
{
    [SerializeField] private Color garmentColor = Color.white;
    [SerializeField] private float threadAmount = 100f;

    public Color GarmentColor => garmentColor;
    public float ThreadAmount => threadAmount;
}

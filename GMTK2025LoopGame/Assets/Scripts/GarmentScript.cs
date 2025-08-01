using UnityEngine;

public class GarmentScript : MonoBehaviour
{
    [SerializeField] private Color garmentColor = Color.white;
    [SerializeField] private float threadAmount = 100f;

    public Color GarmentColor => garmentColor;
    public float ThreadAmount => threadAmount;
}

using UnityEngine;

public class PlayerMechanic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<GarmentScript>(out var garment))
        {
            garment.StartShrink(() =>
            {
                GetComponent<PlayerThreadInventory>().AddThread(garment.GarmentColor, garment.ThreadAmount);
                UIThread.Instance.AddColor(garment.GarmentColor);
            });
        }
    }
}

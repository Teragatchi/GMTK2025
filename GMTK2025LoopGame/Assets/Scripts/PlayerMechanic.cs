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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GarmentScript>(out var garment))
        {
            //if (Input.GetMouseButtonDown(0))
            //{
                var inventory = GetComponent<PlayerThreadInventory>();
                inventory.AddThread(garment.GarmentColor, garment.ThreadAmount);
                Destroy(other.gameObject); // simulate "eating"
            //}

        }
    }
}

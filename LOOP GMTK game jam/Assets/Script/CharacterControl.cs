using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Camera mainCamera;

    private Rigidbody rb;
    private Plane groundPlane;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    private void FixedUpdate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!groundPlane.Raycast(ray, out float distance)) return;

        Vector3 target = ray.GetPoint(distance);
        Vector3 direction = (target - transform.position);
        direction.y = 0;

        if (direction.magnitude < 0.1f)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        rb.linearVelocity = direction.normalized * moveSpeed;
    }
}

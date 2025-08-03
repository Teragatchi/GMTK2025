using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] public Camera mainCamera;
    [SerializeField] private Animator _animate;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mouseWorldPos - transform.position);
        //direction.y = direction.y;
        direction = direction.normalized;

        // Stop jitter when near target
        if (Vector2.Distance(mouseWorldPos, transform.position) < 0.5f)
        {
            rb.linearVelocity = Vector2.zero;
            _animate.SetBool("IsMoving", false);
            return;
        }

        rb.linearVelocity = direction * moveSpeed;
        _animate.SetBool("IsMoving", true);
    }
}

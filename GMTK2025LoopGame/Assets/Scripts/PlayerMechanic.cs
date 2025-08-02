using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMechanic : MonoBehaviour
{
    [SerializeField] private float shrinkDuration = 0.3f;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject HookThreadObject;
    //[SerializeField] private int ThreadLossOnHook = 10;

    private bool PlayerIsFalling;
    private PlayerMovement playerMoveScript;
    private PlayerThreadInventory playerThreadInv;
    private Vector3 MousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMoveScript = GetComponent<PlayerMovement>();
        playerThreadInv = GetComponent<PlayerThreadInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        //MousePos = playerMoveScript.mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        mouseScreenPos.z = 0f;
        MousePos = playerMoveScript.mainCamera.ScreenToWorldPoint(mouseScreenPos);
        MousePos.z = 0f; // explicitly flatten

        //Debug.Log($"Player is falling bool : {PlayerIsFalling}");
        if ( PlayerIsFalling)
        {
            StartCoroutine(ShrinkPlayerDeath());
        }

        //RaycastHit2D rayHit = Physics2D.Raycast(transform.position, MousePos);
        Vector2 direction = ((Vector2)MousePos - (Vector2)transform.position).normalized;
        int rayDistance = 20;
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, direction, rayDistance);
        Debug.DrawRay(transform.position, direction, UIThread.Instance.threadSlots[2].color);


        if (Input.GetMouseButtonDown(0) && rayHit.collider != null)
        {
            if (rayHit.collider.name == "Table")
            {
                Debug.Log("you are hitting the table");
                HookToObject(rayHit);
            }
        }
    }

    private void HookToObject(RaycastHit2D ray)
    {
        Color usedColor = UIThread.Instance.threadSlots[2].color;

        Debug.Log($"amt of {usedColor} remaining is {playerThreadInv.GetThreadAmount(usedColor)}");

        Vector3 start = transform.position;
        Vector3 end = MousePos;
        Vector3 mid = (start + end) / 2f;

        Vector3 direction = end - start;
        float distance = direction.magnitude;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (!playerThreadInv.UseThread(usedColor, distance *5)) return;

        GameObject thread = Instantiate(HookThreadObject, mid, Quaternion.Euler(0, 0, angle));
        thread.transform.localScale = new Vector3(distance, 0.2f, 0f);
        int layer = LayerMask.NameToLayer("IgnoreRaycast");
        if (layer == -1)
        {
            Debug.LogWarning("Layer 'IgnoreRaycast' does not exist. Using default layer.");
        }
        else
        {
            thread.layer = layer;
        }

        SpriteRenderer sr = thread.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = usedColor;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<GarmentScript>(out var garment))
        {
            garment.StartShrink(() =>
            {
                playerThreadInv.AddThread(garment.GarmentColor, garment.ThreadAmount);
                UIThread.Instance.AddColor(garment.GarmentColor);
            });
        }

        if(collision.name == "Table" || collision.name == "ThreadObject")
        {
            PlayerIsFalling = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Table" || collision.name == "ThreadObject")
        {
            PlayerIsFalling = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "Table")
        {
            PlayerIsFalling = false;
        }
    }

    private IEnumerator ShrinkPlayerDeath()
    {
        Vector3 startScale = Player.transform.localScale;
        float elapsed = 0f;

        while (elapsed < shrinkDuration)
        {
            float t = elapsed / shrinkDuration;
            Player.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Player.transform.localScale = Vector3.zero;
        Destroy(Player);
        RestartCurrentScene();
    }
    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

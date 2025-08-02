using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMechanic : MonoBehaviour
{
    [SerializeField] private float shrinkDuration = 0.3f;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject HookThreadObject;

    private bool PlayerIsFalling;
    private PlayerMovement playerMoveScript;
    private PlayerThreadInventory playerThreadInv;
    private Vector2 MousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMoveScript = GetComponent<PlayerMovement>();
        playerThreadInv = GetComponent<PlayerThreadInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = playerMoveScript.mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //Debug.Log($"Player is falling bool : {PlayerIsFalling}");
        if( PlayerIsFalling)
        {
            StartCoroutine(ShrinkPlayerDeath());
        }

        //RaycastHit2D rayHit = Physics2D.Raycast(transform.position, MousePos);
        Vector2 direction = (MousePos - (Vector2)transform.position);
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, direction);
        Debug.DrawRay(transform.position, direction, UIThread.Instance.threadSlots[2].color);


        if (Input.GetMouseButtonDown(0) && rayHit.collider != null)
        {
            if (rayHit.collider.name == "Table")
            {
                HookToObject(rayHit);
                Debug.Log("you are hitting the table");
            }
        }
    }

    private void HookToObject(RaycastHit2D ray)
    {
        playerThreadInv.UseThread(UIThread.Instance.threadSlots[2].color, 10);
        Debug.Log($"amt of {UIThread.Instance.threadSlots[2].color} remaining is {playerThreadInv.GetThreadAmount(UIThread.Instance.threadSlots[2].color)}");
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

        if(collision.name == "Table")
        {
            PlayerIsFalling = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Table")
        {
            PlayerIsFalling = true;
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

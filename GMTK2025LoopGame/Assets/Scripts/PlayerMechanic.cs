using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMechanic : MonoBehaviour
{
    [SerializeField] private float shrinkDuration = 0.3f;
    [SerializeField] private GameObject Player;
    private bool PlayerIsFalling;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Player is falling bool : {PlayerIsFalling}");
        if( PlayerIsFalling)
        {
            StartCoroutine(ShrinkPlayerDeath());
            //RestartCurrentScene();
        }
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

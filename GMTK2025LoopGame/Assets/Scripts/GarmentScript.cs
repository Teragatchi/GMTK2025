using System.Collections;
using UnityEngine;

public class GarmentScript : MonoBehaviour
{
    [SerializeField] private Color garmentColor = Color.white;
    [SerializeField] private float threadAmount = 100f;

    public Color GarmentColor => garmentColor;
    public float ThreadAmount => threadAmount;

    [SerializeField] private float shrinkDuration = 0.5f;
    private bool isBeingEaten = false;

    public void StartShrink(System.Action onComplete)
    {
        if (isBeingEaten) return;
        StartCoroutine(ShrinkRoutine(onComplete));
    }

    private IEnumerator ShrinkRoutine(System.Action onComplete)
    {
        isBeingEaten = true;
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < shrinkDuration)
        {
            float t = elapsed / shrinkDuration;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        onComplete?.Invoke();
        Destroy(gameObject);
    }
}

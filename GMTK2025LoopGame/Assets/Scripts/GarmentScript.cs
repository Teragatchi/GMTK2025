using System.Collections;
using UnityEngine;

public class GarmentScript : MonoBehaviour
{
    [SerializeField] private Color garmentColor = Color.white;
    [SerializeField] private float threadAmount = 100f;
    [SerializeField] private int MaxBites = 3;
    private int currentBiteCount = 0;

    public Color GarmentColor => garmentColor;
    public float ThreadAmount => threadAmount;

    [SerializeField] private float shrinkDuration = 0.5f;
    private bool isBeingEaten = false;
    private Vector3 _initialScale;

    private void Start()
    {
        _initialScale = transform.localScale;
    }

    public void StartShrink(System.Action onComplete)
    {
        if (isBeingEaten) return;
        if (currentBiteCount < MaxBites)
        {
            StartCoroutine(ShrinkRoutine(onComplete));
        }
    }

    private IEnumerator ShrinkRoutine(System.Action onComplete)
    {
        isBeingEaten = true;
        currentBiteCount++;
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        float shrinkFactor = 1f - (1f / MaxBites); // uniform scale reduction per bite
        Vector3 shrinkDifference = startScale * shrinkFactor;

        while (elapsed < shrinkDuration)
        {
            float t = elapsed / shrinkDuration;
            transform.localScale = Vector3.Lerp(startScale, shrinkDifference, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        //transform.localScale -= shrinkDifference;
        onComplete?.Invoke();
        
        if(currentBiteCount >= MaxBites)
        {
            Destroy(gameObject);
        }

        isBeingEaten = false;
    }
}

using System.Collections;
using UnityEngine;

public class DespawnShrapnel : MonoBehaviour
{
    public float waitToShrink = 2f;   // Time to wait before starting the shrink
    public float shrinkDuration = 1f; // Time it takes to completely shrink
    private Vector3 originalScale;    // To store the initial scale

    void Start()
    {
        // Save the original scale for use in shrinking
        originalScale = transform.localScale;

        // Start the shrink process after the wait time
        StartCoroutine(CallShrink());
    }

    private IEnumerator CallShrink()
    {
        yield return new WaitForSeconds(waitToShrink);
        yield return StartCoroutine(Shrink());
    }

    private IEnumerator Shrink()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            // Interpolate the scale over time
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / shrinkDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the scale is exactly zero at the end
        transform.localScale = Vector3.zero;

        // Destroy the GameObject after shrinking
        Destroy(gameObject);
    }
}

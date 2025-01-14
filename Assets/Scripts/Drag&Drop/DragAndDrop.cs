using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ChessPieceDrag : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private float dragHeight = 0.2f; // Height above the board during dragging
    private bool isDragging = false;
    private Rigidbody rb;
    private ParticleSystem ps;

    public float moveDuration;
    public float waitToMove;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
    }

    void OnMouseDown()
    {
        // Disable gravity and record the offset
        rb.useGravity = false;
        isDragging = true;
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        // Keep the piece above the board and follow the mouse position
        Vector3 mousePosition = GetMouseWorldPosition() + offset;
        mousePosition.y = dragHeight; // Lock Z-axis above the board
        transform.position = mousePosition;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Enable gravity to drop the piece back on the board
        rb.useGravity = true;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Convert mouse screen position to world position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        ps.Play();
        StartCoroutine(LerpToPosition(other.transform.position)); // Adjust duration as needed
        print(other.gameObject);
    }

    private IEnumerator LerpToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;

        yield return new WaitForSeconds(waitToMove);
        float elapsedTime = 0f;


        while (elapsedTime < moveDuration )
        {
            transform.position = Vector3.Lerp(startPosition,new Vector3(targetPosition.x, transform.position.y, targetPosition.z), elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set precisely to avoid small discrepancies
        transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
    }

}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private float dragHeight = 0.2f; // Height above the board during dragging
    private bool isDragging = false;
    private Rigidbody rb;
    private ParticleSystem ps;
    private TurnSwitching turnSwitch;
    private ScriptManager scriptManager;

    public float moveDuration;
    public float waitToMove;
    public bool isWhite;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
        turnSwitch = FindAnyObjectByType<TurnSwitching>();
        scriptManager = FindAnyObjectByType<ScriptManager>();
        turnSwitch.pieces.Add(this);
    }

    void OnMouseDown()
    {
        if (!this.enabled) return;
        // Disable gravity and record the offset
        rb.useGravity = false;
        isDragging = true;
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
    }

    void OnMouseDrag()
    {
        if (!this.enabled || !isDragging) return;

        // Keep the piece above the board and follow the mouse position
        Vector3 mousePosition = GetMouseWorldPosition() + offset;
        mousePosition.y = dragHeight; // Lock Z-axis above the board
        transform.position = mousePosition;
    }

    void OnMouseUp()
    {
        if (!this.enabled) return;
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
        if (scriptManager.gameStarted)
        {
            turnSwitch.SwitchTurn();
        }
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

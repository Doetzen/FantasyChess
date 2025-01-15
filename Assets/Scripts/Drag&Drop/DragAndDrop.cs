using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private float dragHeight = 0.2f; // Height above the board during dragging
    private bool isDragging = false;
    private bool ignoreTrigger;
    private bool ignoreCollision;
    private Rigidbody rb;
    private ParticleSystem ps;
    private TurnSwitching turnSwitch;
    private ScriptManager scriptManager;
    private GameObject storedTile;

    public float moveDuration;
    public float waitToMove;
    public bool isWhite;
    private RaycastHit hit;
    public Material mat;
    

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
        turnSwitch = FindAnyObjectByType<TurnSwitching>();
        scriptManager = FindAnyObjectByType<ScriptManager>();
        turnSwitch.pieces.Add(this);
        storedTile = GameObject.FindGameObjectWithTag("S");
    }

    void OnMouseDown()
    {
        if (!this.enabled) return;
        // Disable gravity and record the offset
        rb.useGravity = false;
        isDragging = true;
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
        storedTile.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        ignoreCollision = false;
    }

    void OnMouseDrag()
    {
        if (!this.enabled || !isDragging) return;

        // Keep the piece above the board and follow the mouse position
        Vector3 mousePosition = GetMouseWorldPosition() + offset;
        mousePosition.y = dragHeight; // Lock Z-axis above the board
        transform.position = mousePosition;
        StartCoroutine(RayCast());
    }

    void OnMouseUp()
    {
        if (!this.enabled) return;
        isDragging = false;

        // Enable gravity to drop the piece back on the board
        rb.useGravity = true;
        StopCoroutine(RayCast());
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
        if (ignoreTrigger == false)
        {
            ps.Play();
            StartCoroutine(LerpToPosition(other.transform.position)); // Adjust duration as needed
            if (scriptManager.gameStarted)
            {
                turnSwitch.SwitchTurn();
            }
            storedTile.transform.position = new Vector3(0, -1, 0);
        }
        else
        {
            ignoreTrigger = false;
        }
        ignoreCollision = true;
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "M")
        {
            transform.position = storedTile.transform.position;
            ignoreTrigger = true;
            storedTile.transform.position = new Vector3(0, -1, 0);
        }
        else if (collision.gameObject.GetComponent<DragAndDrop>() && collision.gameObject.GetComponent<DragAndDrop>().isWhite == isWhite)
        {
            if (ignoreCollision == false)
            {
                transform.position = storedTile.transform.position;
                ignoreTrigger = true;
                storedTile.transform.position = new Vector3(0, -1, 0);
            }
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

    public IEnumerator RayCast()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1f))
        {
            if (hit.collider.gameObject.GetComponent<DragAndDrop>())
            {
                if (hit.collider.gameObject.GetComponent<DragAndDrop>().isWhite != isWhite)
                {
                    mat.color = Color.red;
                }
                else
                {
                    mat.color = Color.cyan;
                }
            }
            else
            {
                mat.color = Color.cyan;
            }
        }
        yield return null;
    }
    
}

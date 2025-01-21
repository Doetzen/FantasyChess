using UnityEngine;

public class GoToTarget : MonoBehaviour
{
    public Transform target; // The target the object is moving towards
    public float moveSpeedMin, moveSpeedMax; // Speed of the movement
    public float rotationSpeedMin, rotationSpeedMax; // Maximum speed of rotation in degrees per second

    // New variables for height oscillation
    public float oscillationStrength = 0.5f; // Random height variation strength
    public float oscillationSpeed = 2f;     // Speed of vertical oscillation

    private float moveSpeed, rotationSpeed;
    private float oscillationOffset;

    private void Start()
    {
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);

        // Randomize initial oscillation offset for each bat
        oscillationOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void Update()
    {
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        // Ensure the target is assigned
        if (target == null)
            return;

        // Calculate the direction towards the target
        Vector3 direction = target.position - transform.position;

        // Add oscillation to the vertical (Y-axis) direction
        direction.y += Mathf.Sin(Time.time * oscillationSpeed + oscillationOffset) * oscillationStrength;

        // Normalize the direction vector
        direction.Normalize();

        // Rotate smoothly towards the target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move towards the target with the given speed
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}

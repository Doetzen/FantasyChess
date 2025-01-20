using UnityEngine;

public class GoToTarget : MonoBehaviour
{
    public Transform target; // The target the object is moving towards
    public float moveSpeedMin, moveSpeedMax; // Speed of the movement
    public float rotationSpeedMin,rotationSpeedMax; // Maximum speed of rotation in degrees per second

    private float moveSpeed, rotationSpeed;
    private void Start()
    {
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
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

        // Rotate smoothly towards the target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move towards the target with the given speed
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}

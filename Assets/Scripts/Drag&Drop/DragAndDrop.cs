using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mousePosition;
    public bool isDragged;

    private Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePosition();
    }

    private void OnMouseDrag()
    {
      transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);   
    }
}

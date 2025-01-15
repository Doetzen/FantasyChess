using UnityEngine;

public class TakePiece : MonoBehaviour
{

    private DragAndDrop dragAndDrop;
    private TurnSwitching turnSwitch;

    private void Start()
    {
        dragAndDrop = GetComponent<DragAndDrop>();
        turnSwitch = FindAnyObjectByType<TurnSwitching>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!this.enabled) return;

        if (collision.gameObject.GetComponent<DragAndDrop>() != null)
        {
            if (dragAndDrop.isWhite)
            {
                if (collision.gameObject.GetComponent<DragAndDrop>().isWhite == false)
                {
                    turnSwitch.pieces.Remove(collision.gameObject.GetComponent<DragAndDrop>());
                    Destroy(collision.gameObject.transform.parent.transform.parent.gameObject);
                }
            }
            else if (!dragAndDrop.isWhite)
            {
                if (collision.gameObject.GetComponent<DragAndDrop>().isWhite)
                {
                    turnSwitch.pieces.Remove(collision.gameObject.GetComponent<DragAndDrop>());
                    Destroy(collision.gameObject.transform.parent.transform.parent.gameObject);
                }
            }
        }
    }

   

}

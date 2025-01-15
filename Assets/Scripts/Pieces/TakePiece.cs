using UnityEngine;

public class TakePiece : MonoBehaviour
{

    private DragAndDrop dragAndDrop;
    private TurnSwitching turnSwitch;
    private GameOver _gameOver;

    public bool gameOver;

    private void Start()
    {
        dragAndDrop = GetComponent<DragAndDrop>();
        turnSwitch = FindAnyObjectByType<TurnSwitching>();
        _gameOver = FindAnyObjectByType<GameOver>();
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

                    if (collision.gameObject.CompareTag("K"))
                    {
                        gameOver = true;
                        _gameOver.GameOverBlack();
                    }
                }
            }
            else if (!dragAndDrop.isWhite)
            {
                if (collision.gameObject.GetComponent<DragAndDrop>().isWhite)
                {
                    turnSwitch.pieces.Remove(collision.gameObject.GetComponent<DragAndDrop>());
                    Destroy(collision.gameObject.transform.parent.transform.parent.gameObject);
                    
                    if (collision.gameObject.CompareTag("K"))
                    {
                        gameOver = true;
                        _gameOver.GameOverWhite();
                    }
                }
            }
        }
    }

   

}

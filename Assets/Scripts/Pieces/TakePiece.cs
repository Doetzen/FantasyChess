using UnityEngine;

public class TakePiece : MonoBehaviour
{

    private DragAndDrop dragAndDrop;
    private TurnSwitching turnSwitch;
    private GameOver _gameOver;
    private SetPieceToHolder holder;

    public bool gameOver;

    private void Awake()
    {
        dragAndDrop = GetComponent<DragAndDrop>();
        turnSwitch = FindAnyObjectByType<TurnSwitching>();
        _gameOver = FindAnyObjectByType<GameOver>();
        holder = FindAnyObjectByType<SetPieceToHolder>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!this.enabled) return;

        if (collision.gameObject.GetComponent<DragAndDrop>() != null)
        {
            if (dragAndDrop.isWhite != collision.gameObject.GetComponent<DragAndDrop>().isWhite)
            {
                turnSwitch.pieces.Remove(collision.gameObject.GetComponent<DragAndDrop>());
                holder.SetToHolder(collision.gameObject);
               // Destroy(collision.gameObject.transform.parent.transform.parent.gameObject);

                if (collision.gameObject.CompareTag("K"))
                {
                    gameOver = true;
                    if (dragAndDrop.isWhite)
                    {
                        _gameOver.GameOverBlack();
                    }
                    else
                    {
                        _gameOver.GameOverWhite();
                    }
                }
            }
        }
    }

   

}

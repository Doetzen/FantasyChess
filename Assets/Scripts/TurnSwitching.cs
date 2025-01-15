using System.Collections.Generic;
using UnityEngine;

public class TurnSwitching : MonoBehaviour
{
    private bool isWhiteTurn;
    public List<DragAndDrop> pieces = new List<DragAndDrop>();
   

    // Start is called before the first frame update
    void Start()
    {
        isWhiteTurn = true;
        foreach (var piece in pieces)
        {
            if (piece.isWhite == false)
            {
                piece.enabled = false;
                piece.GetComponent<TakePiece>().enabled = false;
            }
        }
    }

    public void SwitchTurn()
    {
        if (isWhiteTurn)
        {
            foreach (var piece in pieces)
            {
                if (piece.isWhite)
                {
                    piece.enabled = false;
                    piece.GetComponent<TakePiece>().enabled = false;
                }
                else
                {
                    piece.enabled = true;
                    piece.GetComponent<TakePiece>().enabled = true;
                }
                
            }
            isWhiteTurn=false;
        }
        else
        {
            foreach (var piece in pieces)
            {
                if (piece.isWhite)
                {
                    piece.enabled = true;
                    piece.GetComponent<TakePiece>().enabled = true;
                }
                else
                {
                    piece.enabled = false;
                    piece.GetComponent<TakePiece>().enabled = false;
                }
                
            }
            isWhiteTurn = true;
        }
    }
}

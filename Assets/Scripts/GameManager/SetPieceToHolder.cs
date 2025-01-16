using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPieceToHolder : MonoBehaviour
{
    [SerializeField]private GameObject[] pieceHolderWhite, pieceHolderBlack;
    private int pieceCountWhite, pieceCountBlack;

    private void Start()
    {
        pieceCountWhite = 0;
        pieceCountBlack = 0;
    }
    public void SetToHolder(GameObject piece)
    {
        if (piece.GetComponent<DragAndDrop>().isWhite)
        {
            piece.transform.parent.transform.parent.transform.SetParent(pieceHolderWhite[pieceCountWhite].transform);
            piece.transform.parent.transform.parent.transform.position = pieceHolderWhite[pieceCountWhite].transform.position;

            piece.GetComponent<DragAndDrop>().enabled = false;
            piece.GetComponent<TakePiece>().enabled = false;
            piece.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            pieceCountWhite++;
        }
        else if(!piece.GetComponent<DragAndDrop>().isWhite)
        {
            piece.transform.parent.transform.parent.transform.SetParent(pieceHolderBlack[pieceCountBlack].transform);
            piece.transform.parent.transform.parent.transform.position = pieceHolderBlack[pieceCountBlack].transform.position;

            piece.GetComponent<DragAndDrop>().enabled = false;
            piece.GetComponent<TakePiece>().enabled = false;
            piece.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            pieceCountBlack++;
        }
    }
}

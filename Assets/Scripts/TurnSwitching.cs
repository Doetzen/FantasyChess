using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSwitching : MonoBehaviour
{
    private bool isWhiteTurn;
    private DragAndDrop[] pieces;
    private void Awake()
    {
        pieces = FindObjectsOfType<DragAndDrop>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isWhiteTurn = true;
        for (int i = 0;i < pieces.Length;i++)
        {
            if (pieces[i].isWhite == false)
            {
                pieces[i].enabled = false;
            }
        }
    }

    public void SwitchTurn()
    {
        if (isWhiteTurn)
        {
            for (int i = 0; i < pieces.Length;i++)
            {
                if (pieces[i].isWhite)
                {
                    pieces[i].enabled = false;
                }
                else
                {
                    pieces[i].enabled = true;
                }
            }
            isWhiteTurn=false;
        }
        else
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                if (pieces[i].isWhite)
                {
                    pieces[i].enabled = true;
                }
                else
                {
                    pieces[i].enabled = false;
                }
            }
            isWhiteTurn = true;
        }
    }
}

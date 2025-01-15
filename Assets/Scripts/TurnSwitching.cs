using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSwitching : MonoBehaviour
{
    private bool isWhiteTurn;
    public List<DragAndDrop> pieces = new List<DragAndDrop>();
    public GameObject cameraWhite, cameraBlack;
    public GameObject[] cameras;
    public float waitForCam;

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
        Cursor.lockState = CursorLockMode.Locked;
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
        StartCoroutine(CameraSwitch());
    }

    public IEnumerator CameraSwitch()
    {
        for (int i = 0;cameras.Length > i; i++)
        {
            cameras[i].SetActive(false);
        }
        int random = Random.Range(0,cameras.Length);
        cameras[random].SetActive(true);
        if (!isWhiteTurn)
        {
            cameraWhite.SetActive(false);
            yield return new WaitForSeconds(waitForCam);
            cameraBlack.SetActive(true);
        }
        else
        {
            cameraBlack.SetActive(false);
            yield return new WaitForSeconds(waitForCam);
            cameraWhite.SetActive(true);
        }
        yield return new WaitForSeconds(waitForCam);
        Cursor.lockState = CursorLockMode.Confined;
    }
}

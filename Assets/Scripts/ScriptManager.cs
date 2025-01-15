using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(StartTurnSwitch());
    }
    public IEnumerator StartTurnSwitch()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<TurnSwitching>().enabled = true;
        gameStarted = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}

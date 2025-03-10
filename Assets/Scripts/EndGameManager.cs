using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public GameObject EndGamePanel;

    public void OnEndDialogue()
    {
        EndGamePanel.SetActive(true);
        Time.timeScale = 0;
    }

}

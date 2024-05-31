using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Youwin : MonoBehaviour
{
    public GameObject YouWinPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger zone.");
            YouWinPanel.SetActive(true);
            // Uncomment this line if you have an audio source component and assign it in the Inspector
            // winAudio.Play();
            Time.timeScale = 0f; // Pause the game
        }
    }

}


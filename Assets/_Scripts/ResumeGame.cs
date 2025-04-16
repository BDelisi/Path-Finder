using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    public PauseMenu pauseMenu;
    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseMenu>();
    }

    public void OnClick()
    {
        pauseMenu.Resume();
    }
}

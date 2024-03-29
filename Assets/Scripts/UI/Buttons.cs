using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

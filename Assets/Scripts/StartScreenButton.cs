using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButton : MonoBehaviour
{

    public GameObject startButton;
    public GameObject quitButton;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckButtonsOnScreenPressed();
    }


    void CheckQuitButtonPressed() {

        if (quitButton.GetComponentInChildren<RectTrigger>().mIsTriggered)
        {
            Application.Quit();
        }
    }

    void CheckStartButtonPressed() {
        if (startButton.GetComponentInChildren<RectTrigger>().mIsTriggered)
        {
            SceneManager.LoadScene(sceneName: "Calirate");
        }

    }

    void CheckButtonsOnScreenPressed() {
        CheckStartButtonPressed();
        CheckQuitButtonPressed();
    }
}

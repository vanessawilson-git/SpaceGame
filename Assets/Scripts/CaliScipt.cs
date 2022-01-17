using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaliScipt : MonoBehaviour
{
    public GameObject DoneButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DoneButton.GetComponentInChildren<RectTrigger>().mIsTriggered)
        {
            SceneManager.LoadScene(sceneName: "RocketNext");
        }

    }
}

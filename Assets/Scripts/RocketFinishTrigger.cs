using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketFinishTrigger : MonoBehaviour
{


    
    public GameObject astroidHolder;
    private Collider2D[] astroids;
 
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = astroidHolder.GetComponent<MoveAstroids>().staticMovement;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Finish")
        {
            SceneManager.LoadScene(sceneName: "TempEndScreen");
        }

    }

}

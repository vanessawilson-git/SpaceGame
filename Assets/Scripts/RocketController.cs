using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketController : MonoBehaviour
{

    public Rigidbody2D rb;
    private float dirX;
    private float dirY;
    private float moveSpeed = 5f;

    public GameObject TopButton;
    public GameObject DownButton;
    public GameObject RightButton;
    public GameObject LeftButton;
    public float speedforce;
    public float maxSpeed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();

        
    }

    // Update is called once per frame
     void Update()
    {

            if (TopButton.GetComponent<RectTrigger>().mIsTriggered)
            {
                if (rb.velocity.magnitude < maxSpeed)
                {
                    rb.AddForce(Vector2.up * speedforce);
                }
            }


            if (DownButton.GetComponent<RectTrigger>().mIsTriggered)
            {
                if (rb.velocity.magnitude < maxSpeed)
                {
                    rb.AddForce(Vector2.down * speedforce);
                }
            }

            if (RightButton.GetComponent<RectTrigger>().mIsTriggered)
            {
                if (rb.velocity.magnitude < maxSpeed)
                {
                    rb.AddForce(Vector2.right * speedforce);
                }
            }


            if (LeftButton.GetComponent<RectTrigger>().mIsTriggered)
            {
                if (rb.velocity.magnitude < maxSpeed)
                {
                    rb.AddForce(Vector2.left * speedforce);
                }
            }

            dirX = Input.GetAxis("Horizontal") * moveSpeed;
            dirY = Input.GetAxis("Vertical") * moveSpeed;


      
      
        var velocity = new Vector2(dirX, dirY);
        rb.position += velocity * Time.deltaTime;


     }



     void OnTriggerEnter2D(Collider2D col)
     {
         if (col.gameObject.tag == "Finish")
         {
             SceneManager.LoadScene(sceneName: "TempEndScreen");
         }

     }




}

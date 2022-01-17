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

        AddRocketKinectMovement();

         //keyoard movement
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

    void AddRocketKinectMovement() {

        AddTopButtonMovement();
        AddDownMovement();
        AddLeftMovement();
        AddRightMovement();
    }

    void AddTopButtonMovement() {

        if (TopButton.GetComponent<RectTrigger>().mIsTriggered)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(Vector2.up * speedforce);
            }
        }
    }

    void AddDownMovement()
    {
        if (DownButton.GetComponent<RectTrigger>().mIsTriggered)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(Vector2.down * speedforce);
            }
        }

    }

    void AddLeftMovement()
    {
        if (LeftButton.GetComponent<RectTrigger>().mIsTriggered)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(Vector2.left * speedforce);
            }
        }

    }

    void AddRightMovement()
    {
        if (RightButton.GetComponent<RectTrigger>().mIsTriggered)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(Vector2.right * speedforce);
            }
        }
    }

}

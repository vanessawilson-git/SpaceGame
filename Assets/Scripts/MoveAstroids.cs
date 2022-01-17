using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveAstroids : MonoBehaviour
{
    private GameObject BaseRocket;
    private Vector2 lastPositionRocket;

    private Vector3 Movement;

    public Vector3 staticMovement;
    public bool UseRandomness;

    private int movementPattern;
    private System.Random rnd = new System.Random();

   
    public GameObject scrollingBackground;

    private Rigidbody2D[] astroids;

    // Start is called before the first frame update
    void Start()
    {
        BaseRocket = GameObject.Find("RocketController");
        RocketController rocket = BaseRocket.GetComponent<RocketController>();
        lastPositionRocket = rocket.rb.position;
        ScrollInfinate scroll = BaseRocket.GetComponent<ScrollInfinate>();

        astroids = GetComponentsInChildren<Rigidbody2D>();


        CheckForRAndomness(rocket, scroll);


    }

    // Update is called once per frame
    void Update()
    {
      

    }

    
    void GiveRandomEffect(Rigidbody2D rb, RocketController rocket, ScrollInfinate scroll )
    {
        movementPattern = rnd.Next(1, 5);

        switch (movementPattern)
        {
            case 1:
            {
                Movement = new Vector3(-1.5f, 0, 0);

                if (rocket.rb.position != lastPositionRocket)
                {
                    rb.velocity = new Vector2(Time.deltaTime * Input.GetAxis("Horizontal") * scroll.scrollSpeed, 0);
                }

                rb.velocity = Movement;
                Debug.Log("option1");
                break;
            }
            case 2:
            {
                Movement = new Vector3(-2, -0.1f, 0);

                if (rocket.rb.position != lastPositionRocket)
                {
                    rb.velocity = new Vector2(Time.deltaTime * Input.GetAxis("Horizontal") * scroll.scrollSpeed , 0);
                }

                rb.velocity = Movement;
                Debug.Log("option2");
                break;
            }
            case 3:
            {
                Movement = new Vector3(-2.5f, 0, 0);

                if (rocket.rb.position != lastPositionRocket)
                {
                    rb.velocity = new Vector2(Time.deltaTime * Input.GetAxis("Horizontal") * scroll.scrollSpeed, 0);
                }

                rb.velocity = Movement;


                Debug.Log("option3");
                break;
            }
            case 4:
            {
                Movement = new Vector3(-1.7f, 0, 0);

                if (rocket.rb.position != lastPositionRocket)
                {
                    rb.velocity = new Vector2(Time.deltaTime * Input.GetAxis("Horizontal") * scroll.scrollSpeed, 0);
                }

                rb.velocity = Movement;
                Debug.Log("option4");
                break;
            }
            case 5:
            {
                Movement = new Vector3(-2.7f, -0.1f, 0);

                if (rocket.rb.position != lastPositionRocket)
                {
                    rb.velocity -= new Vector2(Time.deltaTime * Input.GetAxis("Horizontal") * scroll.scrollSpeed, 0);
                }

                rb.velocity = Movement;
                Debug.Log("option5");
                break;
            }
        }

    }

    void ConsistentAstroidMovement(Rigidbody2D rb, RocketController rocket, ScrollInfinate scroll)
    {
        
        if (rocket.rb.position != lastPositionRocket)
        {
            rb.velocity = new Vector2(Time.deltaTime * Input.GetAxis("Horizontal") * scroll.scrollSpeed, 0);
        }

        rb.velocity = staticMovement;
        //Debug.Log("optionConsistent");

    }



    void CheckForRAndomness(RocketController rocket, ScrollInfinate scroll) {
        if (UseRandomness == true)
        {
            foreach (var rb in astroids)
            {
                GiveRandomEffect(rb, rocket, scroll);
            }
        }
        else
        {
            foreach (var rb in astroids)
            {
                ConsistentAstroidMovement(rb, rocket, scroll);
            }
        }

    }
}

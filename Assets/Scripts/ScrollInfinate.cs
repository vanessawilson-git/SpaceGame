using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;


public class ScrollInfinate : MonoBehaviour
{

    private GameObject BaseRocket;

    private Material material;
    private Vector2 offset;

    public float xVelocity, yVelocity;

    private Vector2 lastPositionRocket;
    public float scrollSpeed;

    void Awake()
    {
        //This makes sure that the background keeps moving, the offset changes causing the scrolling effect
        material = GetComponent<MeshRenderer>().material;

    }

    // Start is called before the first frame update
    void Start()

    {
        BaseRocket = GameObject.Find("RocketController");
        RocketController rocket = BaseRocket.GetComponent<RocketController>();
        lastPositionRocket = rocket.rb.position;
       

    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector2(xVelocity, yVelocity);
        RocketController rocket = BaseRocket.GetComponent<RocketController>();


        AdjustBackgroundScrollOffsetBasedOnRocketSpeed(rocket);



    }

    void AdjustBackgroundScrollOffsetBasedOnRocketSpeed(RocketController rocket) {

        if (rocket.rb.position != lastPositionRocket)
        {
            material.mainTextureOffset += new Vector2(Time.deltaTime * Input.GetAxis("Horizontal") * scrollSpeed, 0);
            material.mainTextureOffset += new Vector2(0, Time.deltaTime * Input.GetAxis("Vertical") * scrollSpeed);
            //Debug.Log("it reached");
        }

        material.mainTextureOffset += offset * Time.deltaTime;
    }
}

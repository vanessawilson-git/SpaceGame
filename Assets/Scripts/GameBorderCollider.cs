using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameBorderCollider : MonoBehaviour
{

    private BorderColliderScript parentScript;

    private List<Collider2D> thingsToIgnore = new List<Collider2D>();

        // Start is called before the first frame update
    void Start()
    {
        IgnoreCollisons();
    }


    // Update is called once per frame
    void Update()
    {
       

    }


    void FillCollisionsToIgnoreList()
    {

        parentScript = GetComponentInParent<BorderColliderScript>();

        foreach (Collider2D col in parentScript.astroidHolder.GetComponentsInChildren<Collider2D>())
        {
            thingsToIgnore.Add(col);
        }

    }


    void IgnoreCollisonFinishLine() {
        thingsToIgnore.Add(parentScript.Finish.GetComponent<Collider2D>());
    }


    void IgnoreCollisons() {

        FillCollisionsToIgnoreList();

        IgnoreCollisonFinishLine();

        foreach (Collider2D col in thingsToIgnore)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col);
        }


    }

}

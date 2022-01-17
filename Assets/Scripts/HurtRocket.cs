using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtRocket : MonoBehaviour
{

    public int damageToGive = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Rocket")
        {
            Vector3 hitDirection = col.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            FindObjectOfType<HealthManager>().HurtRocket(damageToGive, hitDirection);
        }
    }


}

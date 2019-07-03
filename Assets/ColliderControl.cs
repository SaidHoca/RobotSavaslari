using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "blue")
        {
            ArenaScript.instance.CanAzalt("blue");
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "red")
        {
            ArenaScript.instance.CanAzalt("red");
            Destroy(collision.gameObject);
        }
    }
}

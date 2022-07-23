using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallControll : MonoBehaviour
{
    Rigidbody rb;
    private float speed = 5;

   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
   
}

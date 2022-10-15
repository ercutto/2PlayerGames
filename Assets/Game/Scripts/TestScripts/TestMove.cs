using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{
    public class TestMove : MonoBehaviour
    {
        public float speed=10;
        Rigidbody rb;
        float currentRotatiton;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            currentRotatiton = rb.transform.eulerAngles.x;

        }
        void FixedUpdate()
        {

            //Vector3 movement = new Vector3(0.0f, 0.0f, speed * Time.deltaTime).normalized;
            Vector3 movement = rb.transform.forward * speed * Time.deltaTime;
            rb.velocity = movement * 1;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("WayPoints"))
            {
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                rb.transform.eulerAngles += new Vector3(0, 90, 0);
            }
        }
    }

}


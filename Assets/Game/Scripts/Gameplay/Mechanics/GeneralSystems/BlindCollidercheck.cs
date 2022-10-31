using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{

    public class BlindCollidercheck : MonoBehaviour
    {
        private BoxCollider coll;
        private Vector3 aPos, bPos;
        public GameObject[] points;
        private Rigidbody rb;
   
        public float startTime,distance;
        private void Start()
        {
            coll = GetComponent<BoxCollider>();
            aPos = points[0].transform.position;
            bPos = points[1].transform.position;
            rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            Move();
        }
        private void Move()
        {
            Vector3 MovePos = Vector3.Lerp(bPos, aPos, Mathf.PingPong(Time.time * startTime, distance));
            rb.MovePosition(MovePos);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("aPos"))
            {

                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (other.gameObject.CompareTag("bPos"))
            {


                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        }

    }
}


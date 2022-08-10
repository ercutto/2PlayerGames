using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{
    public class Rotate : MonoBehaviour
    {
        public float rotateSpeed;
        Rigidbody rb;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.transform.Rotate(rotateSpeed * Time.deltaTime * transform.up);
        }
    }
}

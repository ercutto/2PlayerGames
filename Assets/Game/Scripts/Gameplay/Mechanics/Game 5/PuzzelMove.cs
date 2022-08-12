using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzelMove : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    private PhotonView pv;
    private bool turn, Ended,stop;
    private float yAngle;
    public GameObject Hold;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        turn = false;
        stop = false;
        Ended = false;
        Hold.SetActive(true);
        if (pv.IsMine)
        {
           yAngle= rb.transform.eulerAngles.y;
           

        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (pv.IsMine)
        {
            if (!stop)
                rb.AddForce(speed * Time.deltaTime * transform.forward);


            if (turn)
            {
                stop = true;
                rb.transform.eulerAngles = new Vector3(0, 90f, 0);
                Invoke(nameof(MoveNow), 5);
                //if (yAngle != 90)
                //{
                //    rb.transform.eulerAngles = new Vector3(0, 90f, 0);

                //}
                //else
                //{

                //    turn = false;
                //    Invoke(nameof(MoveNow), 2);
                //}



            }


        }
    }

    private void MoveNow()
    {
        turn = false;
        stop = false;
        Hold.SetActive(false);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TurnObject"))
        {
            turn = true;
        }
        else if (other.gameObject.CompareTag("EndObject"))
        {
            Destroy(gameObject,3);
        }
    }

}

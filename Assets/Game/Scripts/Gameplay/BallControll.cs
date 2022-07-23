using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallControll : MonoBehaviourPunCallbacks,IPunObservable
{
    Rigidbody rb;
    private float speed = 5;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }

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

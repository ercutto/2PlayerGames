using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersBools : MonoBehaviour,IPunObservable
{
    PhotonView pv;
    public bool isFull;
    public bool interacting;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            isFull = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pv.IsMine) { return; }
        else
        {
            //if(PhotonNetwork.IsMasterClient)
            if (Input.GetMouseButton(0))
            {
                //interacting = true;
                    pv.RPC("Interact", RpcTarget.All, 0);
            }
            if(Input.GetMouseButtonUp(0))
            {
                //interacting = false;
                    pv.RPC("Interact", RpcTarget.All, 1);

            }
        }

            

    }
    [PunRPC]
    void Interact(int _interact)
    {
        if(_interact==0)
        interacting = true;
        else
        {
            interacting = false;

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(interacting);
        }
        else
        {
            interacting = (bool)stream.ReceiveNext();
        }
    }
}

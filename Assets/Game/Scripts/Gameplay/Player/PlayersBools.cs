using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersBools : MonoBehaviour
{
    PhotonView pv;
    public bool isFull;
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
        
    }
}

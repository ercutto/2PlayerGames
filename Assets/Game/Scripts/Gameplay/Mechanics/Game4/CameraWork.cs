using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace TwoPlayersGame
{
    public class CameraWork : MonoBehaviourPun
    {
        private Camera cam;
        private Transform myPos;
        private PhotonView pV;
        private void Start()
        {
            pV = GetComponent<PhotonView>();
            if (pV.IsMine)
            {
                myPos = this.transform;
                cam = Camera.main;
            }
          

        }
        private void Update()
        {
            if (pV.IsMine)
            {
                if (cam) { cam.transform.SetPositionAndRotation(myPos.position, myPos.transform.rotation); }
                else { cam = Camera.main; }
            }
            
        }
    }

}


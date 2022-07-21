using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace TwoPlayersGame
{
    public class PlayerMovement : MonoBehaviourPun
    {
        
        public float speed = 10f;
        public float turnSpeed = 20f;
        PhotonView pV;
        // Start is called before the first frame update
  
        private void Start()
        {
            pV = GetComponent<PhotonView>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (pV.IsMine)
            {
                float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
                transform.Translate(horizontal, 0.0f, vertical);

            }


        }
    }
}


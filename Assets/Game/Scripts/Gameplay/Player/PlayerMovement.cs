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
        private Rigidbody rb;
        // Start is called before the first frame update
  
        private void Start()
        {
            pV = GetComponent<PhotonView>();
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (pV.IsMine)
                if (SceneManagerHelper.ActiveSceneName == "Game")
                {
                    Game1();
                }
                else if(SceneManagerHelper.ActiveSceneName == "Game2")
                {
                    Game2();
                }
                else
                {
                    WaitingRoom();
                }
            
           


        }
        void Game1()
        {
            float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            //transform.Translate(horizontal, 0.0f, vertical);
            rb.AddForce(horizontal, 0, vertical);
        }
        void Game2()
        {
            float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            //float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            //transform.Translate(horizontal, 0.0f, vertical);
            rb.AddForce(horizontal, 0, 0);
        }
        void WaitingRoom()
        {

        }
    }
}


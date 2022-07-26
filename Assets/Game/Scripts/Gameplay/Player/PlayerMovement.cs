using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace TwoPlayersGame
{
    public class PlayerMovement : MonoBehaviourPun     
    {
        
        public float speed = 10f;
        private float turnSpeed = 0.1f;
        PhotonView pV;
        private Rigidbody rb;
        public GameObject PlayerGraphics;
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
            
            rb.AddForce(horizontal, 0, vertical,ForceMode.Force);
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
            if(movement!=Vector3.zero)
            PlayerGraphics.transform.rotation =Quaternion.Slerp(PlayerGraphics.transform.rotation,Quaternion.LookRotation(movement), turnSpeed);
        }
        void Game2()
        {
            float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            //float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            //transform.Translate(horizontal, 0.0f, vertical);
            rb.AddForce(horizontal, 0, 0,ForceMode.Force);
            
        }
        void WaitingRoom()
        {

        }

       
    }
}


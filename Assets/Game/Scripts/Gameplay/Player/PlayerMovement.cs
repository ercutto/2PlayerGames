using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace TwoPlayersGame
{
    public class PlayerMovement : MonoBehaviourPun     
    {
        
        public float speed = 10f;
        public float turnSpeed = 0.1f;
        PhotonView pV;
        private Rigidbody rb;
        public GameObject PlayerGraphics;
        public GameObject hand;
        
        private float smallTilt = 0.02f;
        private float tilt = 0.1f;
        public Animator anim;

  

        private void Start()
        {
            pV = GetComponent<PhotonView>();
            rb = GetComponent<Rigidbody>();
            
        }

    
        void FixedUpdate()
        {
            if (pV.IsMine)
            {

                if (SceneManagerHelper.ActiveSceneName == "Game" || SceneManagerHelper.ActiveSceneName == "Game 3" ||
                       SceneManagerHelper.ActiveSceneName == "Game 4" || SceneManagerHelper.ActiveSceneName == "Game 5")
                {
                    GameOne();
                }
                else if (SceneManagerHelper.ActiveSceneName == "Game2")
                {
                    GameTwo();
                }else if(SceneManagerHelper.ActiveSceneName == "Game 6")
                {
                    GameSix();
                }
                else
                {
                    WaitingRoom();
                }


            


            }
                

            
        }
        #region Movements
        void GameOne()
        {
         

            float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            rb.AddForce(horizontal, 0, vertical,ForceMode.Force);
            
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical).normalized;
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);
                hand.transform.RotateAround(transform.position, new Vector3(0, 2, 0), 0);
               // anim.SetBool("isWalking", true);
                pV.RPC("AnimMod", RpcTarget.All, "isWalking", true);
            }
            else
            {
                pV.RPC("AnimMod", RpcTarget.All, "isWalking", false);
               // anim.SetBool("isWalking", false);
            }
            
            //PlayerGraphics.transform.rotation =Quaternion.Slerp(PlayerGraphics.transform.rotation,Quaternion.LookRotation(movement), turnSpeed);

        }
        void GameTwo()
        {
            rb.transform.position = rb.transform.position;

            float horizontal = Input.GetAxis("Horizontal") * speed* 2 * Time.deltaTime;
 
            rb.AddForce(horizontal, 0, 0,ForceMode.Force);
            Vector3 movement = new Vector3(horizontal, 0.0f, 0.0f).normalized;

            transform.rotation = Quaternion.Euler(0, horizontal * smallTilt, horizontal * smallTilt).normalized;
        }
        void GameSix()
        {
            rb.transform.position = rb.transform.position;
            float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            rb.AddForce(0, vertical, horizontal, ForceMode.Force);
            Vector3 movement = new Vector3(0.0f,vertical,horizontal).normalized;
            
            Quaternion newEulerAngle = Quaternion.Euler(0, 0, 20 + 10);
            transform.rotation = Quaternion.Euler(0,0, vertical * tilt).normalized;
            //Vector3 movement = new Vector3(horizontal, vertical, 0);
            //if (movement != Vector3.zero)
            //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);
            //hand.transform.RotateAround(transform.position, new Vector3(0, 2, 0), 0);

        }
        void WaitingRoom()
        {

        }
        #endregion

        [PunRPC]
        void AnimMod(string boolName,bool TrueOrFalse)
        {
            anim.SetBool(boolName, TrueOrFalse);
        }
    }
}


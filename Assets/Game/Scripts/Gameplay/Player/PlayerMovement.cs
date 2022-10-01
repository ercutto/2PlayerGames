using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace TwoPlayersGame
{
    public class PlayerMovement : MonoBehaviourPun     
    {
  
        public float speed = 10f;
        public float speedMultiplier = 50f;
        public float turnSpeed = 0.1f;
        PhotonView pV;
        private Rigidbody rb;
        public GameObject PlayerGraphics;
        public GameObject hand;
        //ForMoveWithCam
        private Camera cam;
        private Transform myPos;
      
        private float smallTilt = 0.02f;
        private float tilt = 0.1f;
        public Animator anim;
        bool playWalking;
        Vector3 offset = new Vector3(0, 2.3f, 0f);

  

        private void Start()
        {
            pV = GetComponent<PhotonView>();
            if (pV.IsMine)
            {
                rb = GetComponent<Rigidbody>();
                myPos = this.transform;
                cam = Camera.main;

            }
            

        }

    
        void FixedUpdate()
        {
            if (pV.IsMine)
            {

                if (SceneManagerHelper.ActiveSceneName == "Game" || SceneManagerHelper.ActiveSceneName == "Game 3" ||
                        SceneManagerHelper.ActiveSceneName == "Game 5")
                {
                    GameOne();
                }
                else if (SceneManagerHelper.ActiveSceneName == "Game2")
                {
                    GameTwo();
                }
                else if (SceneManagerHelper.ActiveSceneName == "Game 4")
                {
                    MoveWithCamera();
                }
                else if(SceneManagerHelper.ActiveSceneName == "Game 6")
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
        void GameOne()//Game(futboll),Game3(MouseCatch),game5(Factory)
        {
         
            float horizontal = Input.GetAxis("Horizontal") * speed* Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * speed *Time.deltaTime;
            //rb.AddForce(horizontal, 0, vertical,ForceMode.Force);//Player is sliding if we use this!
            rb.velocity = new Vector3(horizontal, 0.0f, vertical);
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical).normalized;
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);
                hand.transform.RotateAround(transform.position, new Vector3(0, 2, 0), 0);
               // anim.SetBool("isWalking", true);
                pV.RPC("AnimMod", RpcTarget.All, "isWalking", true);
                SoundToPlay("event:/GameSounds/StepSound");
            }
            else
            {
                pV.RPC("AnimMod", RpcTarget.All, "isWalking", false);
               // anim.SetBool("isWalking", false);
            }
            
            //PlayerGraphics.transform.rotation =Quaternion.Slerp(PlayerGraphics.transform.rotation,Quaternion.LookRotation(movement), turnSpeed);

        }
        void GameTwo()//CarRace
        {
            rb.transform.position = rb.transform.position;
            float horizontal = Input.GetAxis("Horizontal") * speed* speedMultiplier* Time.deltaTime;
            rb.AddForce(horizontal, 0, 0,ForceMode.Force);// Player is sliding if we use
            Vector3 movement = new Vector3(horizontal, 0.0f, 0.0f).normalized;
            transform.rotation = Quaternion.Euler(0, horizontal * smallTilt, horizontal * smallTilt).normalized;
        }
        void GameSix()
        {
            rb.transform.position = rb.transform.position;
            float horizontal = Input.GetAxis("Horizontal") * speed *speedMultiplier* Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * speed * speedMultiplier* Time.deltaTime;
            //rb.velocity = new Vector3(0.0f,vertical,horizontal).normalized;
            rb.AddForce(0, vertical, horizontal, ForceMode.Force);// Sliding if we use
            Vector3 movement = new Vector3(0.0f,vertical,horizontal).normalized;
            
            Quaternion newEulerAngle = Quaternion.Euler(0, 0, 20 + 10);
            transform.rotation = Quaternion.Euler(0,0, vertical * tilt).normalized;
            //Vector3 movement = new Vector3(horizontal, vertical, 0);
            //if (movement != Vector3.zero)
            //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);
            //hand.transform.RotateAround(transform.position, new Vector3(0, 2, 0), 0);

        }
        void MoveWithCamera()//Dark Game With camera follow
        {
            if (pV.IsMine)
            {
                if (cam) { cam.transform.SetPositionAndRotation(myPos.position+offset, myPos.transform.rotation); }
                else { cam = Camera.main; }

                
                float horizontal = Input.GetAxis("Horizontal") * 150f * Time.deltaTime;
                float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
                Vector3 movement = new Vector3(0.0f, 0.0f, vertical).normalized;
                if (movement != Vector3.zero) {
                    //rb.AddForce(transform.forward * vertical, ForceMode.Force);
                    rb.velocity = transform.forward * vertical;
                    pV.RPC("AnimMod", RpcTarget.All, "isWalking", true);
                    SoundToPlay("event:/GameSounds/StepSound");
                }
                else
                {
                    pV.RPC("AnimMod", RpcTarget.All, "isWalking", false);
                }
                //Vector3 movePos=(rb.transform.forward*vertical)*Time.deltaTime;
                //rb.MovePosition(movePos);
                transform.Rotate(0, horizontal, 0);
            }
        }
        void WaitingRoom()
        {
          //Player is in waiting room and Can not move yet
        }
        #endregion

        [PunRPC]
        void AnimMod(string boolName,bool TrueOrFalse)
        {
            anim.SetBool(boolName, TrueOrFalse);
        }
        void SoundToPlay(string Path) {
            FMODUnity.RuntimeManager.PlayOneShot(Path, GetComponent<Transform>().position);
        }

    }
    
}


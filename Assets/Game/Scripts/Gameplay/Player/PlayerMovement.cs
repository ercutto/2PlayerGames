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

        // Start is called before the first frame update

        private void Start()
        {
            pV = GetComponent<PhotonView>();
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
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
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
            if(movement!=Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);
            hand.transform.RotateAround(transform.position,new Vector3(0, 2, 0), 0);
            //PlayerGraphics.transform.rotation =Quaternion.Slerp(PlayerGraphics.transform.rotation,Quaternion.LookRotation(movement), turnSpeed);

        }
        void GameTwo()
        {
            float horizontal = Input.GetAxis("Horizontal") * speed* 2 * Time.deltaTime;
            //float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            //transform.Translate(horizontal, 0.0f, vertical);
            rb.AddForce(horizontal, 0, 0,ForceMode.Force);
            //Quaternion newEulerAngle = Quaternion.Euler(0, 0 + 0, 20 + 10);
            transform.rotation = Quaternion.Euler(0, horizontal * smallTilt, horizontal * smallTilt).normalized;
        }
        void GameSix()
        {
            float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            rb.AddForce(0, vertical, horizontal, ForceMode.Force);
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

        
    }
}


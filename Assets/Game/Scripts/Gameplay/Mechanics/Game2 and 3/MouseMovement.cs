using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace TwoPlayersGame
{
    public class MouseMovement : MonoBehaviourPun,IPunObservable
    {
        private PhotonView Pv;
        public int scoreValue = 10;
        private GameTwoScore gameTwoScore;
        private Rigidbody rb;
        public bool rotate;
        public byte sceneName;
        public bool ratOnIdle,moving, standing;
        private Animator animator;
        float count;
        // Start is called before the first frame update
        void Start()
        {

     
            Pv = GetComponent<PhotonView>();

            if (Pv.IsMine)
            {
                    count = 0;
                    rb = GetComponent<Rigidbody>();
                    animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
                    ratOnIdle = true;
                    float rand = Random.Range(1, 3);
                    InvokeRepeating(nameof(BoolChange), rand, rand);
                
            }
            
            
            
        }

        // Update is called once per frame
        void Update()
        {

            if (Pv.IsMine)
            {
               
                if (!rotate)
                {
                   
                    


                    Move();
                  

                }
                else { return; }


                if (SceneManagerHelper.ActiveSceneBuildIndex != sceneName)
                {
                    Destroy(gameObject);
                }

              
            }

           

            
            
        }
        void BoolChange()
        {
            if (!ratOnIdle)
            {
                ratOnIdle=true;
            }
            else if(ratOnIdle){ ratOnIdle = false; }
        }
        
        void Move()
        {
            if (ratOnIdle)
            {
                
                MovementAction(0f);
                AnimRun(false);
                AnimIdle(true);

            }
            else if (!ratOnIdle)
            {

                AnimRun(true);
                AnimIdle(false);
                MovementAction(700f);
                

            }

        }
       
        void MovementAction(float speed)
        {
            
            
            Vector3 movementTwo = speed * Time.deltaTime * rb.transform.forward;
            
            rb.velocity = movementTwo*1;

            count += Time.deltaTime;
            float maxTime = Random.Range(2f, 4f);
            if (count >=maxTime)
            {
                int Rand = Random.Range(-1, 1);
                rb.transform.eulerAngles += new Vector3(0,rb.transform.rotation.y + 90,0)*Rand;
                
                count = 0;
            }




        }
       

        void AnimRun(bool moves)
        {
            moving = moves;
            if (moving)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }

        }

        void AnimIdle(bool stand)
        {
            standing = stand;
            if (standing)
            {
                animator.SetBool("Idle", true);
            }
            else
            {
                animator.SetBool("Idle", false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (other.gameObject.CompareTag("Player"))
                {

                    if (other.gameObject.GetComponent<PlayerManager>().FirstOrSecond == 1)
                    {
                        GameObject.Find("BlueScore").GetComponent<CollectPoints>().AddScore(scoreValue);
                        if (Pv.IsMine && !rotate) PhotonNetwork.Destroy(gameObject);
                     


                    }
                    else if (other.gameObject.GetComponent<PlayerManager>().FirstOrSecond == 2)
                    {
                        GameObject.Find("RedScore").GetComponent<CollectPoints>().AddScore(scoreValue);
                        if (Pv.IsMine && !rotate) PhotonNetwork.Destroy(gameObject);
                     

                    }
                    else { return; }
                }
                else if (other.gameObject.CompareTag("Boundry")) PhotonNetwork.Destroy(gameObject);
            }
            else { return; }

          

        }
    
        

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {

                stream.SendNext(ratOnIdle);
                stream.SendNext(moving);
                stream.SendNext(standing);
             



            }
            else
            {

                ratOnIdle = (bool)stream.ReceiveNext();
                moving = (bool)stream.ReceiveNext();
                standing = (bool)stream.ReceiveNext();
            
            }
        }
    }

}

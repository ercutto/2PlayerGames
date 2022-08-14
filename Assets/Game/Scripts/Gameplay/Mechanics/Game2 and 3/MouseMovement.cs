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
        // Start is called before the first frame update
        void Start()
        {

     
                Pv = GetComponent<PhotonView>();
                if (Pv.IsMine)
                {
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
        private void OnTriggerEnter(Collider other)
        {

                if (other.gameObject.CompareTag("Player"))
                {

                    if (other.gameObject.GetComponent<PlayerManager>().FirstOrSecond == 1)
                    {
                        GameObject.Find("BlueScore").GetComponent<CollectPoints>().AddScore(scoreValue);
                        if (Pv.IsMine && !rotate) PhotonNetwork.Destroy(gameObject);
                        //Destroy(gameObject);
                        //string PlayerName = other.gameObject.GetPhotonView().Owner.NickName;

                        //gameTwoScore.BlueName(PlayerName);
                        //gameTwoScore.AddScoreBlue(scoreValue);


                    }
                    else if (other.gameObject.GetComponent<PlayerManager>().FirstOrSecond == 2)
                    {
                        GameObject.Find("RedScore").GetComponent<CollectPoints>().AddScore(scoreValue);
                        if (Pv.IsMine && !rotate) PhotonNetwork.Destroy(gameObject);
                        //string PlayerNameOther = other.gameObject.GetPhotonView().Owner.NickName;
                        //Destroy(gameObject);
                        //gameTwoScore.AddScoreRed(scoreValue);
                        //gameTwoScore.RedName(PlayerNameOther);

                    }
                    else { return; }
                }

                else if (other.gameObject.CompareTag("Boundry")) Destroy(gameObject);



            
           





        }
    
        void Move()
        {
            if (ratOnIdle)
            {
                standing = true;
                moving = false;
                rb.AddForce(-30f * Time.deltaTime * transform.forward);
               
            }
            else if (!ratOnIdle)
            {
                rb.AddForce(700f * Time.deltaTime * transform.forward);
                moving = true;
                standing = false;
           
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

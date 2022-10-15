using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
namespace TwoPlayersGame {
    public class EnemyAI : MonoBehaviour
    {
        public float moveSpeed;
        private NavMeshAgent nav;
        private PhotonView pv;
        bool cooldown;
        private Collider enemyCollider;
        private GameObject[] player;
        private GameObject firstPlayer, secondPlayer;
        private GameObject singlePlayer=null;
        float FirstPlayersDistance, SecondPlayersDistance;



        // Start is called before the first frame update
        void Start()
        {
            pv = GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    cooldown = false;
                    nav = GetComponent<NavMeshAgent>();
                    player = GameObject.FindGameObjectsWithTag("Player");
                    enemyCollider = GetComponent<BoxCollider>();
                    firstPlayer = player[0];
                    secondPlayer = player[1];
                }
               
               
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }
            else
            {
                if (!pv.IsMine) return;
                else
                {
                    if (!cooldown)
                    {
                        if (firstPlayer != null && secondPlayer != null) 
                        { EnemeyLogic(); }
                        else
                        {
                            
                            if (singlePlayer == null)
                            {
                                singlePlayer = GameObject.FindWithTag("Player");
                                
                            }
                            else { SingleMove(); }

                        }
                       
                        
                    }


                }
            }
                
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    enemyCollider.enabled = false;
                    nav.isStopped = true;
                    cooldown = true;

                    if (cooldown)
                    {
                        bool callFuntion = true;
                        if (callFuntion)
                        {
                            Invoke(nameof(CooldownFinished), 5);
                            callFuntion = false;
                        }

                    }

                }
            }
            
        }
        void EnemeyLogic()
        {
            if (nav.isStopped != false) { nav.isStopped = false; }

            FirstPlayersDistance = Vector3.Distance(transform.position,firstPlayer.transform.position);
            SecondPlayersDistance = Vector3.Distance(transform.position, secondPlayer.transform.position);
            if (FirstPlayersDistance < SecondPlayersDistance)
            {
                MovePos(0);

            }
            else if (FirstPlayersDistance > SecondPlayersDistance)
            {
                MovePos(1);

            }
            else { nav.destination = transform.position; }

        }
        void SingleMove()
        {
            nav.isStopped = false; 

            nav.destination = singlePlayer.transform.position;
        }
        void MovePos(int Who)
        {
            nav.destination = player[Who].transform.position;
        }
        void CooldownFinished()
        {
            enemyCollider.enabled = true;
            cooldown = false;
        }
    }
}


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
        private Collider collider;
        private GameObject[] player;
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
                    collider = GetComponent<BoxCollider>();
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
                        nav.isStopped = false;
                        if (player[0] != null)
                        {
                            FirstPlayersDistance = Vector3.Distance(transform.position, player[0].transform.position);
                        }
                        else
                        {
                            nav.destination = player[1].transform.position;
                        }

                        if (player[1] != null)
                        {
                            SecondPlayersDistance = Vector3.Distance(transform.position, player[1].transform.position);
                        }
                        else
                        {
                            nav.destination = player[0].transform.position;
                        }


                        if (FirstPlayersDistance < SecondPlayersDistance)
                        {
                            nav.destination = player[0].transform.position;
                        }
                        else if (FirstPlayersDistance > SecondPlayersDistance)
                        {
                            nav.destination = player[1].transform.position;

                        }
                        else { nav.destination = transform.position; }
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
                    collider.enabled = false;
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
        void CooldownFinished()
        {
            collider.enabled = true;
            cooldown = false;
        }
    }
}


using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TwoPlayersGame
{
    public class PuzzelMove : MonoBehaviour
    {
        public float speed = 1f;
        private Rigidbody rb;
        private PhotonView pv;
        private bool turn, Ended, stop, missionComplated;
        private float yAngle;
        public GameObject Hold;
        private int objectNumber;
        public int score;
        private TogetherWinScore togetherWinScore;
        [SerializeField] 
        bool[] allbools = new bool[] { false,false,false,false,false,false,false,false };
        // Start is called before the first frame update
        void Start()
        {
            pv = GetComponent<PhotonView>();
            rb = GetComponent<Rigidbody>();
            turn = false;
            stop = false;
            Ended = false;
            Hold.SetActive(true);
            if (pv.IsMine)
            {
                yAngle = rb.transform.eulerAngles.y;
                missionComplated = false;
                togetherWinScore = GameObject.Find("Score").GetComponent<TogetherWinScore>();
            }
        }

        void Update()
        {

            if (pv.IsMine)
            {
                if (!stop)
                    rb.AddForce(speed * Time.deltaTime * transform.forward);


                if (turn)
                {
                    stop = true;
                    rb.transform.eulerAngles = new Vector3(0, 90f, 0);
                    Invoke(nameof(MoveNow), 5);
        
                }


            }
        }

        private void MoveNow()
        {
            turn = false;
            stop = false;
            Hold.SetActive(false);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("TurnObject"))
            {
                turn = true;
            }
            else if (other.gameObject.CompareTag("CollectableParts "))
            {   if (!PhotonNetwork.IsMasterClient) return;
                else
                {
                    objectNumber = other.gameObject.GetComponent<CollectableParts>().objectCount;
                    ChangeBool(objectNumber);
                }
               
            }
            else if (other.gameObject.CompareTag("EndObject"))
            {
                if (!PhotonNetwork.IsMasterClient) return;
                else
                    Invoke(nameof(DestroyItself), 7);
                    
            }
        }
        void ChangeBool(int boolToChange)
        {
            allbools[boolToChange] = true;
            togetherWinScore.AddScore(10);
            Debug.Log(allbools[boolToChange].ToString());
            if(allbools[0]&& allbools[1]&& allbools[2] && allbools[3] && allbools[4] && allbools[5] && allbools[6] && allbools[7])
            {
                Debug.Log("Mission Complated");
                togetherWinScore.AddScore(100);
                togetherWinScore.WinMessage();
            }
            else
            {
                return;
            }
            //foreach (bool arraysBool in allbools)
            //{
            //   if(arraysBool==true)
            //    {
            //        missionComplated = true;
            //        if (missionComplated)
            //        {
            //            Debug.Log("Mission Complated");
            //        }
            //    }
            //    else
            //    {
            //        missionComplated = false;
            //        Debug.Log(allbools[0].ToString() + allbools[1].ToString() + allbools[2].ToString() + allbools[3].ToString() + allbools[4].ToString() + allbools[5].ToString() + allbools[6].ToString() + allbools[6].ToString());
            //    }
                
               
            
               
            
                
            //}
           
            
        }
        void DestroyItself()
        {
            PhotonNetwork.Destroy(gameObject);
        }



    }
}


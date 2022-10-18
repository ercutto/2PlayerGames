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
        private bool /*turn,*/ Ended, stop/*, missionComplated*/;
        private float yAngle;

        private GameObject wayPoint;
        private int objectNumber;
        public int score;
        private TogetherWinScore togetherWinScore;
        public GameObject carFrame;
        private int carFrameTimeCount;
        public bool[] allbools = new bool[] { false,false,false,false,false,false,false,false };
        
        void Start()
        {
            pv = GetComponent<PhotonView>();
            rb = GetComponent<Rigidbody>();
            //turn = false;
            stop = false;
            Ended = false;
          
            if (pv.IsMine)
            {
                carFrameTimeCount = 0;
                pv.RPC("SettingActiveOrFalse", RpcTarget.All, false);
                /* missionComplated = false;*/
                togetherWinScore = GameObject.Find("Score").GetComponent<TogetherWinScore>();
            }
        }

        void Update()
        {

            if (pv.IsMine)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    if (!togetherWinScore.begin)
                    {
                        PhotonNetwork.Destroy(gameObject);
                    }
                }


                //if (!stop)
                //{
                    Vector3 movement = speed * Time.deltaTime * rb.transform.forward;
                    rb.velocity = movement * 1;
                //}
                 
               

                
                    
                //if (turn)
                //{
                //    stop = true;                   
                    ////rb.transform.eulerAngles = new Vector3(0, 90f, 0);
                    //Invoke(nameof(MoveNow), 5);
        
                //}


            }
        }
        [PunRPC]
        void SettingActiveOrFalse(bool currentValue)
        {
            carFrame.SetActive(currentValue);
        }
        //private void MoveNow()
        //{
        //    //turn = false;
        //    stop = false;
          

        //}

        private void OnTriggerEnter(Collider other)
        {
            //if (other.gameObject.CompareTag("TurnObject"))
            //{
            //    turn = true;
            //}
            //else if (other.gameObject.CompareTag("CollectableParts "))
            //{   if (!PhotonNetwork.IsMasterClient) { return; }
            //    else
            //    {
            //        objectNumber = other.gameObject.GetComponent<CollectableParts>().objectCount;
            //        ChangeBool(objectNumber);
            //    }

            //}
       
            if (other.gameObject.CompareTag("WayPoints"))
            {
                wayPoint = other.gameObject;
                StartCoroutine(CurerentWayPointCollider());
                carFrameTimeCount++;
                if (carFrameTimeCount >= 2) { pv.RPC("SettingActiveOrFalse", RpcTarget.All, true); }
                
                rb.transform.eulerAngles += new Vector3(0, 90, 0);
            }
            else if (other.gameObject.CompareTag("CollectableParts "))
            {
                if (!PhotonNetwork.IsMasterClient) { return; }
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
            if (!Ended)
            {
                if (allbools[boolToChange] == false)
                {
                    allbools[boolToChange] = true;
                    togetherWinScore.AddScore(10);
                    Debug.Log(allbools[boolToChange].ToString());
                    if (allbools[0] && allbools[1] && allbools[2] && allbools[3] && allbools[4] && allbools[5] && allbools[6] && allbools[7])
                    {
                        Ended = true;
                        Debug.Log("Mission Complated");
                        togetherWinScore.AddScore(100);
                        togetherWinScore.WinMessage();
                    }
                    else
                    {
                        return;
                    }
                }
                

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
        IEnumerator CurerentWayPointCollider()
        {
            wayPoint.GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(7);
            wayPoint.GetComponent<BoxCollider>().enabled = true;
        }
        void DestroyItself()
        {
            PhotonNetwork.Destroy(gameObject);
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(carFrame.activeSelf);

            }
            else
            {
                carFrame.SetActive((bool)stream.ReceiveNext());

            }
        }


    }
}


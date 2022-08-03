using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TwoPlayersGame
{
    public class ScoreCount : MonoBehaviourPun// IPunObservable
    {
        public Text ScoreBoard;
        private int score;
        public Text WinMessage;
        private PhotonView pView;
        private BoxCollider boxCollider;
        public Vector3 ballStartPos;
        public bool Goal;
        public bool canGoal;
        private float count;
        public Color newColor;
        public Color newColorTwo;
        public GameObject[] ColorsToChange;
        private GameObject[] PlayerOnGame;
        public GameObject MasterPlayer;
        public GameObject GuestPlayer;
        public int Side;


        // Start is called before the first frame update
        void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
            Goal = false;
            score = 0;
            pView = GetComponent<PhotonView>();
            ballStartPos = new Vector3(0, 0.5f, 0);
            if (PhotonNetwork.IsMasterClient)
            {
         
                foreach(var item in ColorsToChange)
                {
                    item.GetComponent<Renderer>().material.color = newColor;
                }
                //ScoreBoard.color = newColor;
            }
            else
            {
                foreach (GameObject item in ColorsToChange)
                {
                    item.GetComponent<Renderer>().material.color = newColorTwo;
                }
                //ScoreBoard.color = newColorTwo;
            }
            PlayerOnGame= GameObject.FindGameObjectsWithTag("Player");
            foreach (var curPlayer in PlayerOnGame)
            {
                if (curPlayer.GetComponent<PhotonView>().ViewID == 1001)
                {
                    MasterPlayer = curPlayer;
                }
                if (curPlayer.GetComponent<PhotonView>().ViewID == 2001)
                {
                    GuestPlayer = curPlayer;
                }
            }

        }

        // Update is called once per frame
        void Update()
        {
            count += Time.deltaTime;
            if (count > 2)
            {
                canGoal = true;
            }
            else
            {
                canGoal = false;
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                //boxCollider.enabled = false;
               
                //photonView.RPC("ScoreChange", RpcTarget.All);
                ScoreChange();
            }
        }

       

        //[PunRPC]
        void ScoreChange()
        {
            if (canGoal == false)
            {
                return;
            }
            score++;
            ScoreBoard.text = score.ToString();
            count = 0;
            if (score > 3)
            {
                WinMessage.gameObject.SetActive(true);

                if (Side == 1)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        WinMessage.text = MasterPlayer.GetComponent<PhotonView>().Owner.NickName + " You Lose!";
                    }
                    else
                    {
                        WinMessage.text = GuestPlayer.GetComponent<PhotonView>().Owner.NickName + " You Win!";
                    }
                }else if (Side == 2)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        WinMessage.text = MasterPlayer.GetComponent<PhotonView>().Owner.NickName + " You Win!";
                    }
                    else
                    {
                        WinMessage.text = GuestPlayer.GetComponent<PhotonView>().Owner.NickName + " You Lose!";
                    }
                }
               

            }
            else if(score<3)
            {
                WinMessage.gameObject.SetActive(false);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {

                stream.SendNext(score);
                //stream.SendNext(ballStartPos);
                stream.SendNext(canGoal);
                
            }
            else
            {
                score = (int)stream.ReceiveNext();
                //ballStartPos = (Vector3)stream.ReceiveNext();
                //Goal = (bool)stream.ReceiveNext();
                canGoal=(bool)stream.ReceiveNext(); 
            }
        }
    }
}

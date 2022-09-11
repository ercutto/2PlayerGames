using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TwoPlayersGame
{
    public class ScoreCount : MonoBehaviourPun// IPunObservable
    {
        public Text ScoreBoard,OtherScoreBoard;
        private int score;
        public Text WinMessage;
        private PhotonView pView;
        private BoxCollider boxCollider;
        public Vector3 ballStartPos;
        public bool Goal;
        public bool canGoal;
        private float count;
        private float startTime;
        private float startTimeCount=5;
        private bool gameBegins;
        public Color newColor;
        public Color newColorTwo;
        public GameObject[] ColorsToChange;
        private GameObject[] PlayerOnGame;
        public GameObject MasterPlayer;
        public GameObject GuestPlayer;
        public int Side;
        public Button ContinueButton;
        public string LevelName;
        public GameObject OtherScore;

        // Start is called before the first frame update
        void Start()
        {
            startTime = startTimeCount;
            gameBegins = false;
            boxCollider = GetComponent<BoxCollider>();
            Goal = false;
            score = 0;
            //ScoreReset();
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
                if (curPlayer.GetComponent<PlayerManager>().FirstOrSecond==1)
                {
                    MasterPlayer = curPlayer;
                }
                if (curPlayer.GetComponent<PlayerManager>().FirstOrSecond == 2)
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
            if (!gameBegins)
            {
                return;
            }
            else
            {
                if (startTime >= 0) {
                    startTime-=1 * Time.deltaTime;
                    WinMessage.text = "Will Begin after  Seconds!" + ((int)startTime).ToString();
                    if (startTime <= 0)
                    {
                        WinMessage.text = "";
                        startTime = startTimeCount;
                        gameBegins = false;

                    }
                    
                }
                
              
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                if (pView.IsMine)
                {
                    if (PhotonNetwork.IsMasterClient)
                        pView.RPC("ScoreChange", RpcTarget.All);
                        //ScoreChange();
                    
                }
            }
        }
        [PunRPC]
        void ScoreChange()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (canGoal == false)
                {
                    return;
                }
                //score++;
                pView.RPC("ToScore", RpcTarget.All);
                count = 0;
            }
            
            if (score > 4)
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

                   
                }
                else if (Side == 2)
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
                ContinueGame();
            }
            else if(score<3)
            {
                //WinMessage.gameObject.SetActive(false);
               
            }
        }
        [PunRPC]
        void ToScore()
        {
            score++;
            ScoreBoard.text = score.ToString();
        }
        void CallReset()
        {
            pView.RPC("ScoreReset", RpcTarget.All);
        }
        [PunRPC]
        void ScoreReset()
        {
            WinMessage.gameObject.SetActive(true);
            score = 0;
            ScoreBoard.text = score.ToString();
            OtherScore.GetComponent<ScoreCount>().score = 0;
            OtherScoreBoard.text = score.ToString();
            gameBegins = true;
        }
        public void ContinueGame()
        {
            if(PhotonNetwork.IsMasterClient)
            Invoke(nameof(CallReset),3);
   
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(startTime);
                stream.SendNext(score);
                //stream.SendNext(ballStartPos);
                stream.SendNext(canGoal);
                stream.SendNext(gameBegins);
                stream.SendNext(ContinueButton.gameObject.activeInHierarchy);
            }
            else
            {
                score = (int)stream.ReceiveNext();
                startTime = (float)stream.ReceiveNext();
                //ballStartPos = (Vector3)stream.ReceiveNext();
                //Goal = (bool)stream.ReceiveNext();
                canGoal =(bool)stream.ReceiveNext();
                gameBegins =(bool)stream.ReceiveNext();


            }
        }
    }
}

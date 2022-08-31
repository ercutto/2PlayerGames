using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
namespace TwoPlayersGame {
    public class CollectPoints : MonoBehaviour//,IPunObservable
    {
        public Text scoreText;
        private GameObject[] Players;
        public string playerNick;
        public int score;
        public Text WinMessage;
        public int OnGameSide;
        private PhotonView PView;
        public int maxScore = 100;
        //For restart
        public SpawnGameObjects spawnGameObjects;
        public bool restart;
        public GameObject OthersScoreBoard;
        
        // Start is called before the first frame update
        void Start()
        {
            PView = GetComponent<PhotonView>();
            Players = GameObject.FindGameObjectsWithTag("Player");
            restart = false;
            foreach (var item in Players)
            {
               int pNumber= item.GetComponent<PlayerManager>().FirstOrSecond;
                if (pNumber==OnGameSide)
                {
                    playerNick= item.GetComponent<PhotonView>().Owner.NickName;
                   
                }
            }
           

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void AddScore(int addscore)
        {
            if (spawnGameObjects.begin) {
                score += addscore;
                scoreText.text = score.ToString();
                //WinMessage.text = playerNick + " Scores";
                if (score >= maxScore)
                {

                    Debug.Log("Winer is Red + ");
                    spawnGameObjects.begin = false;
                    PView.RPC("WinnerMessage", RpcTarget.All, playerNick);
                    restart = true;

                    WakeResetMethod();
                    OthersScoreBoard.GetComponent<CollectPoints>().WakeResetMethod();
                }
                else
                {
                    restart = false;
                }

                PView.RPC("DisplayValues", RpcTarget.All, score);
                
            }
            
            
        }
        public void WakeResetMethod()
        {
            Invoke(nameof(ResetScore), 2);
        }
        void ResetScore()
        {
            score -= score;
            PView.RPC("DisplayValues", RpcTarget.All, score);
            restart = true;
            spawnGameObjects.restartButton.SetActive(true);
        }
        [PunRPC]
        void DisplayValues(int scores)
        {
            score = scores;
            scoreText.text = score.ToString();
        }
        [PunRPC]
        void WinnerMessage(string playername)
        {
            WinMessage.text = playername + ": "+OnGameSide+" Is winner :)";
            Invoke(nameof(WinMessageReset),3);

        }
        void WinMessageReset()
        {
            WinMessage.text = "";
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {

                stream.SendNext(score);



            }
            else
            {
                score = (int)stream.ReceiveNext();
            }
        }
       
    }
}


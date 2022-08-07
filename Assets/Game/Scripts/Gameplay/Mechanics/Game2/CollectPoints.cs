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
        // Start is called before the first frame update
        void Start()
        {
            PView = GetComponent<PhotonView>();
            Players = GameObject.FindGameObjectsWithTag("Player");
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

            score += addscore;
            scoreText.text = score.ToString();
            //WinMessage.text = playerNick + " Scores";
            //if (score > 10)
            //{

            //    Debug.Log("Winer is Red + ");
            //    WinMessage.text = playerNick + " Is winner :)";
            //}

                PView.RPC("DisplayValues", RpcTarget.All, score);
            
        }
        [PunRPC]
        void DisplayValues(int scores)
        {
            score = scores;
            scoreText.text = score.ToString();
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


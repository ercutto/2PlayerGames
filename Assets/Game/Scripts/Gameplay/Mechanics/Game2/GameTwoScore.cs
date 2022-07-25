using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
namespace TwoPlayersGame
{
    
    public class GameTwoScore : MonoBehaviourPunCallbacks//,IPunObservable
    {
        private PhotonView Pv;
        public Text blueScore,blueText;
        public Text RedScore,redText;
        private string bluePlayerName;
        private string redPlayerName;
        private int score;
        int BScore;
        int RScore;
        private void Start()
        {
            Pv = GetComponent<PhotonView>();
            BScore = 0;
            RScore = 0;
        }

        public void AddScoreBlue(int addscore)
        {
            BScore += addscore;
            blueScore.text = BScore.ToString();

            if (BScore > 10)
            {
                Debug.Log("Winer is ");
            }
        }
        
        public void AddScoreRed(int addscore)
        {
            RScore += addscore;
            RedScore.text = RScore.ToString();
            if (RScore > 10)
            {
                Debug.Log("Winer is Red");
            }

        }
        public void BlueName(string blue)
        {
            bluePlayerName = blue;
            blueText.text = bluePlayerName;
        }
        public void RedName(string Red)
        {
            redPlayerName = Red;
            redText.text = redPlayerName;
        }
       
        //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        //{
        //    if (stream.IsWriting)
        //    {

        //        stream.SendNext(BScore);
        //        stream.SendNext(RScore);
        //        stream.SendNext(bluePlayerName);
        //        stream.SendNext(redPlayerName);



        //    }
        //    else
        //    {
        //        //RScore = (int)stream.ReceiveNext();
        //        //BScore = (int)stream.ReceiveNext();
        //        bluePlayerName = (string)stream.ReceiveNext();
        //        redPlayerName = (string)stream.ReceiveNext();

        //    }
        //}

    }

}


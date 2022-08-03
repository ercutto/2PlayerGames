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
        
        
        public Text WinMessage;
        private int score;
        int BScore;
        int RScore;
        private void Start()
        {
            Pv = GetComponent<PhotonView>();
            BScore = 0;
            RScore = 0;
            WinMessage.text = "Start";
        }

        public void AddScoreBlue(int addscore)
        {
            BScore += addscore;
            blueScore.text = BScore.ToString();
            WinMessage.text = bluePlayerName + "Scores";
            if (BScore > 10)
            {
                Debug.Log("Winer is ");
                WinMessage.text = bluePlayerName + "Is winner";

            }
        }
        
        public void AddScoreRed(int addscore)
        {
            RScore += addscore;
            RedScore.text = RScore.ToString();
            WinMessage.text = redPlayerName + "Scores";
            if (RScore > 10)
            {
                
                Debug.Log("Winer is Red + ");
                WinMessage.text = redPlayerName + "Is winner";
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
      

    }

}


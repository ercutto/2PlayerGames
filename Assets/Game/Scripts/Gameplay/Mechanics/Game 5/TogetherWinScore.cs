using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace TwoPlayersGame
{
    public class TogetherWinScore : MonoBehaviour,IPunObservable
    {
        public Text score,WinMessageText,ClockText;
        public GameObject restartButton, leaveGameModbutton;
        private int scoreValue=0;
        private string winMessage = "Awesome teamwork!";
        private string winMessageReset = "";
        public string ClockTime;
        public float timeCount = 0.0f;
        public float Minutes = 0.0f;
        public float GameTime = 3f;
        public float seconds;
        public bool begin;
        private PhotonView pv;
        // Start is called before the first frame update
        void Start()
        {
            pv = GetComponent<PhotonView>();
            
            if (PhotonNetwork.IsMasterClient)
            {
                begin = true;
                leaveGameModbutton.SetActive(true);
                
                
            }
            else
            {
                leaveGameModbutton.SetActive(false);
            }

            restartButton.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            //if (!PhotonNetwork.IsMasterClient)
            //{
            //    return;
            //}
            //else
           // {
            if (begin)
            {
                    
                Clock();
            }
            else
            {
                if (!PhotonNetwork.IsMasterClient) return;
                else restartButton.SetActive(true);

            }
                
           // }

            ClockText.text = ClockTime;
        }
       
        void Clock()
        {
            timeCount += 1 * Time.deltaTime;
            if (timeCount >= 1)
            {
                seconds++;
                timeCount = 0.0f;
                if (seconds >= 60)
                {
                    Minutes++;
                    seconds = 0.0f;
                    if (Minutes >= GameTime)
                    {
                        
                        begin = false;
                        ClockTime = "Time is over!";
                        
                    }
                }
            }
            ClockTime = Minutes.ToString() + ":" + seconds.ToString() + ":" + timeCount.ToString("0,0");
        }
        public void OnRestartButtonClicked()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
                
            }
            pv.RPC("Restart", RpcTarget.All);
        }
        [PunRPC]
        void Restart()
        {
            timeCount = 0;
            seconds = 0;
            Minutes = 0;
            begin = true;
        }
        public void AddScore(int add)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }
            pv.RPC("ShowChangesToOther", RpcTarget.All,add);
        }
        [PunRPC]
        void ShowChangesToOther(int toAdd)
        {
            scoreValue += toAdd;
            score.text = scoreValue.ToString();
        }

        public void WinMessage()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }
            pv.RPC("ShowWinMessage", RpcTarget.All);
        }
        [PunRPC]
        void ShowWinMessage()
        {
            WinMessageText.text = winMessage;
            if (!PhotonNetwork.IsMasterClient) return;
            else Invoke(nameof(ResetMessage), 3);
        }
        void ResetMessage()
        {
            pv.RPC("SetWinmessageEmpty", RpcTarget.All);

        }
        [PunRPC]
        void SetWinmessageEmpty()
        {
            WinMessageText.text = winMessageReset;
        }
        

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(scoreValue);
                stream.SendNext(winMessage);
                stream.SendNext(ClockTime);
                stream.SendNext(begin);
            }
            else
            {
                scoreValue = (int)stream.ReceiveNext();
                winMessage = (string)stream.ReceiveNext();
                ClockTime = (string)stream.ReceiveNext();
                begin = (bool)stream.ReceiveNext();
            }
        }
    }
}


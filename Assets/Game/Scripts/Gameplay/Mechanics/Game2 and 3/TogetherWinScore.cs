using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace TwoPlayersGame
{
    public class TogetherWinScore : MonoBehaviour,IPunObservable
    {
        public Text score,WinMessageText;
        private int scoreValue=0;
        private string winMessage = "Awesome teamwork!";
        private string winMessageReset = "";
        private PhotonView pv;
        // Start is called before the first frame update
        void Start()
        {
            pv = GetComponent<PhotonView>();
        }

        // Update is called once per frame
        void Update()
        {

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
            pv.RPC("SetWinmessageBack", RpcTarget.All);

        }
        [PunRPC]
        void SetWinmessageBack()
        {
            WinMessageText.text = winMessageReset;
        }
        

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(scoreValue);
                stream.SendNext(winMessage);
            }
            else
            {
                scoreValue = (int)stream.ReceiveNext();
                winMessage = (string)stream.ReceiveNext();
            }
        }
    }
}


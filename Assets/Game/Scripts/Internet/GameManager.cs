using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

namespace TwoPlayersGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("StartScene");
        }
        public void LeaveRoom()
        {
            Debug.Log("<color=orange>You are Leaving the Room: </color>" + PhotonNetwork.CurrentRoom.Name);
            PhotonNetwork.LeaveRoom();
            
        }
    }
}


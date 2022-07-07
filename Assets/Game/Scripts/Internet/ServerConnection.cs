using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

namespace TwoPlayersGame
{
    public class ServerConnection : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
          
            PhotonNetwork.ConnectUsingSettings();
        }
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            SceneManager.LoadScene("Lobby");
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            PhotonNetwork.JoinLobby();
        }
       
    }
}


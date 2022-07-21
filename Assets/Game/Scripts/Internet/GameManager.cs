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
            Debug.LogFormat("<color=orange>GameManager: </color><color=yellow>You are Leaving the Room:{0} </color>",PhotonNetwork.CurrentRoom.Name.ToString());
            PhotonNetwork.LeaveRoom();
            
        }
        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("<color=orange> GameManager : </color> You cannot load a level unless you are a master client!");
            }
            Debug.LogFormat("<color=orange>GameManager : </color><color=green>loading Level: {0}</color>" , PhotonNetwork.CurrentRoom.PlayerCount);
        }
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEntered() {0}", other.NickName);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("<color=orange>GameManager: </color> <color=aqua>OnPlayerEneteredRoom IsMAsterClient {0}</color>", PhotonNetwork.IsMasterClient);
                LoadArena();
            }
        }

    }
}


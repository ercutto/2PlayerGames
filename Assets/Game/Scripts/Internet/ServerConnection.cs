using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace TwoPlayersGame
{
    public class ServerConnection : MonoBehaviourPunCallbacks
    {
        public string gameVersion = "0,0,1";
        [Tooltip("The maximum number of players per room. When a room is full," +
         " it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        public byte maxPlayer = 2;

        public GameObject playButtonCanvas;
        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        private void Start()
        {
            
        }
        public void Connect()
        {
            playButtonCanvas.gameObject.SetActive(false);
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;   
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("  We coud Connected to <color=yellow>Master! </color>"+ "<color=aqua> gameVersion: </color>"+gameVersion);
            PhotonNetwork.JoinRandomRoom();
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            Debug.Log("<color=orange>ServerConnection: </color><color=red> No Room available! </color> <color=yellow> We Create One </color>");

            //Make I function and call it with button input
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayer });
        }
        public override void OnJoinedRoom()
        {

            Debug.Log("<color=orange>ServerConnection: </color><color=lime> Player is in Room</color> <color=teal>MaxPlayer: </color>"+ maxPlayer);
        }
    }
}


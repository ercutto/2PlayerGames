using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace TwoPlayersGame
{
    public class ServerConnection : MonoBehaviourPunCallbacks
    {
        private bool isConnecting;
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
            if (playButtonCanvas!=null) { playButtonCanvas.gameObject.SetActive(false); }
            
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
           
        }
        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                Debug.LogFormat("<color=orange>{0} : </color> <color=green>We coud Connected to </color> <color=yellow> Master! </color>" + "<color=aqua> gameVersion: {1} </color>", name, gameVersion);
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
           
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

            Debug.LogFormat("<color=orange>ServerConnection: </color><color=lime> Player is in Room</color> <color=teal>MaxPlayer: {0}</color>", maxPlayer);
            if (PhotonNetwork.CurrentRoom.PlayerCount==1)
            {
                Debug.Log("<color=orange> ServerConnection:</color> <color=aqua>We load the Room for 1</color>");
                PhotonNetwork.LoadLevel("Room for 1");
            }
            
        }
    }
}


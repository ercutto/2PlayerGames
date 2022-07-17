using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Photon.Pun;
using Photon.Realtime;

namespace TwoPlayersGame
{
    
    public class PlayerVariables : MonoBehaviour
    {
        public InputField _inputField;
        const string playerNamePrefKey = "PlayerName";
        // Start is called before the first frame update
        void Start()
        {

            string defaultName = string.Empty;
            
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }


            PhotonNetwork.NickName = defaultName;
        }
        public void SetPlayerName(string value)
        {
            //we can check here name if its slang or not valid
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value;


            PlayerPrefs.SetString(playerNamePrefKey, value);

        }
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace TwoPlayersGame
{
    public class ColorChanging : MonoBehaviour
    {
        public Material colorRed;
        public Material colorblue;
        public Text blueText;
        public Text RedText;

        // Start is called before the first frame update
        void Awake()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                blueText.material = colorblue;
                RedText.material = colorRed;

            }
            else
            {
                blueText.material = colorRed;
                RedText.material = colorblue;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace TwoPlayersGame
{
    
    public class GameTwoScore : MonoBehaviour
    {
        public int coinValue = 10;
      
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                
            }
        }
    }

}


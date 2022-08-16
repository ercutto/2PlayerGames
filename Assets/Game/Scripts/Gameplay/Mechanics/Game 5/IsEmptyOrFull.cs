using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{
    public class IsEmptyOrFull : MonoBehaviour
    {
        public bool IsEmpty=false;
        public int PuzzlesNumber;
        private void Start()
        {
            IsEmpty = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "CollectableParts") { IsEmpty = false; }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name=="CollectableParts") { IsEmpty = true; }
        }
    }
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace TwoPlayersGame
{
    public class IsEmptyOrFull : MonoBehaviour,IPunObservable
    {
        public bool IsEmpty;
        public int PuzzlesNumber;
        private void Start()
        {
            IsEmpty = true;
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.CompareTag("CollectableParts ")) { IsEmpty = false; }
        //}
        //private void OnTriggerStay(Collider other)
        //{
        //    if (other.gameObject.CompareTag("CollectableParts ")) { IsEmpty = false; }
        //}
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("CollectableParts ")) { Invoke(nameof(SayImEmpty), 3); }
        }
        void SayImEmpty()
        {
            IsEmpty = true;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(IsEmpty);
            }
            else
            {
                IsEmpty = (bool)stream.ReceiveNext();
            }
        }
    }
    
}


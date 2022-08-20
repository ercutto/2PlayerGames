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
        private PhotonView photonView;
        
        [SerializeField]
        private float RepeateRate;
        public float _repeateRate//protecttion
        {
            
            get { return RepeateRate; }
            set
            {
               RepeateRate = value;
               
            }
        }
        private void Start()
        {
            photonView = GetComponent<PhotonView>();
            if (photonView.IsMine)
            {
                IsEmpty = true;
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (photonView.IsMine)
            {
                if (other.gameObject.CompareTag("CollectableParts ")) { Invoke(nameof(SayImEmpty),RepeateRate); }
            }
            
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


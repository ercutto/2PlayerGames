using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace TwoPlayersGame
{
    public class BallControll : MonoBehaviour//, IPunObservable
    {
        Vector3 _networkPosition;
        Quaternion _networkRotation;
        Rigidbody _rb;
        private float kickForce = 1f;
        private PhotonView photonView;
        public AudioSource audioSource;
        public AudioClip bounceClip;



        void Start()
        {
            photonView = GetComponent<PhotonView>();
            _rb = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            if (!photonView.IsMine)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    _rb.position = Vector3.MoveTowards(_rb.position, _networkPosition, Time.deltaTime);
                    _rb.rotation = Quaternion.RotateTowards(_rb.rotation, _networkRotation, Time.deltaTime * 100.0f);
                }
               
            }
        }
        

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Player"))
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    Vector3 direction = (other.transform.position - transform.position).normalized;
                    _rb.AddForce(-direction * kickForce, ForceMode.Impulse);
                   
                }


                if (!audioSource.isPlaying)
                {
                    PlayFx();
                }
            }
            
        }
        void PlayFx()
        {
            audioSource.PlayOneShot(bounceClip);
        }
        //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        //{
        //    if (stream.IsWriting)
        //    {
        //        stream.SendNext(_rb.position);
        //        stream.SendNext(_rb.rotation);
        //        stream.SendNext(_rb.velocity);
        //    }
        //    else
        //    {
        //        _networkPosition = (Vector3)stream.ReceiveNext();
        //        _networkRotation = (Quaternion)stream.ReceiveNext();
        //        _rb.velocity = (Vector3)stream.ReceiveNext();

        //        float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTimestamp));
        //        _networkPosition += (_rb.velocity * lag);
        //    }

        //}
    }

}



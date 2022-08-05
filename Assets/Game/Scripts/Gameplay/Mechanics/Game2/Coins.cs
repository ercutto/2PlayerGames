using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{
    public class Coins : MonoBehaviour//IPunObservable
    {
        private PhotonView Pv;
        public int scoreValue = 10;
        private GameTwoScore gameTwoScore;
        private Rigidbody rb;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            Pv = GetComponent<PhotonView>();
            gameTwoScore = GameObject.Find("GamesPrivateSystem").GetComponent<GameTwoScore>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.AddForce(transform.forward * Time.deltaTime * 100f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetPhotonView().ViewID == 1001)
                {
                    string PlayerName = other.gameObject.GetPhotonView().Owner.NickName;
                    gameTwoScore.BlueName(PlayerName);
                    gameTwoScore.AddScoreBlue(10);

                }
                else if (other.gameObject.GetPhotonView().ViewID == 2001)
                {
                    string PlayerNameOther = other.gameObject.GetPhotonView().Owner.NickName;
                   
                    gameTwoScore.AddScoreRed(10);
                    gameTwoScore.RedName(PlayerNameOther);
                }
                else { return; }
            }
        }
        
        //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        //{
        //    if (stream.IsWriting)
        //    {

        //        stream.SendNext(scoreValue);
                
               

        //    }
        //    else
        //    {
        //        scoreValue = (int)stream.ReceiveNext();
            
        //    }
        //}
    }
   
}


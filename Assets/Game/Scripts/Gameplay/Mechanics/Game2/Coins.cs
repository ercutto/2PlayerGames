using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{
    public class Coins : MonoBehaviour,IPunObservable
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
                if (other.gameObject.GetComponent<PlayerManager>().FirstOrSecond == 1)
                {
                    string PlayerName = other.gameObject.GetPhotonView().Owner.NickName;

                    gameTwoScore.BlueName(PlayerName);
                    gameTwoScore.AddScoreBlue(scoreValue);
                    if (Pv.IsMine) PhotonNetwork.Destroy(gameObject);

                }
                else if (other.gameObject.GetComponent<PlayerManager>().FirstOrSecond == 2)
                {
                    string PlayerNameOther = other.gameObject.GetPhotonView().Owner.NickName;

                    gameTwoScore.AddScoreRed(scoreValue);
                    gameTwoScore.RedName(PlayerNameOther);
                    if (Pv.IsMine) PhotonNetwork.Destroy(gameObject);
                }
                else { return; }
               

            }
            
            
           
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {

                stream.SendNext(scoreValue);



            }
            else
            {
                scoreValue = (int)stream.ReceiveNext();

            }
        }
    }
   
}


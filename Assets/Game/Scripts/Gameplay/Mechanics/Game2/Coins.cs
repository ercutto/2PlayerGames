using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{
    public class Coins : MonoBehaviour//,IPunObservable
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
            //gameTwoScore = GameObject.Find("GamesPrivateSystem").GetComponent<GameTwoScore>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.AddForce(100f * Time.deltaTime * transform.forward);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (other.gameObject.CompareTag("Player"))
                {

                    if (other.gameObject.GetComponent<PlayerManager>().FirstOrSecond == 1)
                    {
                        GameObject.Find("BlueScore").GetComponent<CollectPoints>().AddScore(scoreValue);
                        if (Pv.IsMine) PhotonNetwork.Destroy(gameObject);
                        //Destroy(gameObject);
                        //string PlayerName = other.gameObject.GetPhotonView().Owner.NickName;

                        //gameTwoScore.BlueName(PlayerName);
                        //gameTwoScore.AddScoreBlue(scoreValue);


                    }
                    else if (other.gameObject.GetComponent<PlayerManager>().FirstOrSecond == 2)
                    {
                        GameObject.Find("RedScore").GetComponent<CollectPoints>().AddScore(scoreValue);
                        if (Pv.IsMine) PhotonNetwork.Destroy(gameObject);
                        //string PlayerNameOther = other.gameObject.GetPhotonView().Owner.NickName;
                        //Destroy(gameObject);
                        //gameTwoScore.AddScoreRed(scoreValue);
                        //gameTwoScore.RedName(PlayerNameOther);

                    }
                    else { return; }
                }

                else if (other.gameObject.CompareTag("Boundry")) Destroy(gameObject);



            }
            else
            {
                return;
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

        //}
        //}
    }

}


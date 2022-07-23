using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TwoPlayersGame
{
    public class ScoreCount : MonoBehaviour// IPunObservable
    {
        public Text ScoreBoard;
        private int score;
        private PhotonView photonView;
        private BoxCollider boxCollider;
        public Vector3 ballStartPos;
        private bool Goal;
      
        // Start is called before the first frame update
        void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
            Goal = false;
            score = 0;
            photonView = GetComponent<PhotonView>();
            ballStartPos = new Vector3(0, 0.5f, 0);
        }

        // Update is called once per frame
        void Update()
        {


           


        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                if (Goal==false)
                {
                    photonView.RPC("ScoreChange", RpcTarget.All);
                    Goal = true;
                }
                //boxCollider.enabled = false;
                

                //other.gameObject.transform.position = new Vector3(0, 0.5f, 0);
               
                
                StartCoroutine(CollisionCheck());
            }
        }
     
        IEnumerator CollisionCheck()
        {
           
            yield return new WaitForSeconds(2);
            //boxCollider.enabled = true;
            Goal = false;
        }

        [PunRPC]
        void ScoreChange()
        {
            score++;
            ScoreBoard.text = score.ToString();
        }

        //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        //{
        //    if (stream.IsWriting)
        //    {
        //        //stream.SendNext(score);
        //        stream.SendNext(ballStartPos);



        //    }
        //    else
        //    {
        //        //score = (int)stream.ReceiveNext();
        //        ballStartPos = (Vector3)stream.ReceiveNext();

        //    }
       // }
    }
}

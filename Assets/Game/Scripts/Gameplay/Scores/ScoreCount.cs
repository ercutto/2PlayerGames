using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public Text ScoreBoard;
    public int score;
    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            photonView.RPC("Mes", RpcTarget.All);
            ScoreBoard.text = score++.ToString();
        }
    }
    [PunRPC]
    void Mes()
    {
        ScoreBoard.text = score++.ToString();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace TwoPlayersGame
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        public static GameObject localPlayerInstance;
        public  Color myColor;
        private int playerLayer=15;
        private int ignoreLayer=20;
        public int FirstOrSecond;
        // Start is called before the first frame update
        public void Awake()
        {
            if (photonView.IsMine)
            {
                PlayerManager.localPlayerInstance = this.gameObject;
                gameObject.GetComponentInChildren<Renderer>().material.color = myColor;
            }
            DontDestroyOnLoad(this.gameObject);
        }
        void Start()
        {
           
#if UNITY_5_4_OR_NEWER
            // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
#if !UNITY_5_4_OR_NEWER
        <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
#endif
#if UNITY_5_4_OR_NEWER
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }
#endif
        void CalledOnLevelWasLoaded(int level)
        {
            // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
            //float randx = Random.Range(0, 3);
            //float randy = Random.Range(0, 3);
            //transform.position = new Vector3(randx, 0, randy);
            SwitchPosForGame(level);
        }

#if UNITY_5_4_OR_NEWER
        public override void OnDisable()
        {
            // Always call the base to remove callbacks
            base.OnDisable();
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void SwitchPosForGame(int SceneNumber)
        {
            switch (SceneNumber)
            {
                case 1:
                    gameObject.layer = playerLayer;
                    if (PhotonNetwork.IsMasterClient) PosTransform(2, 0, 0);
                    else PosTransform(-2, 0, 0);
                    break;
                case 5:
                    gameObject.layer = playerLayer;
                    if (PhotonNetwork.IsMasterClient) PosTransform(2, 0, 0);
                    else PosTransform(-2, 0, 0);
                    break;
                case 6:
                    gameObject.layer = ignoreLayer;
                    if (PhotonNetwork.IsMasterClient) PosTransform(2, 0, 0);
                    else PosTransform(-2, 0, 0);
                    break;
                default:
                    break;

            }
        }
        private void PosTransform(float xT, float yT,float zT)
        {
            transform.position = new Vector3(xT, yT, zT);
        }
#endif
    }
   



}


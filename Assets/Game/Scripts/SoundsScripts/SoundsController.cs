using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{

    public class SoundsController : MonoBehaviour
    {
        //public static SoundsController _soundsControllerInstance;
        

        public AudioSource audioSourceFx,audioSourceBg;
        public AudioClip clickSound,bgSound;
        private float Volume;
        // Start is called before the first frame update
        void Start()
        {
            //_soundsControllerInstance = this;
          
        }

        // Update is called once per frame
        void Update()
        {
            if (!audioSourceBg.isPlaying)
            {
                BgToPlay();
                
               
            }

           
        }
        public void FxToPlay()
        {
            PlayFx(clickSound);
        }
        void PlayFx(AudioClip clip)
        {
            audioSourceFx.PlayOneShot(clip);
          
           
        }
        public void BgToPlay()
        {

            PlayBg(bgSound);

        }
        void PlayBg(AudioClip clip)
        {
            audioSourceBg.PlayOneShot(clip);
           
           
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TwoPlayersGame
{
    public class ShaderController : MonoBehaviour
    {
        public float glitchChange = .1f;
        private Renderer HaloRenderer;
        private WaitForSeconds glitchLoopWait = new WaitForSeconds(.1f);
        private WaitForSeconds glitchDuration = new WaitForSeconds(.1f);
        // Start is called before the first frame update
        void Awake()
        {
            HaloRenderer = GetComponent<Renderer>();
        }
        IEnumerator Start()
        {
            while (true)
            {
                float glichTest = Random.Range(0f, 1f);
                if (glichTest <= glitchChange)
                {
                    StartCoroutine(Glitch());
                }
                yield return glitchLoopWait;
            }
        }
     
        IEnumerator Glitch()
        {
            glitchDuration=new WaitForSeconds(Random.Range(.05f, .25f));
            HaloRenderer.material.SetFloat("_Amount", 1f);
            HaloRenderer.material.SetFloat("_CutoutThresh", .29f);
            HaloRenderer.material.SetFloat("_Amplitude", Random.Range(100,250));
            HaloRenderer.material.SetFloat("_Speed", Random.Range(1,10));
            yield return glitchDuration;
            HaloRenderer.material.SetFloat("_Amount",0f);
            HaloRenderer.material.SetFloat("_CutoutThresh",0f);

        }
    }
}



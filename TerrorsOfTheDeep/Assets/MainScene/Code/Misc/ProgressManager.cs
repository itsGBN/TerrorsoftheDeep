using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace JustFish
{
    public class ProgressManager : MonoBehaviour
    {
        public bool glitchSprite, glitchDialogue, glitchBox, glitchSurvey;
        public int progressNum;
        public static ProgressManager instance;

        public Sprite[] glitchSprites;

        public FishhookManager fishhookManager;

        public GameObject theBox;
        int boxNum = 5;

        public Volume volume;
        VolumeProfile profile;
        LensDistortion lensDistortion;
        

        private void Awake()
        {
            if (instance == null) { instance = this; }
            else if (instance != this) { Destroy(this); }
        }

        private void Start()
        {
            profile = volume.sharedProfile;
            if (volume.profile.TryGet<LensDistortion>(out var tmp)) { lensDistortion = tmp; }
        }

        private void Update()
        {
            GlitchSprite();
            GlitchDialgue();
            GlitchBox();
        }

        public void IncreaseProgress()
        {
            progressNum++;
            switch (progressNum)
            {
                case 1:
                    glitchSprite = true;
                    break;
                case 2:
                    glitchDialogue = true;
                    break;
                case 3:
                    glitchBox = true;
                    break;
            }
        }

        public void ResetGlitch()
        {
            glitchSprite = false;
            glitchDialogue = false;
            glitchBox = false;
        }

        public void GlitchSprite()
        {
            if (glitchSprite && fishhookManager.fishingState == FishingState.caughtfishing)
            {
                int fishGlitchNum = Random.Range(0, glitchSprites.Length);
                fishhookManager.gameObject.GetComponent<SpriteRenderer>().sprite = glitchSprites[fishGlitchNum];
                ResetGlitch();
            }
        }

        public void GlitchDialgue()
        {
            if (glitchDialogue && fishhookManager.fishingState == FishingState.caughtfishing)
            {
                fishhookManager.fishermanComment = "!...Great on a body!@@1..";
                ResetGlitch();
            }
        }

        
        public void GlitchBox()
        {
            if (glitchBox && fishhookManager.fishingState == FishingState.caughtfishing)
            {
                for (int i = 0; i < 20; i++)
                {
                    GameObject box = Instantiate(theBox, new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-1, 3), -5), Quaternion.identity);
                    box.transform.localScale = new Vector2(Random.Range(0.1f, 0.5f), Random.Range(0.2f, 0.8f));
                    Destroy(box, 0.5f);
                }

                if (boxNum == 0)
                {
                    ResetGlitch();
                }
                else
                {
                    boxNum--;
                    Invoke("GlitchBox", 1);
                    print("Hello");
                }
            }
        }
    }

}
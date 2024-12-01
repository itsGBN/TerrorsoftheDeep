using UnityEngine;

namespace JustFish
{
    public class ProgressManager : MonoBehaviour
    {
        public bool glitchSprite, glitchDialogue, glitchBox;
        public int progressNum;
        public static ProgressManager instance;

        public Sprite[] glitchSprites;

        public FishhookManager fishhookManager;

        private void Awake()
        {
            if (instance == null) { instance = this; }
            else if (instance != this) { Destroy(this); }
        }

        private void Update()
        {
            GlitchSprite();
            GlitchDialgue();
        }

        public void IncreaseProgress()
        {
            progressNum++;
            switch (progressNum)
            {
                case 5:
                    glitchSprite = true;
                    break;
                case 6:
                    glitchDialogue = true;
                    break;
                case 7:
                    fishhookManager.fishingState = FishingState.notfishing; 
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

    }

}
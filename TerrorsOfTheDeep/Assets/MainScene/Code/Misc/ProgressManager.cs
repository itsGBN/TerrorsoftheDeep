using System.Collections;
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

        public GameObject[] glitchQuestions;
        int questionIterator;

        public FishhookManager fishhookManager;

        public GameObject theBox;
        int boxNum = 3, boxColor;

        public Volume volume;
        VolumeProfile profile;
        LensDistortion lensDistortion;

        public SpriteRenderer background;
        public Sprite[] backgroundswitch;
        

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
            GlitchSurvey();
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
                    glitchSurvey = true;
                    fishhookManager.fishingState = FishingState.notfishing;
                    break;
                case 3:
                    boxNum = 40;
                    glitchBox = true;
                    break;
            }
        }

        public void ResetGlitch()
        {
            glitchSprite = false;
            glitchDialogue = false;
            glitchBox = false;
            glitchSurvey = false;
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
                StartCoroutine(GlitchBoxRec());
                ResetGlitch();
            }
        }

        public IEnumerator GlitchBoxRec()
        {
            AudioManager.instance.Glitch3();
            for (int i = 0; i < boxNum; i++)
            {
                GameObject box = Instantiate(theBox, new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-0.5f, 2.5f), -5), Quaternion.identity);
                boxColor = Random.Range(0, 4);
                if (boxColor == 0) { box.GetComponent<SpriteRenderer>().color = new Color(Random.value, 0.48f, 0.48f); }
                else if (boxColor == 1) { box.GetComponent<SpriteRenderer>().color = new Color(0.48f, Random.value, 0.48f); }
                else { box.GetComponent<SpriteRenderer>().color = new Color(0.48f, 0.48f, Random.value); }
                box.transform.localScale = new Vector2(Random.Range(0.1f, 0.5f), Random.Range(0.2f, 0.8f));
                Destroy(box, 0.2f);
                yield return new WaitForSeconds(0.01f);
            }
        }

        public void GlitchSurvey()
        {
            if (glitchSurvey && glitchQuestions[questionIterator].GetComponent<SurveyBehavior>().getIsAnswered() == false)
            {
                glitchQuestions[questionIterator].SetActive(true);
                glitchQuestions[questionIterator].transform.position = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-0.5f, 2.5f), -5);
                glitchQuestions[questionIterator].GetComponent<SurveyBehavior>().setIsAnswered();
                questionIterator++;
                ResetGlitch();
            }
        }

        public void GlitchSurveyRec()
        {
            if(glitchQuestions[questionIterator].GetComponent<SurveyBehavior>().getIsAnswered() == false)
            {
                glitchQuestions[questionIterator].SetActive(true);
                glitchQuestions[questionIterator].transform.position = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-0.5f, 2.5f), -5);
                glitchQuestions[questionIterator].GetComponent<SurveyBehavior>().setIsAnswered();
                glitchQuestions[questionIterator-1].SetActive(false);
                questionIterator++;
                StartCoroutine(GlitchBackground());
            }
        }

        public IEnumerator GlitchBackground()
        {
            AudioManager.instance.Glitch3();
            for (int i = 0; i < 6; i++)
            {
                if(background.sprite == backgroundswitch[0] ) { background.sprite = backgroundswitch[1];}
                else { background.sprite = backgroundswitch[0];}
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

}
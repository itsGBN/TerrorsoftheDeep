using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace JustFish
{
    public class ProgressManager : MonoBehaviour
    {
        public bool glitchSurvey;
        public int progressNum;
        public static ProgressManager instance;

        public Sprite[] glitchSprites;

        public GameObject[] glitchQuestions;
        int questionIterator;

        public FishhookManager fishhookManager;

        public GameObject theBox;
        int boxColor;

        public Volume volume;

        public SpriteRenderer background;
        public Sprite[] backgroundswitch;

        public GameObject glitchHorror;
        public Animator glitchHorrorAnim;

        public FishManager fishManager;

        private void Awake()
        {
            if (instance == null) { instance = this; }
            else if (instance != this) { Destroy(this); }
        }

        private void Start()
        {

        }

        private void Update()
        {
            GlitchSurvey();
        }

        public void IncreaseProgress()
        {
            if (GameObject.FindGameObjectsWithTag("ProgressObject").Length != 1)
            {
                progressNum++;
                switch (progressNum)
                {
                    case 5:
                        GlitchSprite(Random.Range(0, glitchSprites.Length));
                        break;
                    case 10:
                        GlitchDialgue("----Dead--mxwacbi");
                        break;
                    case 12:
                        GlitchBox(40);
                        break;
                    case 15:
                        fishManager.ProgressSpawner(0);
                        GlitchSprite(Random.Range(0, glitchSprites.Length));
                        break;
                    case 20:
                        GlitchHorror("TheManAnim");
                        fishManager.ProgressSpawner(0);
                        break;
                }
            }
        }

        public void ResetGlitch()
        {
            glitchSurvey = false;
        }

        public void GlitchSprite(int num)
        {
            if (fishhookManager.fishingState == FishingState.caughtfishing)
            {
                fishhookManager.gameObject.GetComponent<SpriteRenderer>().sprite = glitchSprites[num];
                ResetGlitch();
            }
        }

        public void GlitchDialgue(string dialogue)
        {
            if (fishhookManager.fishingState == FishingState.caughtfishing)
            {
                fishhookManager.fishermanComment = dialogue;
                ResetGlitch();
            }
        }

        public void GlitchBox(int boxnum)
        {
            if (fishhookManager.fishingState == FishingState.caughtfishing)
            {
                StartCoroutine(GlitchBoxRec(boxnum));
                ResetGlitch();
            }
        }

        public IEnumerator GlitchBoxRec(int boxnum)
        {
            AudioManager.instance.Glitch3();
            for (int i = 0; i < boxnum; i++)
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
            if (glitchSurvey && fishhookManager.fishingState == FishingState.isfishing)
            {
                glitchQuestions[questionIterator].SetActive(true);
                glitchQuestions[questionIterator].transform.position = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-0.5f, 2.5f), -5);
                questionIterator++;
                fishhookManager.fishingState = FishingState.notfishing;
                ResetGlitch();
            }
        }

        public void GlitchSurveyRec()
        {
            glitchQuestions[questionIterator].SetActive(true);
            glitchQuestions[questionIterator].transform.position = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-0.5f, 2.5f), -5);
            glitchQuestions[questionIterator - 1].SetActive(false);
            questionIterator++;
            StartCoroutine(GlitchBackground(6));
        }

        public void GlitchSurveyClose()
        {
            glitchQuestions[questionIterator-1].SetActive(false);
            fishhookManager.fishingState = FishingState.isfishing;
            fishhookManager.SetReel(fishhookManager.reelAnchors);
            StartCoroutine(GlitchBackground(7));   
        }

        public IEnumerator GlitchBackground(int num)
        {
            AudioManager.instance.Glitch3();
            for (int i = 0; i < num; i++)
            {
                if(background.sprite == backgroundswitch[0] ) { background.sprite = backgroundswitch[1];}
                else { background.sprite = backgroundswitch[0];}
                yield return new WaitForSeconds(0.05f);
            }
        }

        public void GlitchHorror(string stateAnim)
        {
            glitchHorror.SetActive(true);
            glitchHorrorAnim.Play(stateAnim);
            ResetGlitch();
        }
    }

}
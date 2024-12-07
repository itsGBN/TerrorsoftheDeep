using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

namespace JustFish
{
    public class ProgressManager : MonoBehaviour
    {
        public bool glitchSurvey;
        public int progressNum;
        public static ProgressManager instance;

        public Sprite[] glitchSprites;
        public GameObject distortedFish;

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

        public VideoPlayer videoPlayer;

        private void Awake()
        {
            if (instance == null) { instance = this; }
            else if (instance != this) { Destroy(this); }

            videoPlayer.loopPointReached += EndReached;
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
                        GlitchDialgue("We’re so happy you joined us!");
                        break;
                    case 12:
                        GlitchBox(40);
                        break;
                    case 15:
                        GlitchSprite(Random.Range(0, glitchSprites.Length));
                        break;
                    case 17:
                        GlitchDialgue("----Dead--mxwacbi");
                        break;
                    case 20:
                        GlitchHorror("CreepyFishAnim");
                        fishManager.ProgressSpawner(0);
                        break;
                    case 23:
                        GlitchDialgue("He’s happier with us. So much happier.");
                        break;
                    case 27:
                        GlitchSprite(Random.Range(0, glitchSprites.Length)); //Once the game cartridge is in maybe put it here?
                        break;
                    case 30:
                        fishManager.ProgressSpawner(1);
                        break;
                    case 32:
                        GlitchDialgue("Couldn’t you just play this game forever?");
                        break;
                    case 35:
                        GlitchBox(70);
                        break;
                    case 38:
                        GlitchSprite(Random.Range(0, glitchSprites.Length));
                        break;
                    case 41:
                        GlitchDialgue("Soon, you will be with us soon.");
                        fishManager.ProgressSpawner(1);
                        break;
                    case 43:
                        videoPlayer.gameObject.transform.parent.gameObject.SetActive(true);
                        videoPlayer.Play();
                        break;
                }
            }
        }

        public void ResetGlitch()
        {
            glitchSurvey = false;
            distortedFish.SetActive(false);
        }

        public void GlitchSprite(int num)
        {
            if (fishhookManager.fishingState == FishingState.caughtfishing)
            {
                AudioManager.instance.Glitch2();
                fishhookManager.gameObject.GetComponent<SpriteRenderer>().sprite = glitchSprites[num];
                distortedFish.SetActive(true);
                Invoke("ResetGlitch", 0.4f);
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
                AudioManager.instance.MusicOneStop();
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
            AudioManager.instance.MusicTwo();
            glitchQuestions[questionIterator-1].SetActive(false);
            fishhookManager.fishingState = FishingState.isfishing;
            fishhookManager.SetReel(fishhookManager.reelAnchors);
            StartCoroutine(GlitchBackground(7));
            fishManager.wave1 = false;
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
            AudioManager.instance.Glitch1();
            glitchHorrorAnim.Play(stateAnim);
        }

        void EndReached(VideoPlayer vp)
        {
            Destroy(vp.gameObject.transform.parent.gameObject);
        }
    }

}
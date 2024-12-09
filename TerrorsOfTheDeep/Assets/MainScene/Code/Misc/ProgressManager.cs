using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace JustFish
{
    public class ProgressManager : MonoBehaviour
    {
        public bool glitchSurvey;
        public int progressNum, glitchAudioFish;
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
        
        public GameObject howToPlay;

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
            GlitchFishAudio();

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
                    case 7:
                        GlitchDialgue("We’re so happy you joined us!");
                        GlitchBox(40);
                        break;
                    case 10:
                        GlitchSprite(Random.Range(0, glitchSprites.Length));
                        break;
                    case 12:
                        GlitchBox(40);
                        glitchAudioFish = 1;
                        GlitchDialgue("He’s happier with us. So much happier.");
                        break;
                    case 15:
                        GlitchBox(60);
                        fishManager.ProgressSpawner(0);
                        break;
                    case 18:
                        GlitchBox(75);
                        fishManager.ProgressSpawner(1);
                        break;
                    case 20:
                        GlitchHorror("CreepyFishAnim");
                        break;
                    case 22:
                        GlitchBox(40);
                        GlitchDialgue("Couldn’t you just play this game forever?");
                        break;
                    case 25:
                        fishManager.ProgressSpawner(3);
                        glitchAudioFish = 1;//Once the game cartridge is in maybe put it here?
                        break;
                    case 27:
                        GlitchHorror("PentagramAnim");
                        break;
                    case 29:
                        glitchAudioFish = 0;
                       
                        break;
                    case 31:
                        GlitchBox(40);
                        GlitchDialgue("Soon, you will be with us soon.");
                        break;
                    case 34:
                        glitchAudioFish = 2;
                        break;
                    case 35:
                        GlitchBox(80);
                        fishManager.ProgressSpawner(2);
                        break;
                }
            }
        }

        public void ResetGlitch()
        {
            glitchAudioFish = 0;
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
            }
        }

        public void GlitchBox(int boxnum)
        {
            if (fishhookManager.fishingState == FishingState.caughtfishing)
            {
                StartCoroutine(GlitchBoxRec(boxnum));
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
            if (Random.value >= 0.5f) { StartCoroutine(GlitchBoxRec(20)); }
            StartCoroutine(GlitchBackground(6));
        }

        public void GlitchSurveyClose()
        {
            AudioManager.instance.MusicChaos();
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


        public void EndStart()
        {
            videoPlayer.gameObject.transform.parent.gameObject.SetActive(true);
            AudioManager.instance.MusicTwoStop();
            videoPlayer.Play();
        }

        void EndReached(VideoPlayer vp)
        {
            Destroy(vp.gameObject.transform.parent.gameObject);
            SceneManager.LoadScene("MainScene 1");
        }

        public void GlitchFishAudio()
        {
            if (glitchAudioFish != 0 && fishhookManager.fishingState == FishingState.isfishing)
            {
                switch (glitchAudioFish)
                {
                    case 2:
                        AudioManager.instance.NotSafe();
                        break;
                    case 1:
                        AudioManager.instance.NeverLeave();
                        break;
                }

                ResetGlitch();
            }
        }

        public void HtPlay()
        {
            Destroy(howToPlay);
        }
    }

}
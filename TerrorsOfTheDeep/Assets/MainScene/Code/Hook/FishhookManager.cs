using TMPro;
using UnityEngine;

namespace JustFish
{
    public enum FishingState { isfishing, caughtfishing, notfishing } // State Machine

    public class FishhookManager : MonoBehaviour
    {
        public FishingState fishingState = FishingState.isfishing; // Initializing the State Machine
        
        public LineRenderer reel; // Use the linereder as the reel
        public Transform[] reelAnchors; // start and end positions for the reel
        Transform[] reelPoints; // Find its transform points

        public Sprite hookSprite; // The original sprite of the hook
        public SpriteRenderer hookRenderer;

        public string fishermanComment;

        public TextMeshProUGUI fishermenLines;

        void Start()
        {
            SetReel(reelAnchors); //Use the SetReel Method
        }

        void Update()
        {
            FishingStates(); //Use the Fishing State Method
        }

        public void FishingStates()
        {
            Vector3 mousePosition = Input.mousePosition; // Initialize the mouse position

            switch (fishingState)
            {
                case FishingState.isfishing: // When fishing
                    mousePosition.z = 10f; // Initialize the mouse position of the z axis
                    transform.position = Camera.main.ScreenToWorldPoint(new Vector3(915f, mousePosition.y, mousePosition.z));  // Make the Hook follow the mouse y position
                    MoveReel(); // Use the Move Reel method
                    break; // Break

                case FishingState.caughtfishing: // When fishing
                    mousePosition.z = 10f; // Initialize the mouse position of the z axis
                    transform.position = Camera.main.ScreenToWorldPoint(new Vector3(915f, mousePosition.y, mousePosition.z));  // Make the Hook follow the mouse y position
                    AudioManager.instance.fishReel();
                    MoveReel(); // Use the Move Reel method
                    if(transform.position.y > 2)
                    {
                        hookRenderer.sprite = hookSprite;
                        fishermenLines.text = fishermanComment;
                        if(!IsInvoking("SetText")) { Invoke("SetText", 1.5f); }
                        AudioManager.instance.fishReelStop();
                        AudioManager.instance.fishAddPoint();
                        fishingState = FishingState.isfishing;
                    }
                    break; // Break
                case FishingState.notfishing:
                    ResetReel();
                    break;
            }
        }

        public void SetReel(Transform[] reelPoints)
        {
            reel.positionCount = reelPoints.Length; // Set the reel length to the transform position
            this.reelPoints = reelPoints; // Set it to itself
        }

        public void ResetReel()
        {
            reel.positionCount = 0; // Set the position to zero 
        }

        public void MoveReel()
        {
            for (int i = 0; i < reelPoints.Length; i++) // For each point in the line
            {
                reel.SetPosition(i, reelPoints[i].position); // Set it to its position
            }
        }

        public void SetText()
        {
            fishermenLines.text = ".....";
        }

        private void OnTriggerEnter(Collider other)
        {
            if((other.tag == "Fish" || other.tag == "ProgressObject") && fishingState == FishingState.isfishing) // When collided with the Fish tag
            {
                hookRenderer.sprite = other.gameObject.GetComponent<SpriteRenderer>().sprite;
                fishermanComment = other.gameObject.GetComponent<FishBehavior>().comment;
                fishingState = FishingState.caughtfishing;
                AudioManager.instance.fishHookLaunch();
                ProgressManager.instance.IncreaseProgress();
                Destroy(other.gameObject);
                if(other.GetComponent<FishBehavior>().namer == "Arm")
                {
                    ProgressManager.instance.glitchSurvey = true;
                    print(ProgressManager.instance.glitchSurvey);
                    ProgressManager.instance.GlitchHorror("TheManAnim");
                }
            }

            if ((other.tag == "Squid" && fishingState == FishingState.caughtfishing) && GameObject.FindGameObjectsWithTag("ProgressObject").Length != 1) // When collided with the Fish tag
            {
                hookRenderer.sprite = hookSprite;
                AudioManager.instance.fishReelStop();
                fishingState = FishingState.isfishing;
            }
        }
    }
}
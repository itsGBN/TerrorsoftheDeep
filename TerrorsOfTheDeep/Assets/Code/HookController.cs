using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] GameObject hook, hookMiniGame;
    [SerializeField] public string hookState, hookFish;

    FishController fishController;
    [SerializeField] Sprite hookSprite;

    ReelController reelController;
    [SerializeField] Transform[] reelPoints;

    bool isBarBlocked;
    public int barBlockedNum;

    private void Awake()
    {
        hookMiniGame.SetActive(false);
        fishController = GetComponent<FishController>();
        reelController = gameObject.transform.GetChild(0).GetComponent<ReelController>();
    }


    private void OnMouseDown()
    {
        if (hookState == "notFishing")
        {
            hookState = "isFishing"; // set is fiashing to true
            hook.GetComponent<SpriteRenderer>().sprite = hookSprite;
            reelController.SetReel(reelPoints);
        }
    }

    private void Update()
    {
        HookMovment();
    }

    private void HookMovment()
    {
        if (hookState == "isFishing")
        {
            hook.SetActive(true);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 49f;
            hook.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(950f, mousePosition.y, mousePosition.z));  // If isFishing is true, Make the Hook follow the mouse y position
        }

        if (hookState == "notFishing")
        {
            hook.SetActive(false);
        }

        if (hookState == "gameFishing")
        {
            foreach (GameObject fish in fishController.fish)
            {
                if(hookFish == fish.GetComponent<Fish>().fishName)
                {
                    hook.GetComponent<SpriteRenderer>().sprite = fish.GetComponent<SpriteRenderer>().sprite;
                }
            }

            hookMiniGame.SetActive(true);

            if(hookMiniGame.transform.GetChild(0).transform.localScale.y <= 1 && !isBarBlocked)
            {
                if(hookMiniGame.transform.GetChild(0).transform.localScale.y >= 0.2 && hookMiniGame.transform.GetChild(0).transform.localScale.y <= 0.3 || hookMiniGame.transform.GetChild(0).transform.localScale.y >= 0.6 && hookMiniGame.transform.GetChild(0).transform.localScale.y <= 0.7)
                {
                    isBarBlocked = true;
                }
                hookMiniGame.transform.GetChild(0).transform.localScale += new Vector3(0, 0.3f * Time.deltaTime, 0);
            }

            if (hookMiniGame.transform.GetChild(0).transform.localScale.y <= 1)
            {
                hookMiniGame.transform.GetChild(1).transform.localScale += new Vector3(0, 0.05f * Time.deltaTime, 0);
                
                if(hookMiniGame.transform.GetChild(1).transform.localScale.y >= hookMiniGame.transform.GetChild(0).transform.localScale.y)
                {
                    barBlockedNum = 0;
                    isBarBlocked = false;
                    hook.GetComponent<SpriteRenderer>().sprite = hookSprite;
                    hookMiniGame.transform.GetChild(0).transform.localScale = new Vector3(1, 0, 1);
                    hookMiniGame.transform.GetChild(1).transform.localScale = new Vector3(1, -0.03f, 1);
                    hookMiniGame.SetActive(false);
                    hookState = "isFishing"; // set is fiashing to true
                }
            }

            if (isBarBlocked)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    barBlockedNum++;
                }
                if (barBlockedNum == 10)
                {
                    hookMiniGame.transform.GetChild(0).transform.localScale += new Vector3(0, 0.2f, 0);
                    barBlockedNum = 0;
                    isBarBlocked = false;
                }
            }

            if (hookMiniGame.transform.GetChild(0).transform.localScale.y >= 1)
            {
                hookMiniGame.transform.GetChild(0).transform.localScale = new Vector3(1, 0, 1);
                hookMiniGame.transform.GetChild(1).transform.localScale = new Vector3(1, -0.03f, 1);
                hookMiniGame.SetActive(false);
                hookState = "caughtFishing";
            }
        }

        if (hookState == "caughtFishing")
        {
            if (Vector2.Distance(hook.transform.position, transform.GetChild(0).position) > 3)
            {
                hook.transform.position = Vector3.MoveTowards(hook.transform.position, transform.GetChild(0).position, 20 * Time.deltaTime);
            }
            else
            {
                hookState = "notFishing";
                reelController.ResetReel();
            }
        }
    }
}

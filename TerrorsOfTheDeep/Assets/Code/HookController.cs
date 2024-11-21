using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] GameObject hook;
    [SerializeField] public string hookState, hookFish;

    FishController fishController;
    [SerializeField] Sprite hookSprite;

    ReelController reelController;
    [SerializeField] Transform[] reelPoints;

    private void Awake()
    {
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

        if (hookState == "caughtFishing")
        {
            hookState = "notFishing";
            reelController.ResetReel();
        }
    }

    private void FixedUpdate()
    {
        HookMovment();
    }

    private void HookMovment()
    {
        if (hookState == "isFishing" || hookState == "caughtFishing")
        {
            hook.SetActive(true);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 49f;
            hook.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(1002.95f, mousePosition.y, mousePosition.z));  // If isFishing is true, Make the Hook follow the mouse y position
        }

        if (hookState == "notFishing")
        {
            hook.SetActive(false);
        }

        if (hookState == "caughtFishing")
        {
            foreach (GameObject fish in fishController.fish)
            {
                if(hookFish == fish.GetComponent<Fish>().fishName)
                {
                    hook.GetComponent<SpriteRenderer>().sprite = fish.GetComponent<SpriteRenderer>().sprite;
                }
            }
        }
    }
}

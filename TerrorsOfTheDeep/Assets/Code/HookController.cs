using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] GameObject hook;
    [SerializeField] public string hookState;

    private void OnMouseDown()
    {
        if (hookState == "notFishing")
        {
            hookState = "isFishing"; // set is fiashing to true
        }

        if (hookState == "caughtFishing")
        {
            hook.GetComponent<SpriteRenderer>().color = Color.black;
            hook.GetComponent<BoxCollider>().enabled = true;
            hookState = "notFishing";
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
            mousePosition.z = 59f;
            hook.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(1002.95f, mousePosition.y, mousePosition.z));  // If isFishing is true, Make the Hook follow the mouse y position
        }

        if (hookState == "notFishing")
        {
            hook.SetActive(false);
        }

        if (hookState == "caughtFishing")
        {
            hook.GetComponent<SpriteRenderer>().color = Color.red;
            hook.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

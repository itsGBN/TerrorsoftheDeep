using UnityEngine;

public class Fish : MonoBehaviour
{
    HookController hookController;

    [Header("Fish Properties")]
    public int fishSpeed;
    [Range(0, 1)] public float fishPopulation;

    [Header("Fish Description")]
    public string fishName;
    public int fishLength, fishWeight;
    public string[] fishComment;


    private void Awake()
    {
        hookController = GameObject.FindGameObjectWithTag("boat").GetComponent<HookController>();
        Destroy(gameObject, 10);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * fishSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hook")
        {
            if (hookController.hookState == "isFishing")
            {
                hookController.hookFish = fishName;
                hookController.hookState = "gameFishing";
                Destroy(gameObject);
            }
        }
    }
}

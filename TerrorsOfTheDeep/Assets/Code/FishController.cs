using UnityEngine;

public class FishController : MonoBehaviour
{
    //Inspector Variables
    [SerializeField] HookController hookController;
    [SerializeField] GameObject[] fish;

    //Private Variables
    float randomNum;

    private void OnParticleTrigger()
    {
        if (hookController.hookState != "caughtFishing")
        {
            print("Hit");
            hookController.hookState = "caughtFishing";
        }
    }

    private void Start()
    {
        InvokeRepeating("SpawnFish", 1, 1f);
    }

    private void SpawnFish()
    {
        foreach (GameObject fish in fish)
        {
            randomNum = UnityEngine.Random.Range(0, 1f);
            if(randomNum < fish.GetComponent<Fish>().fishPopulation)
            {
                Instantiate(fish, new Vector3(-63, UnityEngine.Random.Range(-30, 12), 49), Quaternion.identity);
            }
        }
    }
}

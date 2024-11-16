using UnityEngine;

public class FishParticleController : MonoBehaviour
{
    [SerializeField] FishingController fishingController;

    private void OnParticleTrigger()
    {
        if(fishingController.hookState != "caughtFishing")
        {
            print("Hit");
            fishingController.hookState = "caughtFishing";
        }
    }
}

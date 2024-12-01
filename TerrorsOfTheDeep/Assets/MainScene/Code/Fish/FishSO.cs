using UnityEngine;

namespace JustFish 
{
    [CreateAssetMenu]
    public class FishSO : ScriptableObject
    {
        [Header("Fish Properties")] // Fish properties in game
        public float fishSpeed; // How fast it moves
        public float fishLifeTime; // How long it lasts
        [Range(0, 1)] public float fishPopulation; // How much spawns

        [Header("Fish Description")] // How it looks and decribes
        public string fishName; // What is its name
        public Sprite fishSprite; // What is it look
        public string[] fishComment; // What is its desription
    }
}

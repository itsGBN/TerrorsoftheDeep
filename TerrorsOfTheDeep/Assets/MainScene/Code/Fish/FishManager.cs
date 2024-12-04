using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace JustFish
{
    public class FishManager : MonoBehaviour
    {
        public FishSO[] fish;   
        float fishPopulationNum;
        string fishComment;

        int randomFishCommentNum;
        public GameObject fishPrefab;
        SpriteRenderer fishRenderer;
        FishBehavior fishBehavior;


        void Start()
        {
            StartCoroutine(FishSpawner());
        }

        public IEnumerator FishSpawner()
        {
            yield return new WaitForSeconds(1f);       
            foreach (var fish in fish)
            {
                fishPopulationNum = Random.Range(0, 1f);
                if (fishPopulationNum < fish.fishPopulation)
                {
                    Instantiate(fishPrefab, new Vector3(-5, Random.Range(-0.7f, 1f), 0), Quaternion.identity);
                    fishRenderer = fishPrefab.GetComponent<SpriteRenderer>();
                    fishBehavior = fishPrefab.GetComponent<FishBehavior>();
                    fishBehavior.namer = fish.name;
                    fishBehavior.lifeTime = fish.fishLifeTime;
                    fishRenderer.sprite = fish.fishSprite;
                    fishBehavior.speed = fish.fishSpeed;
                    randomFishCommentNum = Random.Range(0, fish.fishComment.Length);
                    fishBehavior.comment = fish.fishComment[randomFishCommentNum];
                }
            }
            yield return StartCoroutine(FishSpawner());  
        }
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace JustFish
{
    public class FishManager : MonoBehaviour
    {
        public FishSO[] fish;
        public FishSO[] fish2;
        public FishSO[] progressObj;
        public bool wave1 = true;
        float fishPopulationNum;
        string fishComment;

        int randomFishCommentNum;
        public GameObject fishPrefab;
        SpriteRenderer fishRenderer;
        FishBehavior fishBehavior;

        void Start()
        {
            wave1 = true;
            StartCoroutine(FishSpawner());
        }

        public IEnumerator FishSpawner()
        {
            yield return new WaitForSeconds(1f);       

            if(wave1)
            {
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
                        fishBehavior.scorer = fish.fishSore;
                        randomFishCommentNum = Random.Range(0, fish.fishComment.Length);
                        fishBehavior.comment = fish.fishComment[randomFishCommentNum];
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }

            else
            {
                foreach (var fish in fish2)
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
                        fishBehavior.scorer = fish.fishSore;
                        randomFishCommentNum = Random.Range(0, fish.fishComment.Length);
                        fishBehavior.comment = fish.fishComment[randomFishCommentNum];
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }

            yield return StartCoroutine(FishSpawner());
        }

        public void ProgressSpawner(int num)
        {
            Instantiate(fishPrefab, new Vector3(-5, Random.Range(-0.7f, 1f), 0), Quaternion.identity);
            fishRenderer = fishPrefab.GetComponent<SpriteRenderer>();
            fishBehavior = fishPrefab.GetComponent<FishBehavior>();
            fishBehavior.namer = progressObj[num].name;
            fishBehavior.lifeTime = progressObj[num].fishLifeTime;
            fishRenderer.sprite = progressObj[num].fishSprite;
            fishBehavior.speed = progressObj[num].fishSpeed;
            randomFishCommentNum = Random.Range(0, progressObj[num].fishComment.Length);
            fishBehavior.comment = progressObj[num].fishComment[randomFishCommentNum];
        }
    }
}

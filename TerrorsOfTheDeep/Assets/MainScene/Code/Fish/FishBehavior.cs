using UnityEngine;

namespace JustFish
{
    public class FishBehavior : MonoBehaviour
    {
        public float speed;
        public float lifeTime;
        public string comment;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        void FixedUpdate()
        {
            FishMovement();
        }

        public void FishMovement()
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}


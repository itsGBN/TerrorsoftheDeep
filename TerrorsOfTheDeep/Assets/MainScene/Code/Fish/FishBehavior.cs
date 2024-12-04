using UnityEngine;

namespace JustFish
{
    public class FishBehavior : MonoBehaviour
    {
        public float speed;
        public float lifeTime;
        public string comment;
        public string namer;

        private void Start()
        {
            if(namer == "Squid")
            {
                gameObject.tag = "Squid";
            }
            Destroy(gameObject, lifeTime);
        }

        void FixedUpdate()
        {
            FishMovement();
        }

        public void FishMovement()
        {
            if(gameObject.tag == "Fish" || gameObject.tag == "Squid")
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
}


using UnityEngine;

namespace JustFish
{
    public class FishBehavior : MonoBehaviour
    {
        public float speed;
        public float lifeTime;
        public string comment;
        public string namer;
        public int scorer;

        float amplitude = 0.04f; // Wave height
        float frequency = 4f; // Wave speed

        private void Start()
        {
            if(namer == "Squid")
            {
                gameObject.tag = "Squid";
            }
            Invoke("SetProgress", 0.1f);
        }

        void FixedUpdate()
        {
            FishMovement();
        }

        public void FishMovement()
        {
            if(gameObject.tag == "Fish" || gameObject.tag == "Squid" || gameObject.tag == "ProgressObject")
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            if(gameObject.tag == "ProgressObject" && transform.position.x > 4)
            {
                transform.position = new Vector3(-4, transform.position.y, transform.position.z);
            }

            if (gameObject.tag == "Squid")
            {
                transform.position = transform.position + Vector3.up * Mathf.Sin(Time.time * frequency) * amplitude;

            }
        }

        public void SetProgress()
        {
            if (namer != "Newspaper" && namer != "Arm" && namer != "Deadbody" && namer != "Cartridge")
            {
                Destroy(gameObject, lifeTime);
            }
            else
            {
                gameObject.tag = "ProgressObject";
            }
        }
    }
}


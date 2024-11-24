using System.Collections;
using UnityEngine;

public class GlitchManager : MonoBehaviour
{

    [SerializeField] Sprite[] level1Glitch;
    SpriteRenderer spriteRenderer;
    public bool canGlitch;
    int glitchNum;
    int glitchTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1,1,1,0);
        StartCoroutine(OpenLevel1Glitch());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator OpenLevel1Glitch()
    {
        if(canGlitch)
        {
            glitchTime = Random.Range(0, 4);

            if (glitchTime == 0)
            {
                glitchNum = Random.Range(0, level1Glitch.Length); // choose the glitch
                spriteRenderer.sprite = level1Glitch[glitchNum];  // adds the glicth
                AudioManager.instance.Glitch3();                  // play glitch audio
                spriteRenderer.color = new Color(1, 1, 1, 1);     // shows the glitch
                yield return new WaitForSeconds(0.2f);            // wait 0.2 seconds
                spriteRenderer.color = new Color(1, 1, 1, 0);     // hides the glitch
                yield return new WaitForSeconds(1f);              // wait 1 seconds
                yield return StartCoroutine(OpenLevel1Glitch());  // repeat couroutine
            }
            else
            {
                yield return new WaitForSeconds(1f);              // wait 1 seconds
                yield return StartCoroutine(OpenLevel1Glitch());  // repeat couroutine
            }
        }
        else
        {
            print("error");
            yield return new WaitForSeconds(1f);              // wait 1 seconds
            yield return StartCoroutine(OpenLevel1Glitch());  // repeat couroutine
        }
    }
}

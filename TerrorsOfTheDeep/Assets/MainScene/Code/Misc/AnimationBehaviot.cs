using UnityEngine;

public class AnimationBehaviot : MonoBehaviour
{
    public void AnimationEnd()
    {
        gameObject.SetActive(false);
    }
}

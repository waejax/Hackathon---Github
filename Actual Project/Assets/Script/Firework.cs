using UnityEngine;

public class Firework : MonoBehaviour
{
    public ParticleSystem celebrationEffect;

    public void PlayCelebration()
    {
        if (celebrationEffect != null)
        {
            celebrationEffect.Play();
        }
    }
}
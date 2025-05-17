using JetBrains.Annotations;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource GunSource;
    public AudioClip BulletSound;


    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }

      
    }
    public void OnGunShoot()
    {
        GunSource.PlayOneShot(BulletSound);
    }
}

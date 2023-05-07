using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunction : MonoBehaviour
{
    [Header("SoundBank")]
    [SerializeField] private AudioClip[] weaponSlash;
    [SerializeField] private float weaponSlashVolume;    
    
    [SerializeField] private AudioClip[] footSteps;
    [SerializeField] private float footStepsVolume;


    public void PlayWeaponSlash()
    {
        PlayerMovement.Instance.sfxAudioSource.PlayOneShot(weaponSlash[Random.Range(0, weaponSlash.Length)], weaponSlashVolume);
    }

    public void PlayFootSteps()
    {
        PlayerMovement.Instance.sfxAudioSource.PlayOneShot(footSteps[Random.Range(0, footSteps.Length)], footStepsVolume);
    }
}

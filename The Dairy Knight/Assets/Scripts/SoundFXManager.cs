using UnityEngine;

/*

    Assignment: Mid
    Written by: Gavin Baltar created by following tutorial 2
    Filename: SoundFXManager.cs
    Description: This script manages the audio of the gameplay scene and is responsible for playing specific audio clips at a given location. Utilized by
        BattleSystem.cs.

*/

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundSFXClip(AudioClip audioClip, Transform spawnTransform,
    float volume)
    {
        // Spawn gameObject
        AudioSource audioSource = Instantiate(soundFXObject,
        spawnTransform.position, Quaternion.identity);

        // Assign audioClip
        audioSource.clip = audioClip;

        // Assign volume
        audioSource.volume = volume;

        // Play sound
        audioSource.Play();

        // Get length of clip
        float clipLength = audioSource.clip.length;

        // Destroy object
        Destroy(audioSource.gameObject, clipLength);
    }
}
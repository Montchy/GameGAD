using UnityEngine;

public class PlayAudioOnInteract : MonoBehaviour
{
    public AudioClip audioClip;
    public float maxHearingDistance = 10f; // Distance beyond which the audio will not be heard
    private AudioSource audioSource;
    private Transform player;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = true;

        player = GameObject.FindWithTag("Player").transform; // Assumes the player has a "Player" tag
    }

    void Update()
    {
        AdjustVolumeBasedOnDistance();
    }

    public void Execute()
    {
        if (audioSource != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.Play();
            }
        }
    }

    private void AdjustVolumeBasedOnDistance()
    {
        if (player == null || audioSource == null)
            return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < maxHearingDistance)
        {
            float volume = Mathf.Lerp(1f, 0f, distance / maxHearingDistance); // Gradually decrease volume
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0f; // Mute audio if beyond max distance
        }
    }
}

using UnityEngine;

public class ImageController : MonoBehaviour
{
    public GameObject images; // Coloca tus imágenes en esta lista en el inspector
    public AudioClip soundEffect;

    private int activeImageIndex = -1;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tecla E presionada");
            DeactivateCurrentImage();
        }
    }


    private void DeactivateCurrentImage()
    {
        images.SetActive(false);
        if (activeImageIndex != -1)
        {
            images.SetActive(false);
            PlaySoundEffect();
            activeImageIndex = -1;
        }
    }

    private void PlaySoundEffect()
    {
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }
}

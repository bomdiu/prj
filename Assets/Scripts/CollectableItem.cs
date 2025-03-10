using UnityEngine;

public class CollectableItem : MonoBehaviour {
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private float collectSoundVolume = 1.5f; // Điều chỉnh âm lượng ở đây
    private AudioSource audioSource;

    private void Start() {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = collectSound;
    }

    public void Collect() {
        if (collectSound != null) {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, collectSoundVolume);
        }

        Debug.Log("Collected: " + gameObject.name);
        Destroy(gameObject);
    }
}

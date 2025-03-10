using UnityEngine;

public class MusicBox : MonoBehaviour {
    [SerializeField] private AudioClip musicClip; // Âm thanh sẽ phát
    private AudioSource audioSource; // Nguồn âm thanh

    private void Start() {
        // Tạo AudioSource cho GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true; // Để phát nhạc lặp lại
    }

    private void Update() {
        // Kiểm tra xem người dùng có nhấn phím E không
        if (Input.GetKeyDown(KeyCode.R)) {
            if (audioSource.isPlaying) {
                audioSource.Stop(); // Dừng phát nhạc nếu đang phát
            } else {
                audioSource.Play(); // Phát nhạc nếu không đang phát
            }
        }
    }
}

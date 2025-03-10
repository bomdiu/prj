using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByeByeMusic : MonoBehaviour
{
    public AudioSource backgroundMusic; // Tham chiếu đến Audio Source của nhạc nền
    public AudioClip backgroundClip;

    void Start()
    {
        if (backgroundMusic == null || backgroundClip == null)
        {
            Debug.LogError("Background music AudioSource is not assigned!");
        }
    }

    public void StartDialogue()
    {

        if (backgroundMusic != null && backgroundClip != null)
        {
            backgroundMusic.clip = backgroundClip;
            backgroundMusic.Play(); // Phát nhạc nền khi bắt đầu đoạn hội thoại
        }

        // Các mã khác để khởi động đoạn hội thoại
    }

    public void EndDialogue()
    {

        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop(); // Dừng nhạc nền khi kết thúc đoạn hội thoại
        }

        // Các mã khác để kết thúc đoạn hội thoại
    }
}

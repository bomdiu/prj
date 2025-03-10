using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class GhostConversation : MonoBehaviour
{
    [SerializeField] GameObject talk;
    [SerializeField] private NPCConversation myConvo;
    private bool isInConversation = false;

    void Start()
    {
        talk.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isInConversation)
        {
            talk.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                talk.SetActive(false);
                isInConversation = true; // Đánh dấu là đang hội thoại
                ConversationManager.Instance.StartConversation(myConvo);
            }
        }
    }

    // Hàm này có thể được gọi từ ConversationManager để reset sau hội thoại
    public void EndConversation()
    {
        isInConversation = false;
    }
}


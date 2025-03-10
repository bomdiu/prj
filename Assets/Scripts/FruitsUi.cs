using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DialogueEditor;

public class FruitsUI : MonoBehaviour
{
    
    private TextMeshProUGUI fruitText;
    [SerializeField] private PlayerCollectItem playerCollectItem; // Liên kết tới PlayerCollectItem
    [SerializeField] private NPCConversation byeConvo;


    void Start()
    {
        fruitText = GetComponent<TextMeshProUGUI>();

        // Kiểm tra nếu playerCollectItem được tham chiếu
        if (playerCollectItem != null)
        {
            // Đăng ký sự kiện OnFruitCollected để cập nhật UI khi thu thập vật phẩm
            playerCollectItem.OnFruitCollected.AddListener(UpdateFruitText);
            UpdateFruitText(playerCollectItem); // Cập nhật UI ngay khi bắt đầu
        }
    }

    public void UpdateFruitText(PlayerCollectItem playerCollectItem)
    {
        fruitText.text = playerCollectItem.NumberOfFruits.ToString();
         // Kiểm tra nếu số lượng fruit đạt 10
        if (playerCollectItem.NumberOfFruits >= 10)
        {
            ConversationManager.Instance.StartConversation(byeConvo);
        }
    }
    
}
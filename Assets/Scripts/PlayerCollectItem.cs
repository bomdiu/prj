using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollectItem : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask collectableLayerMask;

    public int NumberOfFruits { get; private set; } // Đếm số vật phẩm đã thu thập được
    public UnityEvent<PlayerCollectItem> OnFruitCollected; // Sự kiện thu thập vật phẩm

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Thực hiện kiểm tra raycast để thu thập vật phẩm
            float collectDistance = 4f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, collectDistance, collectableLayerMask))
            {
                // Kiểm tra xem vật phẩm có thành phần (component) thu thập được không
                if (raycastHit.transform.TryGetComponent(out CollectableItem collectableItem))
                {
                    // Thu thập vật phẩm
                    collectableItem.Collect();
                    NumberOfFruits++; // Tăng số lượng vật phẩm đã thu thập
                    OnFruitCollected.Invoke(this); // Gọi sự kiện khi thu thập thành công
                    Debug.Log("Collected: " + raycastHit.transform.name);
                }
            }
        }
    }
}
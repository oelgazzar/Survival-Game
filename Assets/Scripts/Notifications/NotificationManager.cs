using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
using System;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] GameObject _notificationPanel;
    [SerializeField] NotificationUI _notificationPrefab;
    [SerializeField] float _notificationDuration;
    [SerializeField] InventoryItemData _debugStoneItem;
    [SerializeField] InventoryItemData _debugWoodItem;

    ObjectPool<NotificationUI> _notificationUIPool;

    readonly List<NotificationUI> _notificationList = new();
    string _lastMessage = string.Empty;
    int _messageCounter = 1;

    private void Start()
    {
        _notificationUIPool = new ObjectPool<NotificationUI>(
            () => Instantiate(_notificationPrefab, _notificationPanel.transform),
            obj => obj.gameObject.SetActive(true),
            obj => obj.gameObject.SetActive(false),
            obj => Destroy(obj.gameObject),
            true,
            10,
            50);

    }


    public void CreateNotification(string message, Sprite icon)
    {
        var notificationUI = _notificationUIPool.Get();
        _notificationList.Add(notificationUI);

        var finalMessage = message;

        if (_lastMessage.CompareTo(message) == 0)
        {
            finalMessage += $" x{++_messageCounter}";
        }
        else
        {
            _messageCounter = 1;
            _lastMessage = message;
        }

        notificationUI.Create(finalMessage, _notificationDuration, icon);
        notificationUI.NotificationTimedOut += OnNotificationTimedOut;
    }

    private void OnNotificationTimedOut(NotificationUI notificationUI)
    {
        notificationUI.NotificationTimedOut -= OnNotificationTimedOut;
        _notificationList.Remove(notificationUI);
        _notificationUIPool.Release(notificationUI);
    }

    [ContextMenu("Create Test Notification")]
    void CreateTestNotification()
    {
        CreateNotification("Picked stone", _debugStoneItem.Sprite);
    }

    [ContextMenu("Create Test Notification2")]
    void CreateTestNotification2()
    {
        CreateNotification("Picked Wood", _debugWoodItem.Sprite);
    }

    private void OnEnable()
    {
        InventorySystem.ItemAddedToInventory += OnItemAddedToInventory;
    }

    private void OnItemAddedToInventory(InventoryItemData data)
    {
        CreateNotification($"Picked {data.Name}", data.Sprite);
    }

    private void OnDisable()
    {
        InventorySystem.ItemAddedToInventory -= OnItemAddedToInventory;
        foreach(var notificationUI in _notificationList)
        {
            if (notificationUI != null)
            {
                notificationUI.StopAllCoroutines();
            }
        }
        _notificationList.Clear();
        //_notificationUIPool.Clear();
    }
}

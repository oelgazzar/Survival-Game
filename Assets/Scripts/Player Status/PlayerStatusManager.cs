using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    [SerializeField] PlayerStatusData[] _allStatusData;
    [SerializeField] int _debugStatusIndex;
    [SerializeField] float _debugValue;

    public static PlayerStatusManager Instance;
    public static event Action<PlayerStatus> StatusChanged;

    readonly Dictionary<PlayerStatusData, PlayerStatus> _playerStatusMap = new();

    private void Awake()
    {
        Instance = this;

        foreach (var statusData in _allStatusData)
        {
            _playerStatusMap[statusData] = new PlayerStatus(statusData);
        }
    }

    public void UpdatePlayerStatus(PlayerStatusData statusData, float value)
    {
        var playerStatus = _playerStatusMap[statusData];
        playerStatus.CurrentValue = value;
        StatusChanged?.Invoke(playerStatus);
    }

    [ContextMenu("Test")]
    void Test()
    {
        var statusData = _allStatusData[_debugStatusIndex];
        UpdatePlayerStatus(statusData, _debugValue);
    }
}
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

        Initialize();
    }

    private void Initialize()
    {
        foreach (var statusData in _allStatusData)
        {
            foreach (var influence in statusData.InfluenceEffects)
            {
                influence.Thresholds.Sort(
                    (a, b) => b.Cutoff.CompareTo(a.Cutoff)
                );
            }
            _playerStatusMap[statusData] = new PlayerStatus(statusData);
        }
    }

    public void ModifyPlayerStatus(PlayerStatusData statusData, float value)
    {
        var playerStatus = _playerStatusMap[statusData];
        playerStatus.UpdateCurrentValue(playerStatus.CurrentValue + value);
        StatusChanged?.Invoke(playerStatus);
    }

    private void Update()
    {
        foreach (var status in _playerStatusMap.Values)
        {
            HandleStatus(status);
        }
    }

    float GetNormalized(PlayerStatusData data)
    {
        var normalizedValue = _playerStatusMap[data].CurrentValue / data.MaxValue;
        return normalizedValue;
    }

    private void HandleStatus(PlayerStatus status)
    {
        var change = status.StatusData.BaseRegenRate + status.StatusData.BaseDecayRate;

        foreach (var influence in status.StatusData.InfluenceEffects)
        {
            var affectingStatus = influence.AffectingStatus;
            var affectingStatusCurrentValue = GetNormalized(affectingStatus);

            foreach(var threshold in influence.Thresholds)
            {
                if (affectingStatusCurrentValue > threshold.Cutoff)
                {
                    change += threshold.Modifier;
                    break;
                }
            }
        }

        ModifyPlayerStatus(status.StatusData, change * Time.deltaTime);
    }

    [ContextMenu("Test")]
    void Test()
    {
        var statusData = _allStatusData[_debugStatusIndex];
        ModifyPlayerStatus(statusData, _debugValue);
    }
}
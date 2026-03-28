using System;

[Serializable]
public class PlayerStatus
{
    public PlayerStatusData StatusData;
    public float CurrentValue {  get; private set; }

    public PlayerStatus(PlayerStatusData statusData)
    {
        StatusData = statusData;
        CurrentValue = statusData.MaxValue;
    }

    public void UpdateCurrentValue(float value)
    {
        CurrentValue = Math.Clamp(value, 0, StatusData.MaxValue);
    }
}

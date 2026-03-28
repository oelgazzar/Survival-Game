public class PlayerStatus
{
    public PlayerStatusData StatusData;
    public float CurrentValue;

    public PlayerStatus(PlayerStatusData statusData)
    {
        StatusData = statusData;
        CurrentValue = statusData.MaxValue;
    }
}

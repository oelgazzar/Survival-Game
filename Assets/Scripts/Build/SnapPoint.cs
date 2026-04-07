using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SnapPoint : MonoBehaviour
{
    public bool IsOccupied { get; private set; }
    public SnapPointType Type;
    public SnapPointType[] CompatibleTypes;

    BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
        _collider.size = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void SetOccupied(bool value)
    {
        IsOccupied = value;
    }

    public bool IsCompatibleWith(SnapPointType otherType)
    {
        foreach(var compatibleType in CompatibleTypes)
        {
            if (compatibleType == otherType) return true;
        }
        return false;
    }
}

public enum SnapPointType
{
    WallTop,
    WallBottom,
    WallSide,
    Floor,
    Ceiling,
    Foundation
}
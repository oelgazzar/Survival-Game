using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatusData", menuName = "Scriptable Objects/PlayerStatusData")]
public class PlayerStatusData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public Sprite Color;
    public int MaxValue;
    public float BaseRegenRate;
    public float BaseDecayRate;
    public List<InfluenceEffect> InfluenceEffects;
}

[Serializable]
public class InfluenceEffect
{
    public PlayerStatusData AffectingStatus;
    public List<EffectThreshold> Thresholds;
}

[Serializable]
public class EffectThreshold
{
    public float Cutoff;
    public float Modifier;
}
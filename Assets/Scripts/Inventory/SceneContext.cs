using TMPro;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    public GameObject ItemInfoPanel;
    public static SceneContext Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}

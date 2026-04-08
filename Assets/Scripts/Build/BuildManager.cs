using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour, ISaveable
{
    [SerializeField] BuildStructure[] _structurePrefabs;
    [SerializeField] BuildGhost[] _ghostPrefabs;

    bool _inBuildMode;
    BuildGhost _currentGhost;
    BuildStructure _currentStructurePrefab;
    BuildGhost _currentGhostPrefab;

    int _currentStructureIndex = 0;

    List<SaveableBuildData> _placedStructures = new List<SaveableBuildData>();

    public string SaveID => "build";

    private void Start()
    {
        _currentStructurePrefab = _structurePrefabs[_currentStructureIndex];
        _currentGhostPrefab = _ghostPrefabs[_currentStructureIndex];
    }

    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.Paused)
            return;


        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            ToggleBuildMode();            
        }

        if (Mouse.current.leftButton.wasPressedThisFrame && _inBuildMode && _currentGhost != null && _currentGhost.IsInValidPosition)
        {
            PlaceStructure();
        }

        if (Mouse.current.rightButton.wasPressedThisFrame && _inBuildMode && _currentGhost != null)
        {
            _currentGhost.Rotate();
        }

        var scrollValue = Mouse.current.scroll.ReadValue().y;
        if (_inBuildMode && scrollValue != 0)
        {
            _currentStructureIndex = (_currentStructureIndex + (int)scrollValue + _structurePrefabs.Length) % _structurePrefabs.Length;

            UpdateCurrentStructure();
        }
    }

    void UpdateCurrentStructure()
    {
        var previousGhostRotation = Quaternion.identity;

        if (_currentGhost != null)
        {
            previousGhostRotation = _currentGhost.transform.rotation;
            Destroy(_currentGhost.gameObject);
        }

        _currentStructurePrefab = _structurePrefabs[_currentStructureIndex];
        _currentGhostPrefab = _ghostPrefabs[_currentStructureIndex];
        _currentGhost = Instantiate(_currentGhostPrefab, Vector3.zero, previousGhostRotation);
    }

    private void ToggleBuildMode()
    {
        _inBuildMode = !_inBuildMode;

        if (_inBuildMode)
        {
            _currentGhost = Instantiate(_currentGhostPrefab);

        }
        else if (_currentGhost != null)
        {
            Destroy(_currentGhost.gameObject);
            _currentGhost = null;
        }
    }

    private void PlaceStructure()
    {
        var newStructure = Instantiate(_currentStructurePrefab, _currentGhost.transform.position, _currentGhost.transform.rotation);
        _currentGhost.Place(newStructure);
        _placedStructures.Add(new SaveableBuildData
        {
            StructureIndex = _currentStructureIndex,
            Position = newStructure.transform.position,
            Rotation = newStructure.transform.rotation
        });
    }

    public string Save()
    {
        return JsonUtility.ToJson(new SerializationWrapper<SaveableBuildData>(_placedStructures));
    }

    public void Load(string state)
    {
        _placedStructures = JsonUtility.FromJson<SerializationWrapper<SaveableBuildData>>(state).Data;

        foreach (var buildData in _placedStructures)
        {
            var structurePrefab = _structurePrefabs[buildData.StructureIndex];
            Instantiate(structurePrefab, buildData.Position, buildData.Rotation);
        }
    }
}

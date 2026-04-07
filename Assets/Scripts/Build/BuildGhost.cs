using UnityEngine;


public class BuildGhost : MonoBehaviour
{
    [SerializeField] LayerMask _placementMask;
    [SerializeField] LayerMask _obstacleLayerMask;

    [SerializeField] Material _validMaterial;
    [SerializeField] Material _invalidMaterial;

    [SerializeField] float _snapRadius = .2f;

    [SerializeField] SnapPoint[] _ownSnapPoints;

    [SerializeField] Collider _collider;

    public bool IsInValidPosition { get; private set; }

    Renderer _renderer;
    Camera _cam;

    SnapPoint _targetSnapPoint;
    SnapPoint _ghostSnapUsed;
    private int _rotationY;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _cam = Camera.main;

    }

    void Update()
    {
        Move();
        TrySnap();
        IsInValidPosition = ValidatePosition(transform.position);
        RenderValidity(IsInValidPosition);
    }
    private void Move()
    {
        transform.rotation = Quaternion.Euler(0, _rotationY, 0);
        var ray = _cam.ScreenPointToRay(new(Screen.width / 2, Screen.height / 2, _cam.transform.position.z));
        if (Physics.Raycast(ray, out var hit, 10, _placementMask))
        {
            transform.position = hit.point;
            var structure = hit.collider.GetComponentInParent<BuildStructure>();
            if (structure != null)
            {
                SnapToStructure(structure);
            } else
            {
                DetectSnap();
            }
        }
    }

    private void SnapToStructure(BuildStructure structure)
    {
        var targetSnapPoints = structure.SnapPoints;
        
        var closestDistance = float.MaxValue;
        SnapPoint closestSnapPoint = null;
        SnapPoint usedGhostSnapPoint = null;

        foreach (var ghostSnapPoint in _ownSnapPoints)
        {
            foreach (var targetSnapPoint in targetSnapPoints)
            {
                if (CanSnapTo(targetSnapPoint, ghostSnapPoint) == false) continue;

                var distance = Vector3.Distance(ghostSnapPoint.transform.position,
                    targetSnapPoint.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSnapPoint = targetSnapPoint;
                    usedGhostSnapPoint = ghostSnapPoint;
                }
            }
        }
        _ghostSnapUsed = usedGhostSnapPoint;
        _targetSnapPoint = closestSnapPoint;
    }

    private void DetectSnap()
    {
        var closestDistance = _snapRadius;
        SnapPoint closestSnapPoint = null;
        SnapPoint usedGhostSnapPoint = null;

        foreach (var ghostSnapPoint in _ownSnapPoints)
        {
            var colliders = new Collider[10];
            var collisions = Physics.OverlapSphereNonAlloc(ghostSnapPoint.transform.position, _snapRadius, colliders);
            if (collisions > 0)
            {
                for (var i = 0; i < collisions; i++)
                {
                    var collider = colliders[i];
                    if (collider.TryGetComponent<SnapPoint>(out var targetSnapPoint) == false) continue; // Skip if no SnapPoint component

                    if (CanSnapTo(targetSnapPoint, ghostSnapPoint) == false) continue;

                    var distance = Vector3.Distance(ghostSnapPoint.transform.position,
                    targetSnapPoint.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestSnapPoint = targetSnapPoint;
                        usedGhostSnapPoint = ghostSnapPoint;
                    }
                }
            }
        }

        _targetSnapPoint = closestSnapPoint;
        _ghostSnapUsed = usedGhostSnapPoint;
    }

    bool CanSnapTo(SnapPoint targetSnapPoint, SnapPoint ghostSnapPoint)
    {
        // Skip snap points on the same object
        if (targetSnapPoint.transform.root == ghostSnapPoint.transform.root) return false;

        // Skip occupied snap points
        if (targetSnapPoint.IsOccupied) return false;

        // Check compatibility
        if (targetSnapPoint.IsCompatibleWith(ghostSnapPoint.Type) == false) return false;

        var dot = Vector3.Dot(targetSnapPoint.transform.forward,
                               ghostSnapPoint.transform.forward);
        if (dot < .95f) return false;

        var targetPosition = transform.position + targetSnapPoint.transform.position - ghostSnapPoint.transform.position;
        if (ValidatePosition(targetPosition) == false) return false;

        return true;
    }

    private bool ValidatePosition(Vector3 position)
    {
        var overlaps = new Collider[10];
        var center = position + _collider.bounds.extents.y * Vector3.up;
        var numOverlaps = Physics.OverlapBoxNonAlloc(center, _collider.bounds.extents * .85f, overlaps, transform.rotation, _obstacleLayerMask);
        for (var i = 0; i < numOverlaps; i++)
        {
            var overlap = overlaps[i];
            if (overlap.gameObject == gameObject) continue; // Ignore self

            return false;
        }
        return true;
    }

    void RenderValidity(bool isValid)
    {
        _renderer.material = isValid ? _validMaterial : _invalidMaterial;
    }

    private void TrySnap()
    {
        if (_targetSnapPoint != null)
        {
            transform.position = transform.position + _targetSnapPoint.transform.position - _ghostSnapUsed.transform.position;
        }
    }

    public void Rotate()
    {
        _rotationY += 90;
    }

    public void Place(BuildStructure newStructure)
    {
        if (_targetSnapPoint != null)
        {
            _targetSnapPoint.SetOccupied(true);
            foreach(var p in newStructure.SnapPoints)
            {
                if (p.transform.position == _targetSnapPoint.transform.position)
                {
                    p.SetOccupied(true);
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_targetSnapPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_targetSnapPoint.transform.position, .1f);
        }
    }
}
using UnityEngine;
using UnityEngine.AI;

public class RabbitController : MonoBehaviour
{
    [SerializeField] Vector3 _wanderAreaCenter;
    [SerializeField] float _wanderAreaRadius;

    float _runTime;
    float _idleTime;
    bool _isRunning;
    float _runTimer;
    float _idleTimer;
    Vector3 _destination;

    NavMeshAgent _agent;
    Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _runTime = Random.Range(5, 7);
        _idleTime = Random.Range(7, 12);

        _isRunning = true;
    }
    void Update()
    {
        if (_isRunning)
            CheckRunTime();
        else
            CheckIdleTime();

        _animator.SetBool("IsRunning", _isRunning);
    }

    private void CheckIdleTime()
    {
        _idleTimer += Time.deltaTime;
        if (_idleTimer >= _idleTime)
        {
            _idleTimer = 0;
            _isRunning = true;
            _agent.SetDestination(GetRandomDestination());
            _agent.isStopped = false;
        }
    }

    void CheckRunTime()
    {
        _runTimer += Time.deltaTime;
        if (_runTimer >= _runTime)
        {
            _runTimer = 0;
            _isRunning = false;
            _agent.isStopped = true;
        }
    }

    Vector3 GetRandomDestination()
    {
        var randomDirection = Random.insideUnitSphere * _wanderAreaRadius;
        var randomPosition =  _wanderAreaCenter + new Vector3(randomDirection.x, transform.position.y, randomDirection.z);
        NavMesh.SamplePosition(randomPosition, out var hit, 1f, NavMesh.AllAreas);
        _destination = hit.position;
        return _destination;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_destination, 10);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_wanderAreaCenter, _wanderAreaRadius);
    }
}

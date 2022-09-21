using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;    
    [SerializeField] private LayerMask _whatIsGround, _whatIsPlayer;
    private Transform player;

    //Patroling
    [SerializeField] private Vector3 _walkPoint;    
    [SerializeField] private float _walkPointRange;
    private bool _walkPointSet;

    //Visibility
    [SerializeField] private float _sightRange;
    private bool _playerInsightRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _agent.speed * PlayerPrefs.GetInt("EnemySpeed");
    }

    private void Update()
    {
        _playerInsightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);

        if (!_playerInsightRange) Patrolling();
        if (_playerInsightRange) ChasePlayer();
    }

    private void Patrolling()
    {
        if (!_walkPointSet)
            Search_walkPoint();

        if (_walkPointSet)
            _agent.SetDestination(_walkPoint);

        Vector3 distanceTo_walkPoint = transform.position - _walkPoint;

        if (distanceTo_walkPoint.magnitude < 2f)
            _walkPointSet = false;
    }

    private void Search_walkPoint()
    {
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, _whatIsGround))
            _walkPointSet = true;
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerManager>().playerHasBeenCaught = true;
        }
    }
}

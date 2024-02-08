using UnityEngine;
using UnityEngine.AI;

public class GolemAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float startWaitTime = 4f;
    [SerializeField] private float speedWalk = 6f;
    [SerializeField] private float speedRun = 9f;
    [SerializeField] private float viewRadius = 15f;
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Transform[] wayPoints;
    
    private int currentWayPointIndex;
    private Vector3 playerPosition;
    private float waitTime;
    private bool playerInRange;
    private bool isPatrol;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        
        playerPosition=Vector3.zero;
        isPatrol = true;
        playerInRange = false;
        waitTime = startWaitTime;

        currentWayPointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        Move(speedWalk);
        navMeshAgent.SetDestination(wayPoints[currentWayPointIndex].position);
    }

    private void Update()
    {
        EnvironmentView();

        if (!isPatrol)
        {
            Chasing();
        }
        else
        {
            Patroling();
        }
    }

    private void Chasing()
    {
        //playerLastPosition = Vector3.zero;
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Stop();
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
            Move(speedRun);
            navMeshAgent.SetDestination(playerPosition);
        }
    }

    private void Move(float speed)
    {
        anim.SetBool("Walk",true);
        if (isPatrol)
        {
            anim.SetBool("Victory",false);
        }
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }
    
    private void Stop()
    {
        anim.SetBool("Walk",false);
        if (isPatrol)
        {
            anim.SetBool("Victory",true);
        }
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    private void NextPoint()
    {
        currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length;
        navMeshAgent.SetDestination(wayPoints[currentWayPointIndex].position);
    }
    
    // патрулирование
    private void Patroling()
    {
        navMeshAgent.SetDestination(wayPoints[currentWayPointIndex].position);
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (waitTime <= 0)
            {
                NextPoint();
                Move(speedWalk);
                waitTime = startWaitTime;
            }
            else
            {
                Stop();
                Debug.Log("patroling stop2");
                waitTime -= Time.deltaTime;
            }
        }
    }
    
    /*входная точка скрипта*/
    private void EnvironmentView()
    {
        //поиск игрока
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        //не входит в цикл пока не найдет игрока
        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform; //берем трансформ игрока
            Vector3 dirToPlayer = (player.position - transform.position).normalized; // определяем направление к игроку
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask)) //если препятствия не мешают видеть игрока
                {
                    this.playerInRange = true; //погоня
                    isPatrol = false;
                }
                else
                {
                    this.playerInRange = false; //остановка погони
                }
            }
            
            // если убежали далеко
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                Debug.Log("lost");
                this.playerInRange = false;
                isPatrol = true;
            }
            // находим позицию игрока
            if (this.playerInRange)
            {
                playerPosition = player.transform.position;
            }
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
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

    private Health enemyHealth;

    private int currentWayPointIndex;
    private Vector3 playerPosition;
    private float waitTime;
    private bool playerInRange;
    private bool isPatrol;
    private bool caughtPlayer;
    private bool isAlive;

    private Animator anim;

    private void Start()
    {
        isAlive = true;
        
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<Health>();
    
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
        if (isAlive)
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

    }

    /// <summary>
    /// приследование
    /// </summary>
    private void Chasing()
    {
        if (!caughtPlayer)
        {
            anim.SetBool("Attack", false);
            Move(speedRun);
            navMeshAgent.SetDestination(playerPosition);
        }
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            caughtPlayer = true;
            transform.LookAt(playerPosition);
            anim.SetBool("Attack", true);
            Stop();
        }
    }

    /// <summary>
    /// Движение (из параметров задается скорость ходьбы или бега)
    /// </summary>
    /// <param name="speed"></param>
    private void Move(float speed)
    {
        anim.SetBool("Walk",true);
        anim.SetBool("Victory",false);
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    /// <summary>
    /// остановка
    /// </summary>
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
    
    /// <summary>
    /// переход к следующей точке
    /// </summary>
    private void NextPoint()
    {
        currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length;
        navMeshAgent.SetDestination(wayPoints[currentWayPointIndex].position);
    }

    /// <summary>
    /// патрулирование
    /// </summary>
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
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider playerWeapon)
    {
        if (playerWeapon.tag == "MeleeWeapon")
        {
            if (enemyHealth.IsDeadCheker())
            {
                isAlive = false;
                Death();
            }
            else
            {
                TakeDamage();
            }
            
            
        }
    }

    private void Death()
    {
        Debug.Log("enemyDown");
        anim.SetBool("Death", true);
        gameObject.GetComponent<Collider>().enabled = false;
    }
    private void TakeDamage()
    {
        anim.SetTrigger("Hit");
    }

    /// <summary>
    /// мозг голема, скрипт отвечающий за поиск игрока, измерения расстояния от
    /// голема до игрока, перехода меду состояниями.
    /// </summary>
    private void EnvironmentView()
    {
        //поиск игрока
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform; //берем трансформ игрока
            Vector3 dirToPlayer = (player.position - transform.position).normalized; // определяем направление к игроку
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                //если препятствия не мешают видеть игрока
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask)) 
                {
                    this.playerInRange = true; //погоня
                    isPatrol = false;
                }
                else
                {
                    this.playerInRange = false; //остановка погони
                }
            }
            
            // если игрок убежал далеко:
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                this.playerInRange = false;
                isPatrol = true;
            }
            //погоня, если игрок уворачивается от атаки:
            if (Vector3.Distance(transform.position, player.position) > navMeshAgent.stoppingDistance)
            {
                this.playerInRange = true; 
                caughtPlayer = false;
            }
            
            // находим позицию игрока
            if (this.playerInRange)
            {
                playerPosition = player.transform.position;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Components")]
    [SerializeField] EnemyFSM enemyFSM;
    [SerializeField] NavMeshAgent navMeshAgent;

    [Header("Enemy Target")]
    [SerializeField] Vector3 target;
    [SerializeField] bool canWalk = true;

    [Header("Enemy Idle")]
    [SerializeField] float maxIdleWalkDistance = 50f;
    [SerializeField] Vector2 _minMaxIdleWaitTime = new Vector2(2, 5);
    [SerializeField] bool walking = false;

    [Header("Enemy Attack")]
    [SerializeField] int damage = 10;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] PlayerHealth targetHealth;

    public bool Walking => walking;
    Coroutine idleWaitTime;
    Coroutine attackRoutine;


    void OnValidate()
    {
        if (enemyFSM == null)
        {
            enemyFSM = GetComponent<EnemyFSM>();
        }
    }

    public void StopWalk()
    {
        canWalk = false;
        navMeshAgent.ResetPath();
    }

    public void StartWalk()
    {
        canWalk = true;
        EnemyIdle();
    }

    void Start()
    {
        // if (!canWalk) return;
        // enemyFSM.ChangeState(enemyFSM.EnemyIdleState);
    }


    public void EnemyIdle()
    {
        if (!canWalk) return;
        enemyFSM.ChangeState(enemyFSM.EnemyIdleState);

    }

    public void EnemyTarget(Transform target)
    {
        if (!canWalk) return;
        enemyFSM.ChangeState(enemyFSM.EnemyTargetState);

        this.target = target.position;
    }


    public void StartEnemyAttack(PlayerHealth playerHealth)
    {
        targetHealth = playerHealth;
        enemyFSM.ChangeState(enemyFSM.EnemyAttackState);
    }

    public void EnemyAttackLogic()
    {
        if (!targetHealth) return;

        navMeshAgent.ResetPath();

        if (attackRoutine == null) attackRoutine = StartCoroutine(AttackRoutine());
        else
        {
            StopCoroutine(attackRoutine);
            attackRoutine = StartCoroutine(AttackRoutine());
        }

    }

    IEnumerator AttackRoutine()
    {
        if (targetHealth)
        {
            targetHealth.TakeDamage(damage);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        yield return new WaitForSeconds(timeBetweenAttacks);

    }


    public void EnemyIdleLogic()
    {
        if (!canWalk) return;
        EnemyIdleWalk();

        
    }

    void Update()
    {
        if (!canWalk) return;
        if (walking) PathCompleted();
    }

    void EnemyIdleWalk()
    {
        target = transform.position;

        //pick a random direction to move
        Vector3 randomDirection = Random.insideUnitSphere * maxIdleWalkDistance;

        //add the random direction to the enemy's position
        randomDirection += transform.position;

        //create a new NavMeshHit to store the hit information
        NavMeshHit hit;

        //find a random position on the NavMesh and store the information in the hit variable
        NavMesh.SamplePosition(randomDirection, out hit, Random.Range(0f, maxIdleWalkDistance), 1);

        target = hit.position;

        //set the destination to the hit position
        navMeshAgent.SetDestination(target);
        // Debug.Log("Enemy is walking");
        walking = true;

       
    }

    void PathCompleted()
    {
        if (!canWalk) return;

        float dist=navMeshAgent.remainingDistance;
        if (dist == Mathf.Infinity)
        {
            return;
        }

        if (navMeshAgent.pathStatus==NavMeshPathStatus.PathComplete && navMeshAgent.remainingDistance==0)
        {
            if (idleWaitTime == null)
            {
                idleWaitTime = StartCoroutine(IdleWaitTime(_minMaxIdleWaitTime));
            }
            else
            {
                StopCoroutine(idleWaitTime);
                idleWaitTime = StartCoroutine(IdleWaitTime(_minMaxIdleWaitTime));
            }
            return;
        }

        return;

    }

    IEnumerator IdleWaitTime(Vector2 waitTime)
    {
        target = transform.position;
        walking = false;
        // Debug.Log("Enemy is waiting");
        yield return new WaitForSeconds(Random.Range(waitTime.x, waitTime.y));
        EnemyIdleWalk();
    }



    public void EnemyTargetLogic()
    {
        if (!canWalk) return;

        Debug.Log("Enemy is targeting");
        navMeshAgent.SetDestination(target);
        walking = true;
    }
}

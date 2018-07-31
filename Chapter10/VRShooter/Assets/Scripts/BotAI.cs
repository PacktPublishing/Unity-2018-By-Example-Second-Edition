using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotAI : MonoBehaviour
{
    public enum AISTATE { CHASE = 0, ATTACK = 1 };
    public AISTATE CurrentState
    {
        get { return _CurrentState; }
        set 
        {
            StopAllCoroutines();
            _CurrentState = value;

            switch(_CurrentState)
            {
                case AISTATE.CHASE:
                    StartCoroutine(StateChase());
                    break;

                case AISTATE.ATTACK:
                    StartCoroutine(StateAttack());
                    break;
            }
        }
    }

    [SerializeField]
    private AISTATE _CurrentState = AISTATE.CHASE;
    private NavMeshAgent ThisAgent = null;
    private Transform ThisPlayer = null;
    private Transform ThisTransform = null;
    public ParticleSystem WeaponPS = null;

    private void Awake()
    {
        ThisAgent = GetComponent<NavMeshAgent>();
        ThisPlayer = GameObject.FindWithTag("Player").GetComponent<Transform>();
        ThisTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        CurrentState = _CurrentState;
    }

    private void OnEnable()
    {
        CurrentState = AISTATE.CHASE;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        WeaponPS.Stop();
    }

    public IEnumerator StateChase()
    {
        WeaponPS.Stop();
        ThisAgent.SetDestination(ThisPlayer.position);

        while (CurrentState == AISTATE.CHASE)
        {
            //Check distance
            float DistancetoDest = Vector3.Distance(ThisTransform.position, ThisPlayer.position);

            if (Mathf.Approximately(DistancetoDest, ThisAgent.stoppingDistance) || DistancetoDest <= ThisAgent.stoppingDistance)
            {
                CurrentState = AISTATE.ATTACK;
                yield break;
            }
        
            yield return null;
        }
    }

    public IEnumerator StateAttack()
    {
        WeaponPS.Play();

        while (CurrentState == AISTATE.ATTACK)
        {
            Vector3 Dir = (ThisPlayer.position - ThisTransform.position).normalized;
            Dir.y = 0;
            ThisTransform.rotation = Quaternion.LookRotation(Dir, Vector3.up);
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverAIScript : MonoBehaviour
{
    public TrayScript[] machines;
    public CarBoxesScript[] cars;

    public Vector3[] machinePos;
    public Vector3[] carPos;
    public Vector3 targetMachine = Vector3.zero;
    public Vector3 targetCar = Vector3.zero;

    public bool stateIdle = true;
    public bool stateMovingToMachine = false;
    public bool stateCollectingBoxes = false;
    public bool stateMovingToCar = false;
    public bool stateKeepingBoxes = false;
    public bool isUpgradeSpeed = false;

    private int lastMaxCount = 0;
    private int leastCarBoxCount = 10000;
    private int targetMachineid = 0;
    private int targetCarid = 0;

    public MoverBoxScript playerBoxScript;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (navMeshAgent.velocity.magnitude / navMeshAgent.speed < 0.2f)
        {
            animator.SetFloat("Blend", 0f, 0.2f, Time.deltaTime);
        }
        else if (navMeshAgent.velocity.magnitude / navMeshAgent.speed < 0.21f)
        {

        }
        else
        {
            animator.SetFloat("Blend", 0.5f, 0.2f, Time.deltaTime);
        }*/
        animator.SetFloat("Blend", navMeshAgent.velocity.magnitude / navMeshAgent.speed, 0.2f, Time.deltaTime);

        if (isUpgradeSpeed)
        {
            navMeshAgent.speed = 15f;
        }
        if (stateIdle)
        {
            targetMachine = Vector3.zero;
            for (int i = 0; i < machines.Length; i++)
            {
                if (machines[i].activeBoxesList.Count >= lastMaxCount && machines[i].gameObject.activeInHierarchy)
                {
                    if (!machines[i].isAiTarget)
                    {
                        targetMachine = machinePos[i];
                        lastMaxCount = machines[i].activeBoxesList.Count;
                        targetMachineid = i;
                    }                 
                }
            }
            if (targetMachine != Vector3.zero)
            {
                machines[targetMachineid].isAiTarget = true;
                lastMaxCount = 0;
                stateMovingToMachine = true;
                stateIdle = false;
            }
        }
        if (stateMovingToMachine)
        {
            navMeshAgent.destination = targetMachine;
            //if (navMeshAgent.remainingDistance < 0.1f)
            if (Vector3.Distance(transform.position, targetMachine) < 0.2f)   //can use stoppingdistance
            {
                transform.position = targetMachine;
                stateMovingToMachine = false;
                stateCollectingBoxes = true;
            }
        }
        if (stateCollectingBoxes)
        {
            if (playerBoxScript.boxNumber >= 10 || machines[targetMachineid].isMoverInArea == false)
            {
                targetCar = Vector3.zero;
                machines[targetMachineid].isAiTarget = false;
                for (int i = 0; i < cars.Length; i++)
                {
                    if (cars[i].boxCountnum <= leastCarBoxCount && cars[i].gameObject.activeInHierarchy)
                    {
                        if (!cars[i].isAiTarget)
                        {
                            targetCar = carPos[i];
                            leastCarBoxCount = cars[i].boxCountnum;
                            targetCarid = i;
                        }
                    }
                }
                if (targetCar != Vector3.zero)
                {
                    cars[targetCarid].isAiTarget = true;
                    leastCarBoxCount = 1000;
                    stateCollectingBoxes = false;
                    stateMovingToCar = true;
                }
            }
        }
        if (stateMovingToCar)
        {           
            navMeshAgent.destination = targetCar;
            if (Vector3.Distance(transform.position, targetCar) < 0.2f)
            {
                transform.position = targetCar;
                stateMovingToCar = false;
                stateKeepingBoxes = true;
            }
        }
        if (stateKeepingBoxes)
        {
            if (playerBoxScript.boxNumber == 0 || cars[targetCarid].isMoverInArea == false)
            {
                cars[targetCarid].isAiTarget = false;
                stateKeepingBoxes = false;
                stateIdle = true;
            }
        }
    }
}

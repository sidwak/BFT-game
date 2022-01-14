using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuyerAiScript : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3[] racksPos;
    public Vector3 targetRack;

    public bool isMoving = true;
    public bool stateIdle = false;
    public bool stateMovingToRack = false;
    public bool stateCollectingBoxes = false;
    public bool stateGoingBack = false;
    public bool stateResetBox = false;

    private int leastRackBoxCount = 0;
    private int targetRackid = 0;

    public float startDelay;

    public MoverBoxScript playerBoxScript;
    public RackBoxHolderScript[] racks;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        stateIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Blend", navMeshAgent.velocity.magnitude / navMeshAgent.speed, 0.2f, Time.deltaTime);

        if (stateIdle)
        {
            for (int i = 0; i < racks.Length; i++)
            {
                if (racks[i].boxCountnum >= leastRackBoxCount && racks[i].gameObject.activeInHierarchy)
                {
                    if (!racks[i].isBuyerAiTarget)
                    {
                        targetRack= racksPos[i];
                        leastRackBoxCount = racks[i].boxCountnum;
                        targetRackid = i;
                    }
                }
            }
            racks[targetRackid].isBuyerAiTarget = true;
            leastRackBoxCount = 0;
            stateMovingToRack = true;
            stateIdle = false;
        }
        if (stateMovingToRack)
        {
            navMeshAgent.destination = targetRack;
            if (Vector3.Distance(transform.position, targetRack) < 0.2f)   //can use stoppingdistance
            {
                transform.position = targetRack;
                stateMovingToRack = false;
                stateCollectingBoxes = true;
                isMoving = false;
            }
        }
        if (stateCollectingBoxes)
        {
            StartCoroutine(WaitForBoxes());
            if (playerBoxScript.boxNumber >= 5)
            {
                racks[targetRackid].isBuyerAiTarget = false;
                stateCollectingBoxes = false;
                stateGoingBack = true;
                isMoving = true;
            }
        }
        if (stateGoingBack)
        {
            navMeshAgent.destination = startPos;
            if (Vector3.Distance(transform.position, startPos) < 1f)   //can use stoppingdistance
            {
                transform.position = startPos;
                stateGoingBack = false;
                stateResetBox = true;
            }
        }
        if (stateResetBox)
        {
            for (int i = 0; i < playerBoxScript.PlayerBoxesList.Count; i++)
            {
                if (playerBoxScript.PlayerBoxesList[i].activeInHierarchy)
                {
                    playerBoxScript.PlayerBoxesList[i].SetActive(false);
                    playerBoxScript.boxNumber--;
                }
            }
            stateResetBox = false;
            stateIdle = true;
        }

        IEnumerator WaitForBoxes()
        {
            yield return new WaitForSeconds(5f);  //previous
            if (stateCollectingBoxes)
            {
                if (!stateGoingBack)
                {
                    stateCollectingBoxes = false;
                    stateGoingBack = true;
                }
            }
        }
    }
}

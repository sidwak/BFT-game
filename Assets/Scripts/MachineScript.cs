using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    public Vector3 endPosition;

    public float spawnSpeed = 0.5f;
    public float upgradeSpawnSpeed = 0.25f;
    public float textureSpeed = 2f;
    private float timeTospawn = 0f;

    public bool isStop = false;
    public bool isBuy = false;
    public bool isDoneUpgrade = false;

    public GameObject machineBox;
    public GameObject player;
    public GameObject canvas;

    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LerpPosition(new Vector2(0f, 1f), 2));
        Animator animator = GetComponentInParent<Animator>();
        if (isBuy)
        {      
            animator.SetBool("isNew", true);
            isBuy = false;
            timeTospawn = Time.time + 0.6f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeTospawn && !isStop)
        {
            GameObject num = Instantiate(machineBox, transform.position, machineBox.transform.rotation);
            num.AddComponent<MachineBoxScript>();
            num.GetComponent<MachineBoxScript>().endPosition = endPosition;
            num.GetComponent<MachineBoxScript>().StartLerp();

            //canvas.GetComponent<CanvasScript>().totalBoxes++;
            if (isDoneUpgrade)
            {
                spawnSpeed = upgradeSpawnSpeed;
            }
            timeTospawn = Time.time + spawnSpeed;
        }

        if (mat.mainTextureOffset.y == 1f)
        {
            mat.mainTextureOffset = Vector2.zero;
            if (!isDoneUpgrade)
            {
                StartCoroutine(LerpPosition(new Vector2(0f, 1f), 2));
            }
            else
            {
                StartCoroutine(LerpPosition(new Vector2(0f, 1f), 4));
            }
        }
    }

    IEnumerator LerpPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = mat.mainTextureOffset;

        while (time < duration)
        {
            mat.mainTextureOffset = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        mat.mainTextureOffset = targetPosition;
    }
}

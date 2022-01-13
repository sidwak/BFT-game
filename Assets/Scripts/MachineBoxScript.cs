using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBoxScript : MonoBehaviour
{
    public float boxSpeed = 2f;

    public bool isTargetset = false;

    public Vector3 endPosition;

    public GameObject targetobject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargetset)
        {
            transform.position = Vector3.Lerp(transform.position, targetobject.transform.position, 10f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetobject.transform.rotation, 10f * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetobject.transform.position) < 0.5f)
            {
                isTargetset = false;
                targetobject.SetActive(true);              
                Destroy(gameObject);
            }
        }
        
    }

    public void StartLerp()
    {
        StartCoroutine(LerpPosition(endPosition, 2));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimationScript : MonoBehaviour
{
    public float animationLength = 3f;

    public GameObject endTarget;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(EndAnimation());

    }

    // Update is called once per frame
    void Update()
    {
        /*transform.position = Vector3.Lerp(transform.position, endTarget.transform.position, 5f * Time.deltaTime);
        if (Vector3.Distance(transform.position, endTarget.transform.position) < 1f)
        {
            Destroy(gameObject);
        }*/

    }

    IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(animationLength);
        Destroy(gameObject);
    }
}

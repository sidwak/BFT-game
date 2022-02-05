using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 car1;
    public Vector3 car2;
    public Vector3 car6;
    public Vector3 machine2;
    public Vector3 machine3;
    public Vector3 machine4;
    public Vector3 machine7;
    public Vector3 truck1;
    public Vector3 rack2;
    public Vector3 vendingMachine;
    public Vector3 shop;


    public Vector3 zone2;
    public Vector3 zone3;
    public Vector3 zone4;
    public Vector3 level2;

    public Vector3 startPos;

    public bool isShopLerp = false;
    public bool isLerpCalled = false;

    public CharacterScript player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Machine1Lerp()
    {
        //player.isLerping = true;
        if (isLerpCalled == false)
        {
            StartCoroutine(LerpPosition(car1, 1f, 3f));
            isLerpCalled = true;
        }
    }
    public void Car1Lerp()
    {
        //player.isLerping = true;
        if (isLerpCalled == false)
        {
            StartCoroutine(LerpPosition(machine3, 2f, 3f));
            isLerpCalled = true;
        }
    }
    public void Machine3Lerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(machine2, 1f, 2f));
            isLerpCalled = true;
        }
    }
    public void Machine2Lerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(car2, 1f, 0f));
            isLerpCalled = true;
        }
    }
    public void Car2Lerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(zone2, 2f, 0f));
            isShopLerp = true;
            isLerpCalled = true;
        }
    }
    
    public void Zone2Lerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(rack2, 1f, 0f));
            isLerpCalled = true;
        }
    }
    public void Rack3or4Lerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(vendingMachine, 2f, 0f));
            isLerpCalled = true;
        }
    }
    public void VendingMachineLerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(zone3, 2f, 2f));
            isLerpCalled = true;
        }
    }

    public void Zone3Lerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(machine4, 1f, 0f));
            isLerpCalled = true;
        }
    }
    public void Machine4Lerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(truck1, 1f, 2f));
            isLerpCalled = true;
        }
    }
    public void Truck2orMachine6Lerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(zone4, 2f, 0f));
            isLerpCalled = true;
        }
    }

    public void Zone4Lerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(machine7, 1f, 0f));
            isLerpCalled = true;
        }
    }
    public void Machine7Lerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(car6, 1f, 2f));
            isLerpCalled = true;
        }
    }
    public void Car6Lerp()
    {
        if (isLerpCalled == false)
        {
            //player.isLerping = true;
            StartCoroutine(LerpPosition(level2, 1f, 0f));
            isLerpCalled = true;
        }
    }

    /*public void Zone1Lerp()   //for ref
    {
        player.isLerping = true;
        StartCoroutine(LerpPosition(zone2, 2f));
    }*/

    IEnumerator LerpPosition(Vector3 targetPosition, float duration, float waitDelay)
    {
        yield return new WaitForSeconds(waitDelay);
        player.isLerping = true;
        float time = 0;
        Vector3 startPosition = transform.position;
        startPos = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        if (!isShopLerp)
        {
            StartCoroutine(WaitForTime());
        }
        else
        {
            isShopLerp = false;
            StartCoroutine(LerpPosition2(shop, 2f));
        }
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(LerpBack(startPos, 2f));
    }

    IEnumerator LerpBack(Vector3 targetPosition, float duration)
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
        player.isLerping = false;
        if (isShopLerp)
        {
            isShopLerp = false;
        }
        isLerpCalled = false;
    }

    IEnumerator LerpPosition2(Vector3 targetPosition, float duration)
    {
        yield return new WaitForSeconds(1f);
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        StartCoroutine(WaitForTime());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorEndScript : MonoBehaviour
{

    public int MaxTrayBoxes;

    public GameObject trayObject;
    public GameObject Tray;
    public GameObject machineSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trayObject.GetComponent<TrayScript>().activeBoxesList.Count < MaxTrayBoxes)
        {
            machineSpawner.GetComponent<MachineScript>().isStop = false;
        }
    }

    private void OnTriggerEnter(Collider other)   //ERROR SOME BOXES COMING AFTER PLAYER PICKED IT UP AND DOUBLE BOXES AT ONE PLACE
    {
        if (other.gameObject.name == "MainPlayer" || other.gameObject.name == "MoverAI")
        {
            return;
        }
        if (trayObject.GetComponent<TrayScript>().activeBoxesList.Count >= MaxTrayBoxes)
        {
            machineSpawner.GetComponent<MachineScript>().isStop = true;
            Destroy(other.gameObject);
            return;
        }
        int numBox = trayObject.GetComponent<TrayScript>().activeNoBoxes;
        GameObject num = trayObject.GetComponent<TrayScript>().inactiveBoxesList[0];
        if (num.activeInHierarchy)
        {
            trayObject.GetComponent<TrayScript>().activeBoxesList.Add(num);
            trayObject.GetComponent<TrayScript>().inactiveBoxesList.RemoveAt(0);
            //Destroy(other.gameObject);
            OnTriggerEnter(other);
            return;
        }
        trayObject.GetComponent<TrayScript>().inactiveBoxesList.RemoveAt(0);
        trayObject.GetComponent<TrayScript>().activeBoxesList.Add(num);     
        //BUT WHAT IF NEXT BOX IS ALSO TURNED ON   FIND GOOD SOLUTION
        other.gameObject.GetComponent<MachineBoxScript>().targetobject = num;
        other.gameObject.GetComponent<MachineBoxScript>().isTargetset = true;
        trayObject.GetComponent<TrayScript>().activeNoBoxes++;
        Destroy(other.gameObject.GetComponent<BoxCollider>());
        Destroy(other.gameObject.GetComponent<Rigidbody>());
    }

}

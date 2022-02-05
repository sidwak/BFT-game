using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "L4_SaveData.json"))
        {
            SceneManager.LoadScene(4);
        }
        else if (File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "L3_SaveData.json"))
        {
            SceneManager.LoadScene(3);
        }
        else if (File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "L2_SaveData.json"))
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

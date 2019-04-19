using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switch_scene : MonoBehaviour
{

    public void load_scene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
     

}

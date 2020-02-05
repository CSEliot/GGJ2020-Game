using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadMainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Reload());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Reload() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}

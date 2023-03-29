using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Go());
    }
    private IEnumerator Go()
    {
        while (true) { 
            yield return new WaitForSeconds(1);
            
        }
    }
}

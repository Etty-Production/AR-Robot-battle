using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgramManeger : MonoBehaviour
{
    private ARRaycastManager ARRaycastManagerScript;
    private Vector2 TouchPosition;
    public GameObject ObjectToSpawn;
    public GameObject Cam;
    public GameObject Triger;
    //public GameObject Test;
    public int KolEnemy_s;
    public bool Wait;
    public AudioSource FireSound;
    void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
        Triger.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    // Update is called once per frame
    void Update()
    {

        if (KolEnemy_s < 10 && !Wait)
        {
            Wait = true;
            StartCoroutine(SetClone());
        }
        Ray ray = new Ray(Cam.transform.position, Cam.transform.forward);
        Debug.DrawRay(Cam.transform.position, Cam.transform.forward * 100f, Color.yellow);
        //FireSound.Play();
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Triger.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
        else
        {
            Triger.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }


    }
    public void Fire()
    {
        Ray ray = new Ray(Cam.transform.position, Cam.transform.forward);
        Debug.DrawRay(Cam.transform.position, Cam.transform.forward * 100f, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject obj = hit.collider.gameObject;
            obj.GetComponent<HealthBarEnemy>().Damage(20f);
            FireSound.Play();

        }
    }
    private IEnumerator SetClone()
    {
        //Test.transform.position += new Vector3(1.0f, 0, 0);
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        ARRaycastManagerScript.Raycast(new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height)), hits, TrackableType.Planes);
        if (hits.Count > 0)
        {
            Instantiate(ObjectToSpawn, hits[0].pose.position, ObjectToSpawn.transform.rotation);
            KolEnemy_s += 1;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
        Wait = false;
    }
}

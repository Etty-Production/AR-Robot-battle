using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInPerson : MonoBehaviour
{
    private Ray _ray;
    private RaycastHit _hit;
    [Header("Player, wich enemy look at/")]
    [SerializeField] private GameObject Camera;
    public bool Shoot;
    public bool Run;
    public float DamageEnemy = 20;
    private float speed;
    Animator anim;   
    string[] Anims = new string[] { "Shoot", "Run"};
    bool[] EndsOfAnim = new bool[2];
    [SerializeField] private GameObject Gun;

    private void UpdateRay()
    {
        _ray = new Ray(Gun.transform.position, Gun.transform.forward);
    }

    private void DrawRay()
    {
        Debug.DrawRay(_ray.origin, _ray.direction * 100f, Color.yellow);
        if (Physics.Raycast(_ray, out _hit))
        {
            Debug.DrawRay(_ray.origin, _ray.direction * 100f, Color.red);
        }
    }

    private void Start()
    {
        speed = 0.7f;
        anim = gameObject.GetComponent<Animator>();
    }
    private void StopIt(string S)
    {
        anim.SetBool(S, false);
        anim.SetBool("EndOf" + S, true);
    }
    private IEnumerator Wait(string Name, float StopTime)
    {
        yield return new WaitForSeconds(StopTime);
        StopIt(Name);
    }

    private void Update()
    {
        bool AnimWork = false;
        for (int i = 0; i < Anims.Length; i++)
            if (anim.GetBool(Anims[i]) ^ anim.GetBool("EndOf" + Anims[i])) AnimWork = true;
        if (!AnimWork)
        {
            int Rnd = Random.Range(0, Anims.Length);
            anim.SetBool(Anims[Rnd], true);
            EndsOfAnim[Rnd] = true;
        }
        if (anim.GetBool("Shoot"))
        {
            if (Shoot) 
            {
                Shoot = false;
                Gun.transform.LookAt(Camera.transform, Vector3.up);
                Gun.transform.rotation = new Quaternion(Gun.transform.rotation.x + Random.Range(0.0f, 0.02f) * Random.Range(-1, 2),
                    Gun.transform.rotation.y + Random.Range(0.0f, 0.02f) * Random.Range(-1, 2), Gun.transform.rotation.z + Random.Range(0.0f, 0.02f) * Random.Range(-1, 2),
                        Gun.transform.rotation.w + Random.Range(0.0f, 0.02f) * Random.Range(-1, 2));
                UpdateRay();
                DrawRay();
                if (EndsOfAnim[0])
                {
                    StartCoroutine(Wait("Shoot", Random.Range(5, 15)));//Random.Range(0, 1)
                    EndsOfAnim[0] = false;
                }
                if (Physics.Raycast(_ray, out _hit)) 
                {
                    GameObject obj = _hit.collider.gameObject;
                    if (obj = Camera)
                    {
                        // damage for player here
                        obj.GetComponent<DamageForPerson>().Damage(DamageEnemy);
                    }
                }
            }
        }
        if (anim.GetBool("Run"))
        {
            if (EndsOfAnim[1])
            {
                StartCoroutine(Wait("Run", Random.Range(2, 5)));
                EndsOfAnim[1] = false;
            }
            Vector3 Step = (Camera.transform.position - gameObject.transform.position).normalized * speed * Time.deltaTime;
            Step.y = 0.0f;
            gameObject.transform.position += Step;
            if ((Camera.transform.position - gameObject.transform.position).sqrMagnitude < 0.01f) Destroy(gameObject);
        }
        if (anim.GetBool("Ded"))
        {
            for (int i = 0; i < Anims.Length; i++)
            {
                anim.SetBool(Anims[i], false);
                anim.SetBool("EndOf" + Anims[i], true);
            }
        }
    }
}


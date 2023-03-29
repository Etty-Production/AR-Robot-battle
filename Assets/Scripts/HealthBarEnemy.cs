using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float HP = 100f;
    public GameObject HelthBar;
    private float HP1;
    public void Start()
    {
        HelthBar.transform.position = gameObject.transform.position + new Vector3(0.0f, 0.42f, 0.0f);
        HP1 = HelthBar.transform.localScale.z / HP;
    }
    // Update is called once per frame

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        GameObject End;
        End = GameObject.Find("AR Session Origin");
        End.GetComponent<ProgramManeger>().KolEnemy_s -= 1;
    }
    public void Damage(float DamageHP)
    {
        HP -= DamageHP;
        HelthBar.transform.localScale = HelthBar.transform.localScale - new Vector3(0.0f, 0.0f, HP1 * DamageHP);
        if (HP < 1)
        {
            Animator anim = gameObject.GetComponent<Animator>();
            anim.SetBool("Ded", true);
            Destroy(HelthBar);
            StartCoroutine(Die());
        }

    }

}

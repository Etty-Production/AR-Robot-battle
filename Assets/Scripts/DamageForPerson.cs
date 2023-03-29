using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageForPerson : MonoBehaviour
{
    [SerializeField] private float Hp;
    public Image HelthBar;
    float OneHpDamage;
    GameObject find;
    public AudioSource DamageSound;
    private void Start()
    {
        DamageSound.Play();
        OneHpDamage = 1 / Hp;
        find = GameObject.Find("Canvas/GameMarcer/DamageAnim");
    }
    private IEnumerator Wait(float StopTime)
    {
        yield return new WaitForSeconds(StopTime);
        find.SetActive(false);
    }
    public void Damage(float Damage)
    {
        Hp = Hp - Damage;
        find.SetActive(true);
        StartCoroutine(Wait(0.35f));
        HelthBar.fillAmount -= OneHpDamage * Damage; 
        if (Hp < 1)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            GameObject End;
            End = GameObject.Find("Canvas");
            End.GetComponent<MenuScript>().PersonDed();
        }
    }
}

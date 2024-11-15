using System.Collections;
using UnityEngine;
public class DamageableArea : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float attackCooldownTime = 10f;

    Collider trigger;

    private void Awake()
    {
        trigger = GetComponent<Collider>();
    }

    private void Start()
    {
        damage = -damage;
        StartCoroutine(CooldownAttact());
    }

    public void Attack(HealthManager healthManager)
    {
        healthManager.Heal(damage);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack(collision.gameObject.GetComponent<HealthManager>());
            StartCoroutine(CooldownAttact());
        }
            
    }

    IEnumerator CooldownAttact()
    {
        trigger.enabled = false;
        yield return new WaitForSeconds(attackCooldownTime);
        trigger.enabled = true;

    }
}

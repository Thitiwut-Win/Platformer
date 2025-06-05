using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damage;
    public Vector2 power;
    public float cooldown;
    private bool isActivated;
    public virtual bool OnTriggerEnter2D(Collider2D collider2D)
    {
        if (!isActivated &&collider2D.TryGetComponent(out BaseUnit baseUnit))
        {
            isActivated = true;
            baseUnit.GetHit(damage);
            baseUnit.GetKnockback(power);
            StartCoroutine(Cooldown());
            return true;
        }
        return false;
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isActivated = false;
    }
}
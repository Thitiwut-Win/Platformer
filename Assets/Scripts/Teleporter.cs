using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 destination;
    public float delayTime;
    private bool isCharging;
    private Player player;
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player player))
        {
            isCharging = true;
            this.player = player;
            StartCoroutine(Countdown());
        }
    }
    public void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player player))
        {
            isCharging = false;
            this.player = null;
        }
    }
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(delayTime);
        if (isCharging) Teleport();
    }
    private void Teleport()
    {
        player.transform.position = destination;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZoneScript : MonoBehaviour
{
    public delegate void OnPlayerDamaged();
    public static event OnPlayerDamaged onDamagePlayer;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "IgnoreBall")
        onDamagePlayer?.Invoke();


        if (other.gameObject.tag == "BossBall")
        {
            Camera.main.gameObject.GetComponent<GameController>().DamagePlayer(1000);
        }
        Destroy(other.gameObject);
    }
}

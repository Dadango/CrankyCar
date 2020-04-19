using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStationSafeZone : MonoBehaviour
{
    public EntityScript entity;
    private void OnTriggerExit(Collider other)
    {
      /*if (other.CompareTag("Player"))*/ { entity.enabled = true;  }
    }
}

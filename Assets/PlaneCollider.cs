using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlaneCollider : NetworkBehaviour {
	public bool enter = true;

   	public Health health;


    private void OnTriggerEnter(Collider other)
    {
		if (!isServer){
            return;
        }

		health = (Health)other.GetComponent("Health");
		health.RpcRespawn();
    }

}

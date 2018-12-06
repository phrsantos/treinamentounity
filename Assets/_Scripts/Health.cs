using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
    public const int maxHealth = 100;
    public RectTransform healthBar;

    public GameObject SpawnPositions;
    public bool destroyOnDeath;

    [SyncVar (hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    void Start() {
        GameObject spawnPositions = GameObject.FindGameObjectWithTag("PlayerSpawnPosition");
        SpawnPositions = spawnPositions;
    }

    void UpdateBar(int health){
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    void Update(){


    }

    public void TakeDamage(int amount) {
        if (!isServer){
            return;
        }

        currentHealth -= amount;
        UpdateBar(currentHealth);

        if (currentHealth <= 0) {
            currentHealth = maxHealth;
            RpcRespawn();
        }
    }

    public void OnChangeHealth(int health) {
        UpdateBar(health);
    }

    [ClientRpc]
    public void RpcRespawn() {
        if (destroyOnDeath) {
            Destroy(gameObject);
        } else {
            if (isLocalPlayer) {
                int amountChildren = SpawnPositions.transform.childCount;
                Transform spawnPositionsTransform = SpawnPositions.transform.GetChild(Random.Range(0, amountChildren));
                transform.position = spawnPositionsTransform.position;
            }
        }
    }
}
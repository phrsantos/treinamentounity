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

    private OnlineCanvasUpdate onlineCanvas;

    [SyncVar (hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    void Start() {
        onlineCanvas = GameObject.Find("OnlineCanvas").GetComponent<OnlineCanvasUpdate>();
        GameObject spawnPositions = GameObject.FindGameObjectWithTag("PlayerSpawnPosition");
        SpawnPositions = spawnPositions;
    }

    void UpdateBar(int health){
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    public void TakeDamage(int amount) {
        if (!isServer){
            return;
        }

        currentHealth -= amount;
        UpdateBar(currentHealth);

        if (currentHealth <= 0) {
            RpcRespawn();
        }

    }

    public void OnChangeHealth(int health) {
        UpdateBar(health);

        if (isLocalPlayer){
            onlineCanvas.UpdateHealthText(health);
        }
    }

    [ClientRpc]
    public void RpcRespawn() {
        if (isServer){
            currentHealth = maxHealth;
        }

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
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
    private Score score;

    [SyncVar (hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    void Start() {
        onlineCanvas = GameObject.Find("OnlineCanvas").GetComponent<OnlineCanvasUpdate>();
        GameObject spawnPositions = GameObject.FindGameObjectWithTag("PlayerSpawnPosition");
        SpawnPositions = spawnPositions;
    }

    public override void OnStartClient(){
        score = gameObject.GetComponent<Score>();
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
            score.ZeroScore();
        }

        if (destroyOnDeath) {
            Destroy(gameObject);
        } else {
            if (isLocalPlayer) {
                // score.ResetAndSaveScore();
                int amountChildren = SpawnPositions.transform.childCount;
                Transform spawnPositionsTransform = SpawnPositions.transform.GetChild(Random.Range(0, amountChildren));
                transform.position = spawnPositionsTransform.position;
            }
        }
    }
}
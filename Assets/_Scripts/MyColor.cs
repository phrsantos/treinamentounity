using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyColor : NetworkBehaviour {
    [SyncVar (hook = "UpdateColor")]
    public Color myCurrentColor;

    // Listener
    void UpdateColor(Color myCurrentColor) {
        GetComponent<MeshRenderer>().material.color = myCurrentColor;
    }

    public override void OnStartClient()
    {
        UpdateColor(myCurrentColor);
    }

    [Command]
    void CmdChangeColor(float red, float green, float blue) {
            myCurrentColor = new Color(red, green, blue);
    }


    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
           CmdChangeColor(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}
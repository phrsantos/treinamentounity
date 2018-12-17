﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChangeScene : NetworkBehaviour {
    public GameObject offineCanvas;
    public GameObject onlineCanvas;

    private string submittedName;

    public InputField playerName;

    void Awake(){
        var input = playerName.GetComponent<InputField>();
        input.onEndEdit.AddListener(SubmitName);
    }

    void SubmitName(string name){
        PlayerPrefs.SetString("Nickname", name);
    }

    public void StartHost() {
        offineCanvas.SetActive(false);
        onlineCanvas.SetActive(true);

        NetworkManager.singleton.StartHost();
    }

    public void StartClient()
    {
        offineCanvas.SetActive(false);
        onlineCanvas.SetActive(true);

        NetworkManager.singleton.networkAddress = "localhost";
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StartClient();
    }

    public void Stop()
    {
        offineCanvas.SetActive(true);
        onlineCanvas.SetActive(false);
        NetworkManager.singleton.StopServer();


        if (isServer)
        {
            NetworkManager.singleton.StopServer();
        }
        else
        {
            NetworkManager.singleton.StopClient();
        }
    }


}

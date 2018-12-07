using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class CameraController : NetworkBehaviour {
	void Start () {
		Camera sceneCamera = Camera.main;
		if (isLocalPlayer) {
			sceneCamera.transform.parent = gameObject.transform;
			sceneCamera.transform.localPosition = new Vector3(-0.88f, 4f, -10.17f);
		}
	}
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private Rigidbody rb;
    private float velocity = 1;
    private float speedModifier = 1;
    private float dashCooldown = 5.0f;
    private float dashCooldownTotalTime = 5.0f;
    private float dashTime = 0;
    private bool isGrounded = false;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    [Command]
    void CmdFire() {
       var bullet = (GameObject)Instantiate(
           bulletPrefab,
           bulletSpawn.position,
           bulletSpawn.rotation
        );

       bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;

        NetworkServer.Spawn(bullet);

       Destroy(bullet, 2.0f);
   }

   public override void OnStartLocalPlayer() {
   }

   // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            velocity = 3;
        }else {
            velocity = 1;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, y * velocity);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
        CmdFire();
        }


        // JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            isGrounded = false;
        }

        //DASH
        if (Input.GetKey (KeyCode.LeftAlt) && dashTime == 0 && dashCooldown == dashCooldownTotalTime) {
            dashCooldown = 0.0f;
            dashTime = 1;
            GameObject UICanvas = GameObject.Find("UICanvas");
            UpdateUIInfo uiInfo = UICanvas.GetComponent<UpdateUIInfo>();
            uiInfo.StartDashTimer(dashCooldownTotalTime);
        }

        if(dashCooldown >= dashCooldownTotalTime)
        {
            //reactivate dash
            dashCooldown = dashCooldownTotalTime;
        }else{
            //decrement dash timer
            dashCooldown += Time.deltaTime;
        }

        if (dashTime > 0) {
            speedModifier = 10.5f;
            rb.velocity = transform.forward * velocity * speedModifier;
            dashTime--;
        }

        if (dashTime == 0) {
            speedModifier = 1.0f;
        }




   }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Ground"))
        {
            isGrounded = true;
        }
    }
}
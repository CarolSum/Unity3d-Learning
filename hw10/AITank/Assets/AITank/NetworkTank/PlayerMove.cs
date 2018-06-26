using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    public GameObject bulletPrefabs;

    public override void OnStartClient()
    {
        Camera.main.GetComponent<MainCameraControl>().setTarget(this.transform);
    }

    public override void OnStartServer()
    {
        Camera.main.GetComponent<MainCameraControl>().setTarget(this.transform);
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        float offsetX = Input.GetAxis("Horizontal");
        float y = this.transform.localEulerAngles.y + offsetX * 5;
        float x = this.transform.localEulerAngles.x;
        this.transform.localEulerAngles = new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            this.GetComponent<Rigidbody>().velocity = this.transform.forward * 20;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.GetComponent<Rigidbody>().velocity = this.transform.forward * -20;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdShoot();
        }
    }

    [Command]
    void CmdShoot()
    {
        var bullet = (GameObject)Instantiate(
            bulletPrefabs,
            new Vector3(transform.position.x, 1.5f, transform.position.z) + transform.forward * 1.5f,
            Quaternion.identity);

        bullet.transform.forward = transform.forward;

        NetworkServer.Spawn(bullet);
        RpcAddForceOnAll(bullet);

        // make bullet disappear after 2 seconds
        Destroy(bullet, 2.0f);

    }

    [ClientRpc]
    void RpcAddForceOnAll(GameObject bullet)
    {
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 20, ForceMode.Impulse);
    }
}

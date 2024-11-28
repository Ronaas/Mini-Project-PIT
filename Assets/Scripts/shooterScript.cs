using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ShooterScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform firepoint;

    private float bulletSpeed = 10;

    void Start()
    {
        XRGrabInteractable gunGrabable = GetComponent<XRGrabInteractable>();
        gunGrabable.activated.AddListener(BangBang);
    }

    void Update()
    {
        // No changes needed in Update
    }

    public void BangBang(ActivateEventArgs arg)
    {
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.position = firepoint.position;
        newBullet.GetComponent<Rigidbody>().velocity = firepoint.forward * bulletSpeed;

        Destroy(newBullet, 7); // Destroy bullet after 7 seconds
    }
}

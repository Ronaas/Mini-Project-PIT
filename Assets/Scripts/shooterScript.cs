using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class shooterScript : MonoBehaviour
{

    public GameObject bullet;
    public Transform firepoint;

    private float bulletSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable gunGrabable = GetComponent<XRGrabInteractable>();
        gunGrabable.activated.AddListener(bangBang);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bangBang(ActivateEventArgs arg)
    {
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.position = firepoint.position;
        newBullet.GetComponent<Rigidbody>().velocity = firepoint.forward * bulletSpeed;

        Destroy(newBullet, 7);
    }
}

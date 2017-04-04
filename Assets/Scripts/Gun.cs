using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform launchPosition;

	// Use this for initialization
    private void Start () {
		
	}
	
	// Update is called once per frame
    private void Update () {
	    if (Input.GetMouseButtonDown(0))
	    {
	        if (!IsInvoking(nameof(FireBullet)))
	        {
	            InvokeRepeating(nameof(FireBullet), 0f, 0.1f);
	        }
	    }

	    if (Input.GetMouseButtonUp(0))
	    {
	        CancelInvoke(nameof(FireBullet));
	    }
	}

    private void FireBullet()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = launchPosition.position;
        bullet.GetComponent<Rigidbody>().velocity = transform.parent.forward * 100;
    }
}

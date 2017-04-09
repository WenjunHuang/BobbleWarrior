using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform launchPosition;

    private AudioSource _audioSource;

	// Use this for initialization
    public void Start ()
    {
        _audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(_audioSource);
    }
	
	// Update is called once per frame
    public void Update () {
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

    protected void FireBullet()
    {
        CreateBullet();
        PlayGunShotSound();
    }

    protected GameObject CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = launchPosition.position;
        bullet.GetComponent<Rigidbody>().velocity = transform.parent.forward * 100;
        return bullet;
    }

    private void PlayGunShotSound()
    {
        _audioSource.PlayOneShot(SoundManager.Instance.GunFire);
    }
}

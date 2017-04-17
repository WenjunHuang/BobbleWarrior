using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform launchPosition;
    public float upgradeTime = 10.0f;
    public bool isUpgraded;
    private float _currentTime;

    private AudioSource _audioSource;


    // Use this for initialization
    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(_audioSource);
    }

    // Update is called once per frame
    public void Update()
    {
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

    public void UpgradeGun()
    {
        isUpgraded = true;
        _currentTime = 0.0f;
    }

    protected void FireBullet()
    {
        if (isUpgraded)
        {
            FireUpgradedBullet();
            PlayUpgradedGunShotSound();
        }
        else
        {
            FireNormalBullet();
            PlayGunShotSound();
        }
    }

    private void FireUpgradedBullet()
    {
        FireNormalBullet();

        var bullet2 = CreateBullet();
        bullet2.velocity = (transform.right + transform.forward / 0.5f) * 100;
        var bullet3 = CreateBullet();
        bullet3.velocity = (transform.right * -1 + transform.forward / 0.5f) * 100;
    }

    private void FireNormalBullet()
    {
        var bullet = CreateBullet();
        bullet.velocity = transform.parent.forward * 100;
    }

    private void PlayUpgradedGunShotSound()
    {
        _audioSource.PlayOneShot(SoundManager.Instance.UpgradedGunFire);
    }

    protected Rigidbody CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = launchPosition.position;
        return bullet.GetComponent<Rigidbody>();
    }

    private void PlayGunShotSound()
    {
        _audioSource.PlayOneShot(SoundManager.Instance.GunFire);
    }
}
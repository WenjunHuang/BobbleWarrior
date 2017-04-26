using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public Gun gun;

    public void Start()
    {
    }

    public void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        gun.UpgradeGun();
        Destroy(gameObject);
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.PowerUpPickup);
    }
}
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private LayerMask destroyableMask;
    [SerializeField] private ParticleSystem shootEffect;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private GameObject hitEffect;

    new Camera camera;
    new AudioSource audio;

    private bool canShoot = true;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        shootEffect.Play();
        audio.Play();
        gunAnimator.SetTrigger("Shoot");

        Ray ray = camera.ScreenPointToRay(new Vector3(camera.pixelWidth, camera.pixelHeight) * 0.5f);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            var target = hitInfo.collider.gameObject;
            if ((destroyableMask.value & 1 << target.layer) != 0)
            {
                Destroy(target);
            }

            Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        }

        StartCoroutine(WaitForShootingCooldown());
    }

    private IEnumerator WaitForShootingCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}

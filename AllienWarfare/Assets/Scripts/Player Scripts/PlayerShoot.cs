
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public RiffleWeapon weapon;
    //public ParticleSystem ps;
    //public GameObject impactEffect;

    public float nextTimeToFire = 0f;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: Camera not assigned");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + (1f / weapon.fireRate);
            //ps.Play();
            Shoot();
            /*InvokeRepeating("ps.Play()", 0f, weapon.fireRate);*/
        }
        else
        {
            //ps.Stop();
        }
    }

    private void Shoot() 
    {
        RaycastHit hit;
        ///if we hit something
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            Debug.Log("We hit " + hit.collider.name);
            if (hit.collider.CompareTag("Enemy"))
            {

                EnemyAI enemyAI = hit.transform.GetComponent<EnemyAI>();
                enemyAI.getHit();
                CharacterStats charStats = hit.transform.GetComponent<CharacterStats>();
                charStats.TakeDamage(weapon.damage);
                Debug.Log(hit.transform.name + " took " + weapon.damage);
            }

            //GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impact, 0.2f);
        }

    }
}

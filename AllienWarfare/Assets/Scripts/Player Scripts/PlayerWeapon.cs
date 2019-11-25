
using UnityEngine;

[System.Serializable]
public class PlayerWeapon : MonoBehaviour
{
    public Animator anim;
    public string name = "Aniquilator300R";

    public float damage = 10f;
    public float range = 300f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Fire", false);
        }
    }

    void Shoot() 
    {
        anim.SetBool("Fire", true);
    }
}

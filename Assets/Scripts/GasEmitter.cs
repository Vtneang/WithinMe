using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasEmitter : MonoBehaviour
{
    [SerializeField]
    public Gas projectile;
    public float rateOfFire = 2f;
    public float projectileSpeed = 3f;

    // Calls ReleaseGas() every rateOfFire.
    void Start()
    {
        InvokeRepeating("ReleaseGas", 0, rateOfFire);
    }

    // Instantiates projectile with velocity of projectileSpeed.
    private void ReleaseGas()
    {
        Gas projectileCopy = Instantiate(projectile, gameObject.transform);
        Rigidbody2D gasBody2D = projectileCopy.GetComponent<Rigidbody2D>();
        gasBody2D.velocity = new Vector2(projectileSpeed, 0);
    }
}

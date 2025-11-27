using UnityEngine;

public class StandGunFiring : MonoBehaviour
{
    int standGunbulletDamage = 100;
    Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    
    void OnParticleCollision(GameObject gb)
    {
        if(gb.CompareTag("StandGun"))
        {
            //enemy.TakeDamage(standGunbulletDamage);
        }
    }
}

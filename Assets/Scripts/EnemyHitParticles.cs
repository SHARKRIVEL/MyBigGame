using UnityEngine;
using System.Collections.Generic;

public class EnemyHitParticles : MonoBehaviour
{
    [SerializeField] HitEffects hitEffectsObject;
    [SerializeField] AudioSource weaponAudioSource;

    int standGunbulletDamage = 50;

    ParticleSystem standGunPs;

    public float volume;

    [SerializeField] float hitForce; 

    void Start()
    {
        standGunPs = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject gb)
    {
        if (gb.CompareTag("Enemy"))
        {
            HitAudio(hitEffectsObject.enemySound);
            HitEffect(gb,hitEffectsObject.enemyEffect);

            Enemy enemy = gb.GetComponentInParent<Enemy>();
            if(enemy)
            {
                enemy.TakeDamage(standGunbulletDamage,-(gb.transform.position - transform.position),hitForce);
            }
        }
        else if(gb.CompareTag("TileGround"))
        {
            HitAudio(hitEffectsObject.groundSound);
            HitEffect(gb,hitEffectsObject.groundEffect);
        }
        else if(gb.CompareTag("Metal"))
        {
            HitAudio(hitEffectsObject.metalSound);
            HitEffect(gb,hitEffectsObject.metalEffect);
        }
        else if(gb.CompareTag("Wood"))
        {
            HitAudio(hitEffectsObject.woodSound);
            HitEffect(gb,hitEffectsObject.woodEffect);
        }
        else if(gb.CompareTag("EnemySpawner"))
        {   
            HitAudio(hitEffectsObject.metalSound);
            HitEffect(gb,hitEffectsObject.metalEffect);

            gb.GetComponent<EnemySpawner>().DamageTaker(standGunbulletDamage);
        }
        else if(gb.CompareTag("ElectricShield"))
        {
            HitAudio(hitEffectsObject.electricShieldSound);
            HitEffect(gb,hitEffectsObject.electricShieldEffect);
        }
    }

    void HitAudio(AudioClip ac)
    {
        weaponAudioSource.PlayOneShot(ac, volume);
    }

    void HitEffect(GameObject gb , ParticleSystem ps)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int collisionEventsCount = standGunPs.GetCollisionEvents(gb,collisionEvents);
        for(int i = 0;i<collisionEventsCount;i++)
        {
            Vector3 contactPoint = collisionEvents[i].intersection;
            Vector3 normal = collisionEvents[i].normal;
            Instantiate(ps,contactPoint,Quaternion.LookRotation(normal));
        }
    }
}

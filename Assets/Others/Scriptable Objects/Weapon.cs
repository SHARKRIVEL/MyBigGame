using UnityEngine;

[CreateAssetMenu(fileName = "Weapon" , menuName = "Scriptable Object/Weapon")]
public class Weapon : ScriptableObject
{
    public float fireRate;
    public float reloadTime;
    public AudioClip reloadSound;
    public int weaponZoom;
    public int magSize;
    public int maxAmmo;
    public int weaponDamage;
    public float impulseVal;
    public AudioClip weaponSound;
    public float bulletHitForce;
}

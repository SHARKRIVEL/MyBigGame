using UnityEngine;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] Rigidbody[] rbs;
    [SerializeField] Animator anim;

    void Start()
    {  
        StartCoroutine(Rebirth());
    }

    IEnumerator Rebirth()
    {
        while(true)
        {
            anim.enabled = true;
            yield return new WaitForSeconds(5f);


            anim.enabled = false;
            foreach(Rigidbody rb in rbs)
            {
                rb.AddForce(Vector3.forward*Random.Range(10,15),ForceMode.Impulse);
            }

            yield return new WaitForSeconds(1.5f); 
        }

        yield return null; 
    }
}

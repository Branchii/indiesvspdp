using UnityEngine;
using System.Collections;

public class HazardAnimation : MonoBehaviour
{
    Animator hazardAnim;

    void Awake()
    {
        hazardAnim = gameObject.GetComponent<Animator>();
    }

    public void Activate()
    {
        Destroy(transform.parent.collider2D);
        hazardAnim.SetBool("destroy", true);
    }

    public void Hit()
    {
        Activate();
    }

    void DestroyHazard()
    {
        Debug.Log("dabble");
        Destroy(transform.parent.gameObject);
    }

    void Idleanim()
    {
        hazardAnim.SetBool("idle", true);
    }

    void ContinueAnim()
    {
        hazardAnim.SetBool("idle", false);
    }
}

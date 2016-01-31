using UnityEngine;
using System.Collections;

public class WitchHit : MonoBehaviour {

    private bool hittable = false;

    public KeyCode hitKey = KeyCode.V;
    private GameObject otherWitch;
    public float force = 30;
    private GameObject stunObject;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(hitKey) && hittable == true)
        {
            stunObject = otherWitch.transform.FindChild("StunAnimObject").gameObject;
            Rigidbody2D witchRigidbody = otherWitch.GetComponent<Rigidbody2D>();
            Vector2 direction = otherWitch.transform.position - transform.parent.transform.position;
            direction.Normalize();
            direction.x = direction.x * force;
            direction.y = direction.y * force;

            witchRigidbody.AddForce(direction);

            var witchScript = otherWitch.GetComponent<WitchController>();
            var stunRender = stunObject.GetComponent<SpriteRenderer>();

            witchScript.isStunned = true;
            stunRender.enabled = true;


            StartCoroutine(
                DelayAction(
                    ()=> {
                        witchScript.isStunned = false;
                        stunRender.enabled = false;
                    },
                    5)
            );
        }
    }

    delegate void Action();

    IEnumerator DelayAction(Action act, float delay)
    {
        yield return new WaitForSeconds(delay);
        act.Invoke();
    }

    void UpdateDirection()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            hittable = true;
            otherWitch = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            hittable = false;
        }
    }
}

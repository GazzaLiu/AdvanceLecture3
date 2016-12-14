using UnityEngine;
using System.Collections;

public class UnityChan : MonoBehaviour {
    public GameObject flame;
    public GameObject bigFlame;
    public GameObject meteor;
    protected Animator animator;
    protected ParticleSystem ps;
    protected ParticleSystem ps2;
    protected Rigidbody rb;

    protected bool isMove = true;
    protected float speed = 10f;
    protected float hAxis = 0f;
    protected float vAxis = 0f;
    protected float walkSpeed;
    protected float turnSpeed;

    protected KeyCode keyJump;
    protected KeyCode keyFlame;
    protected KeyCode keyPunch;
    protected KeyCode keyExplode;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        animator.SetLayerWeight(1, 1);
        ps = flame.GetComponent<ParticleSystem>();
        ps2 = bigFlame.GetComponent<ParticleSystem>();

        walkSpeed = 5.00f;
        turnSpeed = 2.25f;

        keyJump = KeyCode.Z;
        keyFlame = KeyCode.X;
        keyPunch = KeyCode.C;
        keyExplode = KeyCode.V;
    }

    protected virtual void getAxis()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    protected void Update()
    {
        getAxis();

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(1);
        if (state.IsName("Arm Layer.Wave") || state.IsName("Arm Layer.Attack") || state.IsName("Arm Layer.Bomb"))
            isMove = false;
        else
            isMove = true;


        if (vAxis!=0 && isMove)
        {
            rb.angularVelocity = new Vector3(0, 0, 0);
            rb.velocity = (transform.forward * vAxis + transform.right * hAxis*0.2f)*walkSpeed;
            animator.SetBool("isRun", true);
        }
        if (hAxis!=0 && isMove)
        {
            rb.angularVelocity = new Vector3(0, hAxis*turnSpeed, 0);
            animator.SetBool("isRun", true);
        }
        if (Mathf.Abs(hAxis) < 0.2f && Mathf.Abs(vAxis) < 0.2f)
            animator.SetBool("isRun", false);

        if (Input.GetKeyDown(keyJump))
            animator.SetBool("isJump", true);
        else
            animator.SetBool("isJump", false);

        if (Input.GetKey(keyFlame))
            animator.SetBool("isWave", true);
        else
        {
            animator.SetBool("isWave", false);
            ps.Stop();
        }
        if (Input.GetKeyDown(keyFlame))
        {

            ParticleSystem flamePs = (ParticleSystem)Instantiate(ps);
            flamePs.transform.position = this.transform.position + new Vector3(0, 1.221f, 0);
            flamePs.transform.rotation = this.transform.rotation;
            flamePs.Play();
            Destroy(flamePs.gameObject, 3f);
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, 0.5f, transform.forward, Mathf.Infinity, LayerMask.GetMask("Enemy", "UnityChan"));
            foreach (RaycastHit hit in hits)
            {
                GameObject other = hit.collider.gameObject;
                if (other == gameObject)
                    continue;
                Vector3 dir = other.transform.position - transform.position;
                dir.y += 1f;
                float dis = dir.magnitude;
                dir.Normalize();
                other.GetComponent<Rigidbody>().AddForce(50f * dir, ForceMode.VelocityChange);
                if (other.CompareTag("Explode")) 
                    StartCoroutine(other.GetComponent<ExplodeCube>().explode());
            }
        }

        if (Input.GetKeyDown(keyPunch))
        {
            animator.SetBool("isAttack", true);
            meteor.GetComponent<Rigidbody>().AddForce(transform.forward*50+transform.up*15f , ForceMode.VelocityChange);
        }
        else
        {
            animator.SetBool("isAttack", false);
        }

        if (Input.GetKeyDown(keyExplode))
        {
            ParticleSystem flamePs = (ParticleSystem)Instantiate(ps2);
            flamePs.transform.position = this.transform.position;
            flamePs.Play();


            animator.SetBool("isBomb", true);
            Collider[] colliders = Physics.OverlapSphere(transform.position, 6f, LayerMask.GetMask("Enemy", "UnityChan"));
            foreach (Collider collider in colliders)
            {
                GameObject other = collider.gameObject;
                if (other == gameObject) 
                    continue;
                Vector3 dir = other.transform.position - transform.position;
                dir.y += 1f;
                float dis = dir.magnitude;
                dis = Random.Range(dis * 0.9f, dis * 1.1f);
                dir.Normalize();
                other.GetComponent<Rigidbody>().AddForce(120f * dir / dis, ForceMode.VelocityChange);
                if(other.CompareTag("Explode"))
                    StartCoroutine(other.GetComponent<ExplodeCube>().explode());
            }


        }
        else
            animator.SetBool("isBomb", false);
    }
}

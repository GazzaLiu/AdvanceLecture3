using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public GameObject flame;

    Animator animator;
    CharacterController cc;
    ParticleSystem ps;

    bool isMove = true;
    float speed = 0.1f;
    float horizontalSpeed = 0;
    float verticalSpeed = 0;

    void Start () {
        animator = this.GetComponent<Animator>();
        cc = this.GetComponent<CharacterController>();
        ps = flame.GetComponent<ParticleSystem>();
        animator.SetLayerWeight(1, 1);
    }

    void Update () {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(1);

        if(state.IsName("Arm Layer.Wave") || state.IsName("Arm Layer.Attack") || state.IsName("Arm Layer.Bomb"))
            isMove = false;
        else
            isMove = true;

        horizontalSpeed = Input.GetAxis("Horizontal") * speed;
        verticalSpeed = Input.GetAxis("Vertical") * speed;

        if (Input.GetKey(KeyCode.UpArrow) && isMove) {
            transform.eulerAngles = new Vector3(0, 0, 0);
            cc.Move(new Vector3(horizontalSpeed, 0, verticalSpeed));
            animator.SetBool("isRun", true);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && isMove) {
            transform.eulerAngles = new Vector3(0, 180, 0);
            cc.Move(new Vector3(horizontalSpeed, 0, verticalSpeed));
            animator.SetBool("isRun", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && isMove) {
            transform.eulerAngles = new Vector3(0, 270, 0);
            cc.Move(new Vector3(horizontalSpeed, 0, verticalSpeed));
            animator.SetBool("isRun", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && isMove) {
            transform.eulerAngles = new Vector3(0, 90, 0);
            cc.Move(new Vector3(horizontalSpeed, 0, verticalSpeed));
            animator.SetBool("isRun", true);
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) != 1 || Mathf.Abs(Input.GetAxis("Vertical")) != 1)
            animator.SetBool("isRun", false);

        //animator.SetFloat("direction", Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Z))
            animator.SetBool("isJump", true);
        else
            animator.SetBool("isJump", false);

        if (Input.GetKey(KeyCode.X))
            animator.SetBool("isWave", true);
        else {
            animator.SetBool("isWave", false);
            ps.Stop();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {

            flame.transform.position = this.transform.position + new Vector3(0, 1.221f, 0);
            flame.transform.rotation = this.transform.rotation;
            ps.Play();

            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward);
            foreach (RaycastHit hit in hits) {
                GameObject other = hit.collider.gameObject;
                if (other.CompareTag("Explode")) {
                    Vector3 dir = other.transform.position - transform.position;
                    dir.y += 6f;
                    float dis = dir.magnitude;
                    dir.Normalize();
                    other.GetComponent<Rigidbody>().AddForce(50f*dir, ForceMode.VelocityChange);
                    StartCoroutine(other.GetComponent<ExplodeCube>().explode());
                }
            }


        }

        if (Input.GetKeyDown(KeyCode.C))
            animator.SetBool("isAttack", true);
        else
            animator.SetBool("isAttack", false);

        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetBool("isBomb", true);
            Collider[] colliders = Physics.OverlapSphere(transform.position, 6f);
            foreach (Collider collider in colliders)
            {
                GameObject other = collider.gameObject;
                if (other.CompareTag("Explode"))
                {
                    Vector3 dir = other.transform.position - transform.position;
                    dir.y += 3f;
                    float dis = dir.magnitude;
                    dis = Random.Range(dis * 0.9f, dis * 1.1f);
                    dir.Normalize();
                    other.GetComponent<Rigidbody>().AddForce(120f * dir/dis, ForceMode.VelocityChange);
                    StartCoroutine(other.GetComponent<ExplodeCube>().explode());
                }
            }


        }
        else
            animator.SetBool("isBomb", false);
    }
}

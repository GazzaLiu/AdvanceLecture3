using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    Animator animator;
    CharacterController cc;
    public GameObject flame;
    ParticleSystem ps;

    float speed = 0.1f;
    float horizontalSpeed = 0;
    float verticalSpeed = 0;
    Vector3 moveDirection = Vector3.zero;

    void Start () {
        animator = this.GetComponent<Animator>();
        cc = this.GetComponent<CharacterController>();
        ps = flame.GetComponent<ParticleSystem>();
        animator.SetLayerWeight(1, 1);
    }


    void Update () {
        transform.eulerAngles = new Vector3(0, 0, 0);

        horizontalSpeed = Input.GetAxis("Horizontal") * speed;
        verticalSpeed = Input.GetAxis("Vertical") * speed;

        cc.Move(new Vector3(horizontalSpeed, 0, verticalSpeed));

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            animator.SetBool("isRun", true);
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) != 1 || Mathf.Abs(Input.GetAxis("Vertical")) != 1)
            animator.SetBool("isRun", false);

        animator.SetFloat("direction", Input.GetAxis("Horizontal"));

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
            ps.Play();

        if (Input.GetKeyDown(KeyCode.C))
            animator.SetBool("isAttack", true);
        else
            animator.SetBool("isAttack", false);

        if (Input.GetKeyDown(KeyCode.V))
            animator.SetBool("isBomb", true);
        else
            animator.SetBool("isBomb", false);
    }
}

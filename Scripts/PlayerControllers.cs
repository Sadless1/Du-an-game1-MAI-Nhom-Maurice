using UnityEngine;

public class PlayerControllers : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator animator;

    private float inputH;
    private float inputV;
    private Vector2 moveInput;
    private Vector2 huongCuoi;

    public float speedMove = 5f;
    private bool isAttacking = false;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            inputH = Input.GetAxisRaw("Horizontal");
            inputV = Input.GetAxisRaw("Vertical");

            moveInput = new Vector2(inputH, inputV).normalized;

            // ✅ Ưu tiên ngang trước – giống Zelda NES
            if (inputH != 0)
                huongCuoi = new Vector2(inputH, 0);
            else if (inputV != 0)
                huongCuoi = new Vector2(0, inputV);

            // ✅ Cập nhật Animator
            animator.SetFloat("Move X", inputH);
            animator.SetFloat("Move Y", inputV);
            animator.SetBool("Move", moveInput.magnitude > 0);

            // ✅ Update hướng đứng hiện tại
            if (huongCuoi.y > 0) animator.SetFloat("direction", 2);
            else if (huongCuoi.y < 0) animator.SetFloat("direction", 0);
            else if (huongCuoi.x < 0) animator.SetFloat("direction", 1);
            else if (huongCuoi.x > 0) animator.SetFloat("direction", 3);

            // ✅ Bấm X để tấn công
            if (Input.GetKeyDown(KeyCode.X))
            {
                isAttacking = true;
                animator.SetTrigger("Attack");

                // Không dùng hàm Fix, tự gán -1/0/1
                float atkX = Mathf.RoundToInt(huongCuoi.x);
                float atkY = Mathf.RoundToInt(huongCuoi.y);

                animator.SetFloat("Attack X", atkX);
                animator.SetFloat("Attack Y", atkY);

                Debug.Log("Attack X = " + atkX + ", Attack Y = " + atkY);

                Invoke(nameof(EndAttack), 0.4f);
            }
        }
    }

    void FixedUpdate()
    {
        if (!isAttacking)
            rig.linearVelocity = moveInput * speedMove;
        else
            rig.linearVelocity = Vector2.zero;
    }

    void EndAttack()
    {
        isAttacking = false;
        animator.ResetTrigger("Attack");
        animator.Play("Blend Idle");
    }
}

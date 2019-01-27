using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public static Character instance;

    public Animator animator;
    new public Rigidbody2D rigidbody;
    public HingeJoint2D footHingeJoint, bowHingeJoint;
    public GroundCheck groundCheck;
    public Vector2 jumpLength;
    public float walkSpeed = 300;
    public float bowSpeed = 2000;
    public bool playerInput = true;
    public bool ai;
    public float aiHorizontal;
    public GameObject hat, bow, pole, rock, fly2, wing1, wing2;
    public float flyMaxSpeed = 2;
    public float flyAddSpeed = 1;
    public float maximumAeriallyMovementSpeed = 2; // 空中水平移動最大速度
    public float aeriallyMovementAcceleration = 1; // 空中水平移動加速度
    [Range(0, 1)] public float size = 1;
    public Transform mainBone, foot, triggerCheckTran;
    public bool isPlayer;

    List<LevelTrigger> m_TouchTrigger = new List<LevelTrigger>();
    float startPosX;

    void Awake()
    {
        if (isPlayer)
            instance = this;
        startPosX = transform.position.x;
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        bool fly = Input.GetAxisRaw("Vertical") > 0;
        if (ai)
        {
            horizontal = aiHorizontal;
            fly = false;
        }

        animator.SetInteger("Horizontal", Mathf.RoundToInt(horizontal));
        animator.SetBool("Fly", fly);
        animator.SetBool("Take Pole", pole.activeSelf);
        animator.SetBool("Grounded", groundCheck.isGrounded);

        AnimatorStateInfo currentState = animator.GetNextAnimatorStateInfo(0);
        if (currentState.fullPathHash == 0)
            currentState = animator.GetCurrentAnimatorStateInfo(0);

        float moveSpeed = 0;
        float bowMoveSpeed = 0;

        if (currentState.IsName("Walk"))
        {
            moveSpeed = horizontal;
            if (horizontal != 0)
                transform.localEulerAngles = new Vector3(0, horizontal > 0 ? 0 : 180, 0);
        }
        else if (currentState.IsName("Walk_Rain"))
        {
            if (hat.activeSelf)
                moveSpeed = horizontal * 0.5f;
            else
                moveSpeed = horizontal * 0.1f;
            if (horizontal != 0)
                transform.localEulerAngles = new Vector3(0, horizontal > 0 ? 0 : 180, 0);
        }
        else if (currentState.IsName("Walk_Pole"))
        {
            bowMoveSpeed = horizontal;
            if (horizontal != 0)
                transform.localEulerAngles = new Vector3(0, horizontal > 0 ? 0 : 180, 0);
        }
        else if (currentState.IsName("Fly"))
        {
            if (horizontal != 0)
            {
                transform.localEulerAngles = new Vector3(0, horizontal > 0 ? 0 : 180, 0);

                if (transform.localEulerAngles.y == 0 ? rigidbody.velocity.x < maximumAeriallyMovementSpeed : rigidbody.velocity.x > -maximumAeriallyMovementSpeed)
                    rigidbody.velocity += new Vector2(aeriallyMovementAcceleration * Time.deltaTime * (transform.localEulerAngles.y == 0 ? 1 : -1), 0);
            }

            if (wing1.activeSelf && wing2.activeSelf && rigidbody.velocity.y < flyMaxSpeed)
                rigidbody.velocity += new Vector2(0, flyAddSpeed * Time.deltaTime);
        }
        else if (currentState.IsName("Falling"))
        {
            if (horizontal != 0)
            {
                transform.localEulerAngles = new Vector3(0, horizontal > 0 ? 0 : 180, 0);

                if (transform.localEulerAngles.y == 0 ? rigidbody.velocity.x < maximumAeriallyMovementSpeed : rigidbody.velocity.x > -maximumAeriallyMovementSpeed)
                    rigidbody.velocity += new Vector2(aeriallyMovementAcceleration * Time.deltaTime * (transform.localEulerAngles.y == 0 ? 1 : -1), 0);
            }
        }

        JointMotor2D motor = footHingeJoint.motor;
        motor.motorSpeed = moveSpeed * walkSpeed;
        footHingeJoint.motor = motor;

        JointMotor2D motor_bow = bowHingeJoint.motor;
        motor_bow.motorSpeed = bowMoveSpeed * bowSpeed;
        bowHingeJoint.motor = motor_bow;

        size = Mathf.Clamp((transform.position.x - startPosX) / 120, 0.7f, 1);
        mainBone.localScale = new Vector3(size, size, 1);
        foot.localPosition = new Vector3(0, 0.5f + (1 - size), 0);
        groundCheck.transform.localPosition = new Vector3(0, 0.372f + (1 - size), 0);
        triggerCheckTran.transform.localPosition = new Vector3(0, 0.372f + (1 - size), 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        LevelTrigger levelTrigger = other.GetComponent<LevelTrigger>();
        if (levelTrigger)
        {
            if (levelTrigger.triggerType == LevelTrigger.TriggerType.EatFly)
            {
                fly2.gameObject.SetActive(true);
                other.gameObject.SetActive(false);
            }
            else
            {
                m_TouchTrigger.Add(levelTrigger);
                OnUpdateTriggers();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        LevelTrigger levelTrigger = other.GetComponent<LevelTrigger>();
        if (levelTrigger)
        {
            m_TouchTrigger.Remove(levelTrigger);
            OnUpdateTriggers();
        }
    }

    void OnUpdateTriggers()
    {
        bool touchingRain = false;
        foreach (LevelTrigger t in m_TouchTrigger)
        {
            if (t.triggerType == LevelTrigger.TriggerType.Rain)
                touchingRain = true;
        }
        animator.SetBool("Touching Rain", touchingRain);
    }
}

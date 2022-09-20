using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region Movement_variables
    [SerializeField]
    private float moveSpeed;
    Rigidbody2D PlayerRB;
    float xAxis;
    float yAxis;
    #endregion

    #region Sprint_variables
    [SerializeField]
    [Tooltip("Indicates the speed of sprint")]
    private float sprintSpeed;
    float walkSpeed;

    [SerializeField]
    [Tooltip("Indicates sprint timers and rates")]
    private float sprintCooldown, rechargeDelay, rechargeRate;
    float sprintTimer;
    bool recharging;
    bool canSprint;
    public Slider sprintSlider;
    #endregion

    #region Attack_variables
    [SerializeField]
    [Tooltip("Indicates attack cooldown and hitbox spawn delay")]
    private float attackSpeed, hitboxTiming;
    [SerializeField]
    [Tooltip("Indicates minimum and max distance to send ghost relative to the player")]
    private float minDistance, maxDistance;
    float attackTimer;
    bool isAttacking;
    Vector2 currDirection;
    public Slider attackSlider;
    #endregion

    #region Interact_variables
    [Tooltip("Indicates how many keys are currently being held")]
    public int keyCount;
    public GameObject keyTextObject;
    private TextMeshProUGUI keyText;
    #endregion

    #region Animation_variables
    Animator anim;
    #endregion

    #region Unity_functions
    private void Awake()
    {
        PlayerRB = GetComponent<Rigidbody2D>();

        //Sprint variable setup
        walkSpeed = moveSpeed;
        sprintTimer = sprintCooldown;
        recharging = false;
        canSprint = true;
        //Slide intitialize
        sprintSlider.value = sprintTimer / sprintCooldown;

        //Attack variable setup
        attackTimer = 0;
        //Slider initialize
        attackSlider.value = 1 - (attackTimer / attackSpeed);
        //Key initialize
        keyText = keyTextObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            return;
        }

        //Movement
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        Move();

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            recharging = false;
            Sprint();
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
            recharging = true;
        }
        if (recharging)
        {
            StartCoroutine(SprintRechargeRoutine());
        }
        //Update sprint UI
        sprintSlider.value = sprintTimer / sprintCooldown;

        //Attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackTimer <= 0)
        {
            Attack();
        } else
        {
            attackTimer -= Time.deltaTime;
        }
        //Update attack UI
        attackSlider.value = 1 - (attackTimer / attackSpeed);

        //Interact
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Interact();
        }
    }
    #endregion

    #region Movement_functions
    private void Move()
    {
        //Set speed and store direction facing
        Vector2 movementVector = new Vector2(xAxis, yAxis);
        PlayerRB.velocity = movementVector * moveSpeed;

        currDirection = movementVector;
    }
    #endregion

    #region Sprint_functions
    //Sprint when there's enough stamina 
    private void Sprint()
    {
        if (sprintTimer > 0)
        {
            moveSpeed = sprintSpeed;
            sprintTimer -= Time.deltaTime;
        } else
        {
            moveSpeed = walkSpeed;
            recharging = true;
        }
    }

    IEnumerator SprintRechargeRoutine()
    {
        canSprint = false;

        yield return new WaitForSeconds(rechargeDelay);
        canSprint = true;

        sprintTimer += Time.deltaTime * rechargeRate;
        if (sprintTimer >= sprintCooldown)
        {
            recharging = false;
            sprintTimer = sprintCooldown;
        }
    }
    #endregion

    #region Attack_functions
    //How the attack works is that it is on a long cooldown and if it hits the ghost it teleports it away to a random distance
    private void Attack()
    {
        attackTimer = attackSpeed;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        PlayerRB.velocity = Vector2.zero;

        yield return new WaitForSeconds(hitboxTiming);

        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, Vector2.one, 0f, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Ghost"))
            {
                hit.transform.position = Random.insideUnitCircle.normalized * Random.Range(minDistance, maxDistance); 
            }
        }

        yield return new WaitForSeconds(hitboxTiming);
        isAttacking = false;

        yield return null;
    }
    #endregion

    #region Interact_functions
    private void Interact()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, Vector2.one, 0f, Vector2.zero, 0f);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Door"))
            {
                hit.transform.GetComponent<Door>().Interact(this.gameObject);
                //update key text
                keyText.text = "Keys: " + keyCount.ToString();
            }
            else if (hit.transform.CompareTag("Key"))
            {
                hit.transform.GetComponent<Key>().Interact(this.gameObject);
                //update key text
                keyText.text = "Keys: " + keyCount.ToString();
            }
        }
    }
    #endregion
}

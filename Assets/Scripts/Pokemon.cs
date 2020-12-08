using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Linq;
public class Pokemon : MonoBehaviour
{
    public const int delayAttackValue = 200;

    // Start is called before the first frame update
    private AnimationClip moveAnim;
    private AnimationClip attackAnim;
    public string pokeName;
    public float maxHp = 500;
    private float currentHp;
    private float maxMana = 500;
    private float currentMana;
    public int attack = 40;
    public int defense;
    private int specialAttack;
    private int specialDefense;
    private int speed = 250;
    private float range = 6;

    private GameObject currentTarget;
    private Animator m_Animator;
    private SpriteRenderer spriteRenderer;
    private AnimatorOverrideController animatorOverrideController;
    private Rigidbody2D m_Rigidbody;
    private PhotonView photonView;
    private bool isAttacking = false;
    private int reloadAttack = delayAttackValue;
    private bool isInit = false;
    public GameObject healthBar;
    public GameObject manaBar;
    private Image healthBarImage;
    private Image manaBarImage;



    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        healthBarImage = healthBar.GetComponent<Image>();
        manaBarImage = manaBar.GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        photonView = GetComponent<PhotonView>();
        currentHp = maxHp;
        currentMana = 0;
        Initialize("Bulbasaur");
    }

    public void Initialize(string name)
    {
        this.name = name;
        moveAnim = Resources.Load<AnimationClip>("Animation/" + name + "/" + name.ToLower() + "_move");
        attackAnim = Resources.Load<AnimationClip>("Animation/" + name + "/" + name.ToLower() + "_attack");

        animatorOverrideController = new AnimatorOverrideController(m_Animator.runtimeAnimatorController);
        SetMotion("move", moveAnim);
        SetMotion("attack", attackAnim);
        isInit = true;
    }

    private void SetMotion(string stateName, AnimationClip anim)
    {

        animatorOverrideController[stateName] = anim;
    }

    void Update()
    {
        if (!isInit)
            return;

        if (currentTarget != null)
        {
            if (currentTarget.transform.position.x > transform.position.x)
            {
                if (!spriteRenderer.flipX)
                    spriteRenderer.flipX = true;
            }
        }

        healthBarImage.fillAmount = currentHp / maxHp;
        manaBarImage.fillAmount = currentMana / maxMana;

        if (!photonView.IsMine)
            return;

        photonView.RPC("FindTarget", RpcTarget.All);
        if (currentTarget != null)
        {

            Vector2 direct = currentTarget.transform.position - transform.position;


            if (direct.magnitude < range)
            {
                if (reloadAttack >= delayAttackValue)
                {
                    if (!isAttacking)
                    {
                        photonView.RPC("PlayAttackMotion", RpcTarget.All);
                    }
                }
            }
            else
            {
                transform.Translate(direct.normalized * speed * Time.deltaTime / 10);
            }
        }

        if (reloadAttack < delayAttackValue)
            reloadAttack += (int)(speed * Time.deltaTime);

    }
    [PunRPC]
    public void PlayAttackMotion()
    {
        m_Animator.Play("Base Layer.attack", 0, 0f);
        isAttacking = true;
        reloadAttack = 0;
    }

    [PunRPC]
    public void FindTarget()
    {
        if (currentTarget == null)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Pokemon");
            foreach (GameObject target in targets)
            {
                if (!target.Equals(gameObject))
                {
                    currentTarget = target;
                    break;
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHp < damage)
            Kill();
        else
        {
            currentHp -= damage;
            currentMana = Mathf.Min(currentMana + damage * 2, maxMana);
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void ExecuteAttack()
    {
        isAttacking = false;
        currentTarget.GetComponent<Pokemon>().TakeDamage(attack);
    }
}


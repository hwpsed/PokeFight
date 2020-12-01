using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.UI;
using System.Linq;
public class Pokemon : MonoBehaviour
{
    public const int delayAttackValue = 300;

    // Start is called before the first frame update
    private AnimationClip moveAnim;
    private AnimationClip attackAnim;
    private float maxHp = 500;
    private float currentHp;
    private float maxMana = 500;
    private float currentMana;
    private int attack = 40;
    private int defense;
    private int specialAttack;
    private int specialDefense;
    private int speed = 150;
    private float range = 6;

    private Vector2 position;
    private GameObject currentTarget;
    private Animator m_Animator;
    private AnimatorController controller;
    private Rigidbody2D m_Rigidbody;
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
        currentHp = maxHp;
        currentMana = 0;
        Initialize("Bulbasaur");
    }

    public void Initialize(string name)
    {
        this.name = name;
        moveAnim = Resources.Load<AnimationClip>("Animation/" + name + "/" + name.ToLower() + "_move");
        attackAnim = Resources.Load<AnimationClip>("Animation/" + name + "/" + name.ToLower() + "_attack");
        Debug.Log(moveAnim);

        controller = (AnimatorController)m_Animator.runtimeAnimatorController;
        SetMotion("move",  moveAnim);
        SetMotion("attack", attackAnim);
        isInit = true;
    }

    private void SetMotion(string stateName, AnimationClip anim)
    {
        
        var state = controller.layers[0].stateMachine.states.FirstOrDefault(s => s.state.name.Equals(stateName)).state;

        if (state == null)
        {
            Debug.LogError("Couldn't get the state!");
            return;
        }
        else
        {
            controller.SetStateEffectiveMotion(state, anim);
        }
    }

    void Update()
    {
        if (!isInit)
            return;

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Pokemon");
        foreach (GameObject target in targets)
        {
            if (!target.Equals(gameObject))
            {
                currentTarget = target;
                break;
            }
        }

        Vector2 direct = currentTarget.transform.position - transform.position;
        

        if (direct.magnitude < range)
        {
            if (reloadAttack >= delayAttackValue)
            {
                if (!isAttacking)
                {
                    m_Animator.Play("Base Layer.attack", 0, 0f);
                    isAttacking = true;
                    reloadAttack = 0;
                }
            }
        }
        else
        {
            transform.Translate(direct.normalized * speed * Time.deltaTime / 10);
        }

        if (reloadAttack < delayAttackValue)
            reloadAttack += (int)(speed * Time.deltaTime);

        healthBarImage.fillAmount = currentHp / maxHp;
        manaBarImage.fillAmount = currentMana / maxMana;
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

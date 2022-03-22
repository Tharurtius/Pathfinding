using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : BaseManager
{
    [SerializeField] protected Animator _animator;
    protected PlayerManager _playerManager;
    protected override void Start()
    {
        base.Start();

        _playerManager = GetComponent<PlayerManager>();
        if (_playerManager == null)
        {
            Debug.LogError("PlayerManager not found");
        }
    }
    public enum State
    {
        FullHP,
        LowHP,
        Dead,
    }
    public State currentState;
    public override void TakeTurn()
    {
        if (_health == 0)
        {
            currentState = State.Dead;
        }
        switch (currentState)
        {
            case State.FullHP:
                FullHPState();
                StartCoroutine(EndTurn());
                break;
            case State.LowHP:
                LowHPState();
                StartCoroutine(EndTurn());
                break;
            case State.Dead:
                DeadState();
                break;
            //default:
                //Debug.Log("Error!");
        }
    }
    IEnumerator EndTurn()
    {
        yield return new WaitForSecondsRealtime(1f);
        _playerManager.TakeTurn();
    }
    void FullHPState()
    {
        if (_health < 40f)
        {
            currentState = State.LowHP;
            LowHPState();
            return;
        }
        int randomAttack = Random.Range(0, 10);

        switch (randomAttack)
        {
            case int i when i > 0 && i <= 2:
                FlameWheel();
                break;
            case int i when i > 2 && i <= 8:
                VineWhip();
                break;
            case 9:
                SelfDestruct();
                break;
        }
    }
    void LowHPState()
    {
        int randomAttack = Random.Range(0, 10);

        switch (randomAttack)
        {
            case int i when i > 0 && i <= 2:
                SelfDestruct();
                break;
            case int i when i > 2 && i <= 8:
                EatBerries();
                break;
            case 9:
                FlameWheel();
                break;
        }
        if (_health > 60f)
        {
            currentState = State.FullHP;
        }
    }
    void DeadState()
    {
        _animator.SetTrigger("IsDead");
        Debug.Log("You win!");
    }
    public void EatBerries()
    {
        Debug.Log("Eat Berries!");
        _animator.SetTrigger("EatBerries");
        Heal(20f);
    }
    public void SelfDestruct()
    {
        Debug.Log("Self Destruct!");
        _animator.SetTrigger("SelfDestruct");
        DealDamage(_maxHealth);
        _playerManager.DealDamage(80f);
        DeadState();
    }
    public void VineWhip()
    {
        Debug.Log("Vine Whip!");
        _animator.SetTrigger("VineWhip");
        _playerManager.DealDamage(30f);
    }
    public void FlameWheel()
    {
        Debug.Log("Flame Wheel!");
        _animator.SetTrigger("FlameWheel");
        _playerManager.DealDamage(50f);
    }
    public IEnumerator Thinking()
    {
        yield return new WaitForSecondsRealtime(1f);
        TakeTurn();
    }
}

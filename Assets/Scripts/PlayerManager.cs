using UnityEngine;

public class PlayerManager : BaseManager
{
    protected AIManager _aiManager;
    [SerializeField] Animator _animator;
    [SerializeField] protected CanvasGroup _buttonGroup;
    protected override void Start()
    {
        base.Start();
        _aiManager = GetComponent<AIManager>();
        if (_aiManager == null)
        {
            Debug.LogError("AIManager not found");
        }
    }
    public override void TakeTurn()
    {
        if (_health <= 0)
        {
            Dead();
            return;
        }
        _buttonGroup.interactable = true;
    }
    public void EndTurn()
    {
        _buttonGroup.interactable = false;
        StartCoroutine(_aiManager.Thinking());
    }
    public void EatBerries()
    {
        Heal(20f);
        EndTurn();
    }
    public void SelfDestruct()
    {
        DealDamage(_maxHealth);
        _aiManager.DealDamage(80f);
        Dead();
    }
    public void VineWhip()
    {
        _aiManager.DealDamage(30f);
        EndTurn();
    }
    public void FlameWheel()
    {
        _aiManager.DealDamage(50f);
        EndTurn();
    }
    private void Dead()
    {
        _animator.SetTrigger("IsDead");
        Debug.Log("Game Over!");
    }
}
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{

    // The actual stats relating to attacking. This can be saved/loaded etc.
    private AttackData data;

    // For inspector
    public int StartingBaseAttack;

    // The regions are for public read/write of data
    #region properties
    public int BonusAttack{
        get {return data.BonusAttack;}
        set {data.BonusAttack = value;}
    }

    #endregion

    public int Attack
    {
        get { return CalculateAttack(); }
    }


    void Start()
    {
        data.BaseAttack = StartingBaseAttack;
    }

    public void DoAttack(Actor target)
    {
        int damage = Attack;

        target.damagable.TakeDamage(damage);

        //if target.tag != player: EventManager.Broadcast("EnemyTookDamage")
    }

    public bool CanAttack(int x, int y)
    {
        return true;
    }

    public int CalculateAttack()
    {
        return data.BaseAttack + data.BonusAttack;
    }

    // Return the data object used. Should only be used by serialization classes
    public AttackData _GetRawData(){
        return data;
    }

    public void _SetRawData(AttackData _data){
        data = _data;
    }
}
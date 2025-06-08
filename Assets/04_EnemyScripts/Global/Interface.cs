using UnityEngine;


public interface IProduct
{
    void Setting(Enemy enemy);
}

public class ActionType : IProduct
{
    public Enemy Enemy;
    public virtual void Execute()
    {

    }

    public virtual void Setting(Enemy enemy)
    {
        Enemy = enemy;
    }

}
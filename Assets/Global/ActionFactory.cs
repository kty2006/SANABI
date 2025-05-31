using UnityEngine;

public abstract class ActionFactory
{
    public IAction createOperation<Type>() where Type : IAction, new()
    {
        IAction product = createAction<Type>();
        product.Setting();
        return product;
    }

    abstract protected IAction createAction<Type>() where Type : IAction, new();
}
public class AttackFactory : ActionFactory
{
    protected override IAction createAction<Type>()
    {
        return new Type();
    }
}

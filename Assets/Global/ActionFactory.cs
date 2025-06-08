using UnityEngine;

public abstract class ActionFactory
{
    public ActionType createOperation<Type>() where Type : ActionType, new()
    {
        ActionType product = createAction<Type>();
        product.Setting();
        return product;
    }

    abstract protected ActionType createAction<Type>() where Type : ActionType, new();
}
public class AttackFactory : ActionFactory
{
    protected override ActionType createAction<Type>()
    {
        return new Type();
    }
}

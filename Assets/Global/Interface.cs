using UnityEngine;

public interface IAction : IProduct
{
    void Execute();
}

public interface IProduct
{
    void Setting();
}
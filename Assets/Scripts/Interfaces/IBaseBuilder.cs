using System;
using UnityEngine;

public interface IBaseBuilder 
{
    event Action<Transform, IBaseBuilder> ReachedFlag;
    event Action<Worker> QuitMotherbase;
}

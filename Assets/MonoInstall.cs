using UnityEngine;
using Zenject;

public class MonoInstall : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Holder>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
I went to try what I thought would be a simple use of Extenject, put a Monobehavior in a scene, have some injection on it.
Have it link to some ScriptableObjects via a field you can set in the editor, have those ScriptableObjects have some injection.
Instead what I see is that the Monobehavior get it's injection called, and none of the fields do.  If I manually Inject into one
of those objects, it's injection is called but **IT'S** fields that are also waiting for injection don't.

I've distilled the entire thing down to a onehundred line project.

Quick overview to avoid clicking into sources:

![Inspector View](/images/inspector.png)


```cs
public class MonoInstall : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Holder>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}

public class DependenciesNotInjected : ScriptableObject
{
    public int someValue;
    public Holder holder;
    [Inject]
    public void ThisIsNeverCalled(Holder holder)
    {
        this.holder = holder;
        Debug.Log("You will never see this;");
    }
}

public class DepedencyIsInjectedManuallyButFieldOfItIsntInjected : ScriptableObject
{
    public DependenciesNotInjected thisWontBeInjected;
    public Holder holder;
    [Inject]
    public void ThisIsCalledFromHolderStart(Holder holder)
    {
        this.holder = holder;
        Debug.Log("Sandwhiched Between Start messages");
    }
}

public class Holder : MonoBehaviour
{
    public DependenciesNotInjected FirstNotInjected;
    public DependenciesNotInjected SecondNotInjected;
    public DepedencyIsInjectedManuallyButFieldOfItIsntInjected manualCall;
    private DiContainer _container;
    private bool _invokeOnce = false;
    [Inject]
    void ThisIsInvokedFine(DiContainer container)
    {
        Debug.Log("Invoked Holder's attribute message");
        _container = container;
    }
    void Start()
    {
        Debug.Log("About to Manually inject");
        _container.Inject(manualCall);
        Debug.Log("Finished Manual inject");
    }
    void Update()
    {
        if (!_invokeOnce)
        {
            _invokeOnce = true;
            Debug.Log($"In Update with First: {FirstNotInjected.holder?.name ?? "null"} second {SecondNotInjected.holder?.name ?? "null"} manual {manualCall.holder?.name ?? "null"} but manual's thiswontbeinjected; {manualCall.thisWontBeInjected.holder?.name ?? "null"}");
        }
    }
}
```

Output:
Invoked Holder's attribute message
About to Manually inject
Sandwhiched Between Start messages
Finished Manual inject
In Update with First: null second null manual GameObject but manual's thiswontbeinjected; null


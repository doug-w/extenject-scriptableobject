using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

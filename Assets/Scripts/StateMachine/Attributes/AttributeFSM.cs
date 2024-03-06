using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeFSM : StateMachineMB
{
    AttributeController attributeController;

    public AttributeMenuState AttributeMenuState { get; private set; }

    void OnValidate()
    {
        if (attributeController == null)
        {
            attributeController = GetComponent<AttributeController>();
        }
    }

    void Awake()
    {
        AttributeMenuState = new AttributeMenuState(attributeController, this);
    }

    void Start()
    {
        ChangeState(AttributeMenuState, 0.5f);
    }

    
}

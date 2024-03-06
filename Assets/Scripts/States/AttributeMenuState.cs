using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeMenuState : State
{
    AttributeController _controller;
    AttributeFSM _fsm;

    public AttributeMenuState(AttributeController controller, AttributeFSM fsm)
    {
        _controller = controller;
        _fsm = fsm;
    }

    public override void Enter()
    {
        _controller.AnimateAttributeMenuIntro();
    }

    public override void Exit()
    {
        _controller.AnimateAttributeMenuOutro();
    }

}

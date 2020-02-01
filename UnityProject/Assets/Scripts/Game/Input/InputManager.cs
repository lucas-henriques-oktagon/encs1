using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;
using UnityEngine;
using System.Collections.Generic;

public class InputManager : ComponentSystem
{
    private EntityQuery m_EntityQuery;
    private string[] m_InputMap = new string[]
    {
        "Horizontal_0",
        "Action_0",
        "Jump_0",
    };
        
    protected override void OnCreate()
    {
        m_EntityQuery = GetEntityQuery(ComponentType.ReadOnly<C_PlayerInput>());
    }

    protected override void OnUpdate()
    {
        var entities = m_EntityQuery.ToEntityArray(Allocator.TempJob);
        var playerInput = m_EntityQuery.ToComponentDataArray<C_PlayerInput>(Allocator.TempJob);

        for (int i = 0; i < playerInput.Length; i++)
        {
            //horizontal movement
            float x = Input.GetAxis(m_InputMap[playerInput[i].horizontal]);
            //add movement component

            if (x != 0)
                Debug.Log(x);
            
            //jump
            if (Input.GetButtonDown(m_InputMap[playerInput[i].jump]))
            {
                //add jump component
            }

            //action
            if (Input.GetButtonDown(m_InputMap[playerInput[i].action]))
            {
                //add pickup/drop component
            }
        }

        playerInput.Dispose();
        entities.Dispose();

    }
}
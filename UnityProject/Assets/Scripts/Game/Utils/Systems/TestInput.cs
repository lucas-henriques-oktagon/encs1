﻿using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class TestInput : ComponentSystem
{
    protected override void OnUpdate()
    {
        bool dale = (Input.GetMouseButtonDown(0));

        Entities.WithAll<C_CanPick>().ForEach(
            (Entity entity) =>
            {
                if (dale)
                {
                    EntityManager.AddComponent<TC_PickAction>(entity);
                }
            });
    }

}
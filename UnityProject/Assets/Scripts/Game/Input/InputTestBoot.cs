using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class InputTestBoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var player = entityManager.CreateEntity();
        entityManager.AddComponentData(player, new C_PlayerInput
        { 
            horizontal = 0,
            action = 1,
            jump = 2,
        });
    }
}

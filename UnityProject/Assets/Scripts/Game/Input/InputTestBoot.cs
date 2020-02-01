using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class InputTestBoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var player = entityManager.CreateEntity();
        entityManager.AddComponentData(player, new PlayerInput_C
        { 
            inputId = 0
        });
        entityManager.AddComponent<Translation>(player);
        entityManager.AddComponent<MovementComponentData>(player);
        entityManager.AddComponent<C_CanPick>(player);
        entityManager.SetName(player, "Player");

    }
}

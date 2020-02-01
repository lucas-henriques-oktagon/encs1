﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PickupPlayerConverter : MonoBehaviour, IConvertGameObjectToEntity
{
   public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
   {
       dstManager.AddComponent<C_CanPick>(entity);
       dstManager.AddComponent<Translation>(entity);
       dstManager.AddComponent<MovementComponentData>(entity);
       
       dstManager.SetComponentData(entity, new Translation
       {
           Value = transform.position,
       });
       
       dstManager.SetComponentData(entity, new MovementComponentData
       {
           speed = 0,
           directionLook = new int2 { x = 1, y = 0 }
       });
   }
}

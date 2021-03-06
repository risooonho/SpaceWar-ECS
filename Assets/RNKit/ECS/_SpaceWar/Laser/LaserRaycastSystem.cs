﻿using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace RN.Network.SpaceWar
{
    [DisableAutoCreation]
    //[AlwaysUpdateSystem]
    public class LaserRaycastServerSystem : JobComponentSystem//
    {
        [BurstCompile]
        [RequireComponentTag(typeof(OnPhysicsCallMessage))]
        struct OnLaserPhysicsCallMessageJobA : IJobForEach<PhysicsRaycast, RigidbodyForceAtPosition, Laser, Translation, Rotation>
        {
            public void Execute(ref PhysicsRaycast raycast, ref RigidbodyForceAtPosition rigidbodyForceAtPosition, [ReadOnly] ref Laser laser, [ReadOnly]ref Translation laserTranslation, [ReadOnly] ref Rotation laserRotation)
            {
                raycast.ray = new Ray { origin = laserTranslation.Value, direction = math.forward(laserRotation.Value) };
                raycast.ray.origin += raycast.ray.direction * laser.startOffset;
                raycast.distance = laser.distance - laser.startOffset;

                rigidbodyForceAtPosition.direction = raycast.ray.direction;
            }
        }

        [BurstCompile]
        [RequireComponentTag(typeof(OnPhysicsCallMessage))]
        struct OnLaserPhysicsCallMessageJobB : IJobForEach<PhysicsRaycastAll, RigidbodyForceAtPosition, Laser, Translation, Rotation>
        {
            public void Execute(ref PhysicsRaycastAll raycastAll, ref RigidbodyForceAtPosition rigidbodyForceAtPosition, [ReadOnly] ref Laser laser, [ReadOnly]ref Translation laserTranslation, [ReadOnly] ref Rotation laserRotation)
            {
                raycastAll.ray = new Ray { origin = laserTranslation.Value, direction = math.forward(laserRotation.Value) };
                raycastAll.ray.origin += raycastAll.ray.direction * laser.startOffset;
                raycastAll.distance = laser.distance - laser.startOffset;

                rigidbodyForceAtPosition.direction = raycastAll.ray.direction;
            }
        }

        [BurstCompile]
        [RequireComponentTag(typeof(OnPhysicsCallMessage))]
        struct OnLaserPhysicsCallMessageJobC : IJobForEach<PhysicsSphereCast, RigidbodyForceAtPosition, Laser, Translation, Rotation>
        {
            public void Execute(ref PhysicsSphereCast sphereCast, ref RigidbodyForceAtPosition rigidbodyForceAtPosition, [ReadOnly] ref Laser laser, [ReadOnly]ref Translation laserTranslation, [ReadOnly] ref Rotation laserRotation)
            {
                sphereCast.ray = new Ray { origin = laserTranslation.Value, direction = math.forward(laserRotation.Value) };
                sphereCast.ray.origin += sphereCast.ray.direction * laser.startOffset;
                sphereCast.distance = laser.distance - laser.startOffset;

                rigidbodyForceAtPosition.direction = sphereCast.ray.direction;
            }
        }

        [BurstCompile]
        [RequireComponentTag(typeof(OnPhysicsCallMessage))]
        struct OnLaserPhysicsCallMessageJobD : IJobForEach<PhysicsSphereCastAll, RigidbodyForceAtPosition, Laser, Translation, Rotation>
        {
            public void Execute(ref PhysicsSphereCastAll sphereCastAll, ref RigidbodyForceAtPosition rigidbodyForceAtPosition, [ReadOnly] ref Laser laser, [ReadOnly]ref Translation laserTranslation, [ReadOnly] ref Rotation laserRotation)
            {
                sphereCastAll.ray = new Ray { origin = laserTranslation.Value, direction = math.forward(laserRotation.Value) };
                sphereCastAll.ray.origin += sphereCastAll.ray.direction * laser.startOffset;
                sphereCastAll.distance = laser.distance - laser.startOffset;

                rigidbodyForceAtPosition.direction = sphereCastAll.ray.direction;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            inputDeps = new OnLaserPhysicsCallMessageJobA { }.Schedule(this, inputDeps);

            inputDeps = new OnLaserPhysicsCallMessageJobB { }.Schedule(this, inputDeps);

            inputDeps = new OnLaserPhysicsCallMessageJobC { }.Schedule(this, inputDeps);

            inputDeps = new OnLaserPhysicsCallMessageJobD { }.Schedule(this, inputDeps);

            return inputDeps;
        }
    }

}

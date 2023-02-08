using System;
using System.Reflection;
using GorillaLocomotion;
using HarmonyLib;
using UnityEngine;

namespace Slingshot_Teleport.Patches.GorillaLocomotionPatches
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    internal class PlayerTeleportPatch
    {
        internal static bool Prefix(ref Player __instance, ref Vector3 ___lastPosition, ref Vector3[] ___velocityHistory, ref Vector3 ___lastHeadPosition, ref Vector3 ___lastLeftHandPosition, ref Vector3 ___lastRightHandPosition, ref Vector3 ___currentVelocity, ref Vector3 ___denormalizedVelocityAverage)
        {
            bool flag = PlayerTeleportPatch.isTeleporting;
            bool result;
            if (flag)
            {
                bool flag2 = __instance.GetComponent<Rigidbody>() != null;
                if (flag2)
                {
                    PlayerTeleportPatch.teleportDestination.y = PlayerTeleportPatch.teleportDestination.y + 0.75f;
                    __instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    __instance.GetComponent<Rigidbody>().isKinematic = true;
                    __instance.transform.position = PlayerTeleportPatch.teleportDestination;
                    ___lastPosition = PlayerTeleportPatch.teleportDestination;
                    ___velocityHistory = new Vector3[__instance.velocityHistorySize];
                    ___lastHeadPosition = __instance.headCollider.transform.position;
                    MethodInfo method = typeof(Player).GetMethod("CurrentLeftHandPosition", BindingFlags.Instance | BindingFlags.NonPublic);
                    ___lastLeftHandPosition = (Vector3)method.Invoke(__instance, new object[0]);
                    MethodInfo method2 = typeof(Player).GetMethod("CurrentRightHandPosition", BindingFlags.Instance | BindingFlags.NonPublic);
                    ___lastRightHandPosition = (Vector3)method2.Invoke(__instance, new object[0]);
                    ___currentVelocity = Vector3.zero;
                    ___denormalizedVelocityAverage = Vector3.zero;
                    __instance.GetComponent<Rigidbody>().isKinematic = false;
                }
                PlayerTeleportPatch.isTeleporting = false;
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        internal static void TeleportPlayer(Vector3 destination)
        {
            bool flag = PlayerTeleportPatch.isTeleporting;
            if (!flag)
            {
                PlayerTeleportPatch.teleportDestination = destination;
                PlayerTeleportPatch.isTeleporting = true;
            }
        }

        public PlayerTeleportPatch()
        {
        }

        private static bool isTeleporting;

        private static Vector3 teleportDestination;
    }
}

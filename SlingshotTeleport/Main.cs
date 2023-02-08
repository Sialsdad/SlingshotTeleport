using System;
using BepInEx;
using Photon.Pun;
using Slingshot_Teleport.Patches.HarmonyPatches;
using UnityEngine;
using Utilla;

namespace Slingshot_Teleport
{
    // Token: 0x02000003 RID: 3
    [BepInPlugin("gorillatag.Slingshot_Teleport", "Slingshot Teleport", "0.0.1")]
    public class Main : BaseUnityPlugin
    {
        bool on = false;

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            on = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            on = false;
        }


        private void OnEnable()
        {
            this.on = true;
            HarmonyPatches.ApplyHarmonyPatches();
        }


        private void OnDisable()
        {
            this.on = false;
            HarmonyPatches.RemoveHarmonyPatches();
        }


        private void FixedUpdate()
        {
            if (on)
            {
                bool flag = GorillaGameManager.instance != null && GorillaTagger.Instance.myVRRig.slingshot != null;
                if (flag)
                {
                    foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("SlingshotProjectile"))
                    {
                        bool flag2 = gameObject.GetComponent<BallCollision>() == null && gameObject.activeSelf;
                        if (flag2)
                        {
                            bool flag3 = gameObject.GetComponent<SlingshotProjectile>().projectileOwner == PhotonNetwork.LocalPlayer;
                            if (flag3)
                            {
                                Debug.Log("Added BallCollision component to SlingshotProjectile.");
                                gameObject.AddComponent<BallCollision>();
                            }
                        }
                    }
                }
            }
        }

        public Main()
        {
        }
    }
}

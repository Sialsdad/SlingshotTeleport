using System;
using GorillaLocomotion;
using Photon.Pun;
using Slingshot_Teleport.Patches.GorillaLocomotionPatches;
using UnityEngine;

namespace Slingshot_Teleport
{
    public class BallCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            bool flag = base.gameObject.GetComponent<SlingshotProjectile>().projectileOwner == PhotonNetwork.LocalPlayer && (double)(Player.Instance.transform.position - base.gameObject.GetComponent<SlingshotProjectile>().transform.position).magnitude > 1.5;
            if (flag)
            {
                Debug.Log(" ");
                Debug.Log(base.gameObject.GetComponent<SlingshotProjectile>().projectileOwner.NickName);
                Debug.Log(" ");
                PlayerTeleportPatch.TeleportPlayer(base.gameObject.transform.position);
            }
        }

        public BallCollision()
        {
        }
    }
}

using UnityEngine;

namespace AtanUtils.Extensions
{
    public static class PhysicsExtensions
    {
        public static void TorqueRotateTo(this Rigidbody rb, Quaternion targetRotation, float torqueForce)
        {
            // Compute the delta rotation from current to target
            Quaternion delta = targetRotation * Quaternion.Inverse(rb.rotation);
        
            // Make sure we take the shortest path
            if (delta.w < 0f)
            {
                delta.x = -delta.x;
                delta.y = -delta.y;
                delta.z = -delta.z;
                delta.w = -delta.w;
            }

            // Convert to axis‐angle (angle in degrees, axis normalized)
            delta.ToAngleAxis(out float angleDegrees, out Vector3 axis);
            if (angleDegrees > 180f) 
                angleDegrees -= 360f;

            // If nearly aligned, no need to apply torque
            if (Mathf.Abs(angleDegrees) < 0.01f)
                return;

            // Convert to radians for physical torque
            float angleRadians = angleDegrees * Mathf.Deg2Rad;

            // Compute and apply torque: τ = axis × (angle × force)
            Vector3 torque = axis * angleRadians * torqueForce;
            rb.AddTorque(torque, ForceMode.Force);
        }
        
        public static void TorqueLookAt(this Rigidbody rb, Vector3 target, float torqueForce)
        {
            Vector3 toTarget = target - rb.position;
            if (toTarget.sqrMagnitude < Mathf.Epsilon)
                return;

            // Desired orientation
            Quaternion desired = Quaternion.LookRotation(toTarget, rb.transform.up);

            // Rotation difference
            Quaternion delta = desired * Quaternion.Inverse(rb.rotation);

            // Convert to axis‐angle
            delta.ToAngleAxis(out float angleDeg, out Vector3 axis);
            if (angleDeg > 180f) 
                angleDeg -= 360f;
            if (Mathf.Abs(angleDeg) < 0.1f) 
                return;

            // Compute torque (radians × axis × gain)
            float angleRad = angleDeg * Mathf.Deg2Rad;
            Vector3 torque = axis.normalized * (angleRad * torqueForce);

            // Apply as acceleration so mass is ignored
            rb.AddTorque(torque, ForceMode.Acceleration);
        }
    }
}
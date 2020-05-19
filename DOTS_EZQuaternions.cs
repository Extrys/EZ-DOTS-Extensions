using Unity.Mathematics;
using static Unity.Mathematics.math;
using static Unity.Mathematics.quaternion;

namespace SquirrelBytes.DOTS
{
	public static class EZ_DOTSMaths
	{
		public static float4 ToAngleAxis(this in quaternion q) => q.value.ToAngleAxis();
		public static float4 ToAngleAxis(this in float4 q)
		{
			float len = length(q.xyz);
			return float4(select(float3(1, 0, 0), q.xyz / len, len > 0), 2 * atan2(len, q.w));
		}

		public static void ToAngleAxis(this in quaternion q, out float ang, out float3 ax) => q.value.ToAngleAxis(out ang, out ax);
		public static void ToAngleAxis(this in float4 q, out float ang, out float3 ax)
		{
			float len = length(q.xyz);
			ang = 2 * atan2(len, q.w);
			ax = select(float3(1, 0, 0), q.xyz / len, len > 0);
		}

		public static quaternion ToQuaternion(this in float4 angleAxis) => AxisAngle(angleAxis.xyz, angleAxis.w);

		public static quaternion GetRotation(quaternion lhsq, quaternion rhsq)
		{
			float4 lhs = lhsq.value;
			float4 rhs = rhsq.value;
			return quaternion(
			    lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
			    lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
			    lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
			    lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}
	}
}

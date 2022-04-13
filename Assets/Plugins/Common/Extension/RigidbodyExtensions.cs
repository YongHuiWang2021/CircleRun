using UnityEngine;

namespace XD.Core
{
    public static class RigidbodyExtensions
    {
         /// <summary>
        /// 对象的质量
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="mass">（默认为千克）</param>
        public static void SetMass ( this Rigidbody rigidbody,float mass)
        {
            if (rigidbody != null)
            {
                rigidbody.mass = mass;
            }
        }
        /// <summary>
        /// 根据力移动对象时影响对象的空气阻力大小。0 表示没有空气阻力，无穷大使对象立即停止移动。
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="drag"></param>
        public static void SetDrag(this Rigidbody rigidbody,float drag)
        {
            if (rigidbody != null)
            {
                rigidbody.drag = drag;
            }
        }
        /// <summary>
        /// 根据扭矩旋转对象时影响对象的空气阻力大小。0 表示没有空气阻力。请注意，如果直接将对象的 Angular Drag 属性设置为无穷大，无法使对象停止旋转
        /// </summary>
        /// <param name="rigidbody"></param>
        public static void SetAngularDrag(this Rigidbody rigidbody,float angularDrag)
        {
            if (rigidbody != null)
            {
                rigidbody.angularDrag = angularDrag;
            }
        }
        /// <summary>
        /// 如果启用此属性，则对象将受重力影响
        /// </summary>
        /// <param name="rigidbody"></param>
        public static void SetUseGravity(this Rigidbody rigidbody,bool useGravity)
        {
            if (rigidbody != null)
            {
                rigidbody.useGravity = useGravity;
            }
        }
        /// <summary>
        /// 如果启用此选项，则对象将不会被物理引擎驱动，只能通过__变换 (Transform)__ 对其进行操作。对于移动平台，或者如果要动画化附加了 HingeJoint 的刚体，此属性将非常有用。
        /// </summary>
        /// <param name="rigidbody"></param>
        public static void SetKinematic(this Rigidbody rigidbody,bool kinematic)
        {
            if (rigidbody != null)
            {
                rigidbody.isKinematic = kinematic;
            }
        }
        /// <summary>
        ///仅当在刚体运动中看到急动时才尝试使用提供的选项之一。
        /// </summary>
        /// <param name="rigidbody"></param>
        public static void SetInterpolate(this Rigidbody rigidbody,RigidbodyInterpolation interpolation)
        {
            if (rigidbody != null)
            {
             rigidbody.interpolation = interpolation;
            }
        } 
        /// <summary>
        ///用于防止快速移动的对象穿过其他对象而不检测碰撞
        ///Discrete	对场景中的所有其他碰撞体使用离散碰撞检测。其他碰撞体在测试碰撞时会使用离散碰撞检测。用于正常碰撞（这是默认值）。
        ///  - Continuous	对动态碰撞体（具有刚体）使用离散碰撞检测，并对静态碰撞体（没有刚体）使用基于扫掠的连续碰撞检测。设置为__连续动态 (Continuous Dynamic)__ 
        ///  的刚体将在测试与该刚体的碰撞时使用连续碰撞检测。其他刚体将使用离散碰撞检测。用于__连续动态 (Continuous Dynamic)__ 检测需要碰撞的对象。
        ///（此属性对物理性能有很大影响，如果没有快速对象的碰撞问题，请将其保留为 Discrete 设置）
        ///对设置为__连续 (Continuous)__ 和__连续动态 (Continuous Dynamic)__ 碰撞的游戏对象使用基于扫掠的连续碰撞检测。
        ///  还将对静态碰撞体（没有刚体）使用连续碰撞检测。对于所有其他碰撞体，使用离散碰撞检测。用于快速移动的对象。
        ///对刚体和碰撞体使用推测性连续碰撞检测。这也是可以设置运动物体的唯一 CCD 模式。该方法通常比基于扫掠的连续碰撞检测的成本更低。
        /// </summary>
        /// <param name="rigidbody"></param>
        public static void SetCollisionDetection(this Rigidbody rigidbody,CollisionDetectionMode Collision)
        {
            if (rigidbody != null)
            {
                rigidbody.collisionDetectionMode = Collision;
            }
        }
        /// <summary>
        ///	对刚体运动的限制：
        ///Freeze Position	有选择地停止刚体沿世界 X、Y 和 Z 轴的移动。
        /// Freeze Rotation	有选择地停止刚体围绕局部 X、Y 和 Z 轴旋转。
        /// </summary>
        /// <param name="rigidbody"></param>
        public static void SetConstraints(this Rigidbody rigidbody,RigidbodyConstraints constraints)
        {
            if (rigidbody != null)
            {
                rigidbody.constraints = constraints;
            }
        } 
    }
}
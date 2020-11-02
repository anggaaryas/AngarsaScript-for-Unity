using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AngarsaScript
{
    public class Transform
    {
        static Transform instance = null;

        private GameObject gameObject;
        private Pref.Anim animPref;
        private Pref.Physic physicPref;

        private Transform()
        {

        }

        private Transform(GameObject obj, Pref.Anim anim = new Pref.Anim(), Pref.Physic physic = new Pref.Physic())
        {
            gameObject = obj;
            animPref = anim;
            physicPref = physic;
        }

        public static Transform from(GameObject obj)
        {
            instance = new Transform(obj);
            return instance;
        }

        public Transform withAnimating(float speed)
        {
            Pref.Anim anim = new Pref.Anim(true, speed);
            instance.animPref = anim;
            return instance;

        }

        public Transform withPhysic()
        {
            Pref.Physic physic = new Pref.Physic(true);
            instance.physicPref = physic;
            return instance;
        }

        public void moveBy(float x = 0f, float y = 0f, float z = 0f)
        {
            Vector3 pos = new Vector3(x, y, z);
            if (instance.animPref.isAnimating)
            {
                pos *= Time.deltaTime;
                pos *= instance.animPref.speed;
            }

            instance.gameObject.transform.position += pos;
        }

        public void moveTo(float x = 0f, float y = 0f, float z = 0f)
        {
            Vector3 pos = new Vector3(x, y, z);

            if (instance.animPref.isAnimating)
            {
                float step = Time.deltaTime * instance.animPref.speed;

                if (Vector3.Distance(instance.gameObject.transform.position, pos) > 0.001f)
                {
                    instance.gameObject.transform.position = Vector3.MoveTowards(instance.gameObject.transform.position, pos, step);
                }
            }
            else
            {
                instance.gameObject.transform.position = pos;
            }

        }

        public void moveWithPhysic(float x = 0f, float y = 0f, float z = 0f)
        {
            Vector3 force = new Vector3(x, y, z);
            instance.gameObject.GetComponent<Rigidbody>()
                .AddForce(force * instance.animPref.speed);
        }

        public void rotateWithPhysic(float x = 0f, float y = 0f, float z = 0f)
        {
            Vector3 force = new Vector3(x, y, z);
            instance.gameObject.GetComponent<Rigidbody>()
                .AddTorque(force * instance.animPref.speed );
        }

        public void rotateTo(float x = 0f, float y = 0f, float z = 0f)
        {

            if (instance.animPref.isAnimating)
            {
                float step = instance.animPref.speed * Time.deltaTime;

                Vector3 angle = Vector3.RotateTowards(instance.gameObject.transform.forward, new Vector3(x, y, z), step, 0f);
                instance.gameObject.transform.rotation = Quaternion.LookRotation(angle);
            }
            else
            {
                instance.gameObject.transform.rotation = Quaternion.Euler(x, y, z);
            }
        }

        public void rotateBy(float x = 0f, float y = 0f, float z = 0f)
        {
            Vector3 angle = new Vector3(x, y, z);
            if (instance.animPref.isAnimating)
            {
                angle *= instance.animPref.speed;
            }

            instance.gameObject.transform.Rotate(angle);
        }


    }

    public class Pref
    {
        public struct Anim
        {
            public Anim(bool isAnimating = false, float speed = 1f)
            {
                this.isAnimating = isAnimating;
                this.speed = speed;
            }

            public bool isAnimating { get; }
            public float speed { get; }
        };

        public struct Physic
        {
            public Physic(bool hasPhysic = false)
            {
                this.hasPhysic = hasPhysic;
            }

            public bool hasPhysic { get; }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AngarsaScript
{
    public class Transform
    {
        private static GameObject gameObject;
        private static Pref.Anim animPref;
        private static Pref.Physic physicPref;

        private Transform(GameObject obj, Pref.Anim anim = new Pref.Anim(), Pref.Physic physic = new Pref.Physic())
        {
            gameObject = obj;
            animPref = anim;
            physicPref = physic;
        }

        public static Transform from(GameObject obj)
        {
            return new Transform(obj);
        }

        public Transform withAnimating(float speed)
        {
            Pref.Anim anim = new Pref.Anim(true, speed);
            return new Transform(gameObject, anim);

        }

        public Transform withPhysic()
        {
            Pref.Physic physic = new Pref.Physic(true);
            return new Transform(gameObject, animPref, physic);
        }

        public void moveBy(float posX = 0f, float posY = 0f, float posZ = 0f)
        {
            Vector3 pos = new Vector3(posX, posY, posZ);
            if (animPref.isAnimating)
            {
                pos *= Time.deltaTime;
                pos *= animPref.speed;
            }

            gameObject.transform.position += pos;
        }

        public void moveTo(float posX = 0f, float posY = 0f, float posZ = 0f)
        {
            Vector3 pos = new Vector3(posX, posY, posZ);

            if (animPref.isAnimating)
            {
                float step = Time.deltaTime * animPref.speed;

                if (Vector3.Distance(gameObject.transform.position, pos) > 0.001f)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pos, step);
                }
            }
            else
            {
                gameObject.transform.position = pos;
            }

        }

        public void moveWithPhysic(float inputX = 0f, float inputY = 0f, float inputZ = 0f)
        {
            Vector3 force = new Vector3(inputX, inputY, inputZ);
            if (animPref.isAnimating) force *= animPref.speed;
            gameObject.GetComponent<Rigidbody>().AddForce(force);
        }

        public void rotateTo(float x = 0f, float y = 0f, float z = 0f)
        {

            if (animPref.isAnimating)
            {
                float step = animPref.speed * Time.deltaTime;

                Vector3 angle = Vector3.RotateTowards(gameObject.transform.forward, new Vector3(x, y, z), step, 0f);
                gameObject.transform.rotation = Quaternion.LookRotation(angle);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(x, y, z);
            }
        }

        public void rotateBy(float x = 0f, float y = 0f, float z = 0f)
        {
            Vector3 angle = new Vector3(x, y, z);
            if (animPref.isAnimating)
            {
                angle *= animPref.speed;
            }

            gameObject.transform.Rotate(angle);
        }


    }

    public class Pref
    {
        public struct Anim
        {
            public Anim(bool isAnimating = false, float speed = 0f)
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

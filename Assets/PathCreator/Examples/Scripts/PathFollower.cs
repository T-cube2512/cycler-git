using UnityEngine;

namespace PathCreation.Examples
{
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public SerialInputController ser;
        public float speed;
        float distanceTravelled;

        void Start() {
            if (pathCreator != null)
            {
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                speed = ser.fnalspeed / 2;
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        public float distanceTravelled;
        public float pathOffset;
        [SerializeField] private bool _followPathRotation = true;
        [SerializeField] private bool _isPlayer = false;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }

        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled + pathOffset, endOfPathInstruction);
                if(_followPathRotation)
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled + pathOffset, endOfPathInstruction);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        public void SetPathCreator(PathCreator _pathCreator)
        {
            pathCreator = _pathCreator;
        }

        public void SetDistanceInPath(float _distanceTravelled)
        {
            _followPathRotation = true;
            distanceTravelled = _distanceTravelled;
            transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
            StartCoroutine(WaitAndCanRotate());
        }

        public void SetDistanceInPath(float _distanceTravelled, bool isTeleporter)
        {
            distanceTravelled = _distanceTravelled;
            transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
        }

        private IEnumerator WaitAndCanRotate()
        {
            yield return new WaitForSeconds(0.3f);
            if(_isPlayer)
                _followPathRotation = false;
        }

        public void SetPathOffset(float offset)
        {
            pathOffset = offset;
        }
    }
}
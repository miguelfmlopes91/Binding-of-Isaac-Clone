﻿using DungeonGeneration;
using UnityEngine;

namespace Game.Camera
{
    public class CameraController : MonoBehaviour
    {
        public Room currRoom;
        public float moveSpeedWhenRoomChange;

        // Update is called once per frame
        void Update()
        {
            UpdatePosition();
        }

        void UpdatePosition()
        {
            if(currRoom == null)
            {
                return;
            }

            Vector3 targetPos = GetCameraTargetPosition();

            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedWhenRoomChange);
        }

        Vector3 GetCameraTargetPosition()
        {
            if(currRoom == null)
            {
                return Vector3.zero;
            }

            Vector3 targetPos = currRoom.GetRoomCentre();
            targetPos.z = transform.position.z;

            return targetPos;
        }

        public bool IsSwitchingScene()
        {
            return transform.position.Equals( GetCameraTargetPosition()) == false;
        }
    }
}
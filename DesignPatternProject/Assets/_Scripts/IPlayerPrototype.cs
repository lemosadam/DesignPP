using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IPlayerPrototype
    {
        void Initialize(GameObject playerPrefab);
        GameObject Clone(Vector3 position, Quaternion rotation);
    }


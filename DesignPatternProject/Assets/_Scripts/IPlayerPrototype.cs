using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IPlayerPrototype
    {
        void Initialize(GameObject enemyPrefab);
        GameObject Clone(Vector3 position, Quaternion rotation);
    }


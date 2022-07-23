﻿using PathCreation;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{

    public PathCreator pathPrefab;
    public PathFollower followerPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        foreach (Transform t in spawnPoints)
        {
            var path = Instantiate(pathPrefab, t.position, t.rotation);
            var follower = Instantiate(followerPrefab);
            follower.PathCreator = path;
            path.transform.parent = t;

        }
    }
}


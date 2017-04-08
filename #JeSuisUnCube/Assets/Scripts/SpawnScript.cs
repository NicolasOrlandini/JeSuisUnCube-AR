﻿using System.Collections;
using UnityEngine;
using Vuforia;

public class SpawnScript : MonoBehaviour
{
    // Cube element to spawn
    public GameObject mCubeObj;

    // Qtd of Cubes to be Spawned
    public int mTotalCubes = 25;

    // Time to spawn the Cubes
    public float mTimeToSpawn = 0.8f;

    // hold all cubes on stage
    private GameObject[] mCubes;

    // define if position was set
    private bool mPositionSet;

    // Use this for initialization
    void Start()
    {
        // Defining the Spawning Position
        StartCoroutine(SpawnLoop());

        // Initialize Cubes array according to
        // the desired quantity
        mCubes = new GameObject[mTotalCubes];
    }
    

    // Define the position if the object
    // according to ARCamera position
    private bool SetPosition()
    {
        // get the camera position
        Transform cam = Camera.main.transform;

        // set the position 10 units forward from the camera position
        transform.position = cam.forward * 10;
        return true;
    }

    // We'll use a Coroutine to give a little
    // delay before setting the position
    private IEnumerator ChangePosition()
    {

        yield return new WaitForSeconds(0.2f);
        // Define the Spawn position only once
        if (!mPositionSet)
        {
            // change the position only if Vuforia is active
            if (VuforiaBehaviour.Instance.enabled)
            {
                SetPosition();
            }
        }
    }

    // Loop Spawning cube elements
    private IEnumerator SpawnLoop()
    {
        // Defining the Spawning Position
        StartCoroutine(ChangePosition());

        yield return new WaitForSeconds(0.2f);

        // Spawning the elements
        int i = 0;
        while (i <= (mTotalCubes - 1))
        {

            mCubes[i] = SpawnElement();
            i++;
            yield return new WaitForSeconds(Random.Range(mTimeToSpawn, mTimeToSpawn * 2));
        }
    }

    // Spawn a cube
    public GameObject SpawnElement()
    {
        // spawn the element on a random position, inside a imaginary sphere
        GameObject cube = Instantiate(mCubeObj, (Random.insideUnitSphere * 4) + transform.position, transform.rotation) as GameObject;
        // define a random scale for the cube
        float scale = Random.Range(1.2f, 1.8f);
        // change the cube scale
        cube.transform.localScale = new Vector3(scale, scale, scale);
        return cube;
    }
}

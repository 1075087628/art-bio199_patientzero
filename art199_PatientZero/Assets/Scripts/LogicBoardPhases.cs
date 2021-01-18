﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LogicBoardPhases : MonoBehaviour
{
    public GameObject board_zones;
    //public List<GameObject> test;

    private List<List<GameObject>> phases = new List<List<GameObject>>();
    public int currentPhase;

    private void Start()
    {
        for(int i = 0; i < board_zones.transform.childCount; i++) //need to populate Phases list of lists
        {
            phases.Add(new List<GameObject>());
            GameObject phaseZoneParent = board_zones.transform.GetChild(i).GetChild(0).GetChild(0).gameObject; // grab the current Parent Zone game object
            phases[i].Add(phaseZoneParent); // add parent to count for phases (base fill counts)

            int phaseZones = phaseZoneParent.transform.childCount; //assigns the number of snap zones (children) for this phase
            Debug.Log("Phase " + i + " has " + phaseZones + " children." );
            for (int j = 0; j < phaseZones; j++)
            {
                GameObject childZone = phaseZoneParent.transform.GetChild(j).gameObject;
                Debug.Log(childZone.name);
                if (childZone.tag == "SnapZone") //loop through each child object of the snapzone parent, exclude VRTK highlighter for zone
                {
                    phases[i].Add(childZone.gameObject);
                }
            }
        }
        //test = phases[1];
        currentPhase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool phase_finished = false;
        for(int i = 0; i < phases[currentPhase].Count; i++)
        {
            VRTK_SnapDropZone phaseSnapZone = phases[currentPhase][i].GetComponent<VRTK_SnapDropZone>(); //select the current snap zone from the phase we are in.
            if (phaseSnapZone.GetCurrentSnappedObject() == null)
            {
                //Debug.Log("Phase Snap Zone #" + i);
                break;
            }
            if(i == phases[currentPhase].Count-1)
            {
                Debug.Log("Advancing Phase");
                phase_finished = true;
                currentPhase += 1;
            }
        }
    }
}
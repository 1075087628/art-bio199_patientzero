﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDNA : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject leftSegmentPrefab;

    [SerializeField]
    private GameObject rightSegmentPrefab;

    [SerializeField]
    private float offsetScalar;

    public System.String startingStrands;

    public bool isRandomlyGenerated = true; // leave this public as well
    public bool isRNA = false;  // leave this public; otherwise, it won't know to be RNA...

    private bool puzzleComplete = false;

    // store the state of each segment in an array when spawned...
    GameObject puzzleSegment;
    GameObject playerSegment;

    TextMesh[] puzzleStrands;
    TextMesh[] playerStrands;

    void Start()
    {
        SpawnDNASegments();

        // this will randomly generate the strand you have to solve for
        if (isRandomlyGenerated)
        {
            RandomizeStrands();
        }

        // this will take an array of chars that will be used to 
        // decide the strand you have to solve
        else
        {
            CreateCustomStrand(startingStrands);
        }
    }

    private void Update()
    {
        puzzleSolved();
    }

    void puzzleSolved()
    {

        for (int i = 0; i < puzzleStrands.Length; ++i)
        {
            if (puzzleStrands[i].text != OppositeBase(playerStrands[i].text))
            {
                return;
            }
        }

        if (puzzleComplete == false)
        {
            SnapTogether();
            puzzleComplete = true;
        }
    }



    string OppositeBase(string strandBase)
    {
        if (strandBase == "A")
        {
            if (isRNA)
            {
                return "U";
            }

            return "T";
        }

        else if (strandBase == "U")
        {
            return "A";
        }

        else if (strandBase == "T")
        {
            return "A";
        }

        else if (strandBase == "C")
        {
            return "G";
        }

        else if (strandBase == "G")
        {
            return "C";
        }

        return "ERROR";
    }

    void SnapTogether()
    {
        playerSegment.transform.Translate(offsetScalar * Vector3.left, Space.World);
    }


    void SpawnDNASegments()
    {
        // Spawns the segment that you have to solve against...
        puzzleSegment = Instantiate(leftSegmentPrefab, gameObject.transform);
        puzzleStrands = puzzleSegment.GetComponentsInChildren<TextMesh>();
        
        // Spawns the segment that the player has to modify in order to match it... 
        playerSegment = Instantiate(rightSegmentPrefab, gameObject.transform);
        playerSegment.transform.Translate(offsetScalar * Vector3.right, Space.World);
      
        playerStrands = playerSegment.GetComponentsInChildren<TextMesh>();
    }

    void RandomizeStrands()
    {
        RandomizeBases(puzzleStrands, isRNA);

        // Makes sure the player segment won't end up being the same as the puzzle strand
        do
        {
            RandomizeBases(playerStrands, isRNA);
        }
        while (puzzleEqualsPlayer());
    }

    void CreateCustomStrand(System.String bases)
    {
        if (bases.Length != playerStrands.Length)
        {
            Debug.LogError("CreateCustomStrand(): bases has more or less cells than the strands available");
        }

        else
        {
            // check bases to see if the characters used are valide
            string DNA = "GTAC";
            string RNA = "GCAU";

          
            for (int i = 0; i < bases.Length; ++i)
            {
                System.String currentBase = bases[i].ToString();

                if (isRNA)
                {
                    if (!RNA.Contains(currentBase))
                    {
                        Debug.LogError("CreateCustomsStrand(): Invalid bases");
                    }
                }
                else
                {
                    if (!DNA.Contains(currentBase))
                    {
                        Debug.LogError("CreateCustomsStrand(): Invalid bases");
                    }
                }

            }

            for (int i = 0; i < playerStrands.Length; ++i)
            {
                playerStrands[i].text = bases[i].ToString();
            }

            // Makes sure the puzzle segment won't end up being the same as the player strand
            do
            {
                RandomizeBases(puzzleStrands, isRNA);
            }
            while (puzzleEqualsPlayer());
        }
    }
    

    bool puzzleEqualsPlayer()
    {
        for (int i = 0; i < puzzleStrands.Length; ++i)
        {
            if (puzzleStrands[i] != playerStrands[i])
            {
                return false;
            }
        }

        return true;
    }

    
    void RandomizeBases(TextMesh[] segmentStrands, bool isRNA)
    {
        for (int i = 0; i < segmentStrands.Length; ++i)
        {
            int baseCode = Random.Range(0, 4);
            segmentStrands[i].text = SelectBase(baseCode, isRNA);
        }
    }

    string SelectBase(int baseNumber, bool isRNA)
    {
        switch (baseNumber)
        {
            case 0:
                return "A";

            case 1:
                if (isRNA)
                {
                    return "U";
                }

                return "T";

            case 2:
                return "C";

            case 3:
                return "G";

            default:
                return "ERROR";
        }
    }
}
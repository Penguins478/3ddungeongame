using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPDungeon : MonoBehaviour
{
    public int MAX_SECTION_SIZE = 20;
    public int width;
    public int height;
    public List<GameObject> roomTypes;
    public GameObject wall;
    private List<Section> sections = new List<Section>();
    private Section helperSection;
    private Section root;

    public GameObject roomA;
    public GameObject roomB;
    public GameObject roomA1;
    public GameObject roomB1;

    // Start is called before the first frame update
    void Start()
    {
        
        root = new Section(0, 0, height, width);
        
        sections.Add(root);
        bool hasSplit = true;
        
        while(hasSplit) {
            hasSplit = false;
            List<Section> tempSections = new List<Section>();
            foreach(Section helperSection in sections) {
                if(helperSection.leftSection.height == -1 && helperSection.rightSection.height == -1) {
                    //make sure the section isn't too big or 50% of splitting anyway  || Random.Range(0, 4) > 1
                    if(helperSection.width > MAX_SECTION_SIZE || helperSection.height > MAX_SECTION_SIZE) {
                        //splits the section (in addition to checking if it can split)
                        if(helperSection.split()) {
                            tempSections.Add(helperSection.leftSection);
                            tempSections.Add(helperSection.rightSection);
                            hasSplit = true;
                        }
                    }else {
                        helperSection.dontSplit = true;
                    }
                }
            }
            sections.AddRange(tempSections);
        }

        root.createRooms(roomTypes);
        root.createHallways(wall);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour{
    private const int MIN_SIZE = 32;
    //will split each section in a specific way if the ratio 
    //between length and width is greater than this
    private const float SPLIT_RATIO = 1.5f;
    public int x, y, width, height;
    public bool dontSplit = false;

    public Section leftSection;
    public Section rightSection;

    private GameObject room;

    public Section(int x, int y, int height, int width) {
        if(height != -1) {
            leftSection = new Section(0, 0, -1, -1);
            rightSection = new Section(0, 0, -1, -1);
        }
        this.x = x;
        this.y = y;
        this.height = height;
        this.width = width;
    }

    public bool split() {
        if(dontSplit) {
            return false;
        }
        if(leftSection.height != -1 || rightSection.height != -1) {
            return false;
        }
        
        bool splitHeight = Random.Range(0, 2) == 0 ? true : false;
        if(width > height && (width / height) > SPLIT_RATIO) {
            splitHeight = false;
        }else if(height > width && (height / width) > SPLIT_RATIO) {
            splitHeight = true;
        }
        
        //check if there's enough space to keep splitting
        int remainingSpace = (splitHeight ? height : width) - MIN_SIZE;
        if(remainingSpace <= MIN_SIZE) {
            //remaining space is too small to fit another room
            return false;
        }

        int splitPos = Random.Range(MIN_SIZE + 1, remainingSpace);

        if(splitHeight) {
            leftSection = new Section(x, y + splitPos, height - splitPos, width);
            rightSection = new Section(x, y, splitPos, width);
        }else {
            leftSection = new Section(x, y, height, splitPos);
            rightSection = new Section(x + splitPos, y, height, width - splitPos);
        }
        return true;
    }
    public void createRooms(List<GameObject> roomTypes) {
        if(leftSection.height != -1) {
            leftSection.createRooms(roomTypes);
            if(rightSection.height != -1) {
                rightSection.createRooms(roomTypes);
            }
        }else {
            List<GameObject> possibleRooms = new List<GameObject>();
            foreach(GameObject specificRoom in roomTypes) {
                float roomWidth = specificRoom.transform.Find("Ground").transform.localScale.x;
                float roomHeight = specificRoom.transform.Find("Ground").transform.localScale.z;
                if(roomWidth <= width - 2 && roomHeight <= height - 2) {
                    possibleRooms.Add(specificRoom);
                }
            }
            int numPossibleRooms = possibleRooms.Count;
            int roomToCreate = Random.Range(0, numPossibleRooms);
            for (int i = numPossibleRooms - 1; i >= 0; i--) {
                if(Random.Range(0, 3) < 2) {
                    roomToCreate = i;
                    break;
                }
            }
            float chosenRoomWidth = possibleRooms[roomToCreate].transform.Find("Ground").transform.localScale.x;
            float chosenRoomHeight = possibleRooms[roomToCreate].transform.Find("Ground").transform.localScale.z;
            this.room = Instantiate(possibleRooms[roomToCreate], new Vector3(x + Random.Range((int) (chosenRoomWidth / 2 + 1), width - (int) (chosenRoomWidth / 2)), 0, y + Random.Range((int) (chosenRoomHeight / 2 + 1), height - (int) (chosenRoomHeight / 2))), Quaternion.identity) as GameObject;

        }
    }
    public void printPos() {
        if(leftSection.height != -1) {
            leftSection.printPos();
            if(rightSection.height != -1) {
                rightSection.printPos();
            }
        }
    }
    public void createHallways(GameObject wallPiece) {
        if(leftSection.height == -1 || rightSection.height == -1) {
            return;
        }else {
            leftSection.createHallways(wallPiece);
            rightSection.createHallways(wallPiece);
        }
        if(leftSection.x == rightSection.x) {
            if(Random.Range(0, 2) == 1) {
                GameObject roomA = leftSection.getBottomLeftRoom();
                GameObject roomB = rightSection.getTopLeftRoom();
                buildNewHallway(roomA, roomB, wallPiece);
            }else {
                GameObject roomA = leftSection.getBottomRightRoom();
                GameObject roomB = rightSection.getTopRightRoom();
                buildNewHallway(roomA, roomB, wallPiece);
            }
        }else {
            if(Random.Range(0, 2) == 1) {
                GameObject roomA = leftSection.getTopRightRoom();
                GameObject roomB = rightSection.getTopLeftRoom();
                buildNewHallway(roomA, roomB, wallPiece);
            }else {
                GameObject roomA = leftSection.getBottomRightRoom();
                GameObject roomB = rightSection.getBottomLeftRoom();
                buildNewHallway(roomA, roomB, wallPiece);
            }
        }
    }

    public void buildNewHallway(GameObject roomA, GameObject roomB, GameObject wallPiece) {
        float xPosA = roomA.transform.position.x;
        float xPosB = roomB.transform.position.x;
        float zPosA = roomA.transform.position.z;
        float zPosB = roomB.transform.position.z;
        float dX = xPosB - xPosA;
        float dZ = zPosB - zPosA;

        //keeps track of when to keep a torch
        int torchNum = 1;
        //places a torch every ___ hallway pieces
        int torchDensity = 8;

        //start and end positions for the hallway
        float startX;
        float startZ;
        float endX;
        float endZ;

        //used for tracking position of the created hallway so far
        float currentX;
        float currentZ;

        startX = xPosA;
        startZ = zPosA;
        currentX = startX;
        currentZ = startZ;
        endX = startX;
        endZ = startZ;

        if(dZ > -dX && dZ >= dX) {
            //north side of room A
            startZ += roomA.transform.Find("Ground").transform.localScale.z / 2.0f + 1.0f;
            currentZ = startZ;
            endX = xPosB;
            endZ = zPosB - roomB.transform.Find("Ground").transform.localScale.z / 2.0f - 1.0f;

        }else if(dZ < dX && dZ >= -dX) {
            //east side of room A
            startX += roomA.transform.Find("Ground").transform.localScale.x / 2.0f + 1.0f;
            currentX = startX;
            endX = xPosB - roomB.transform.Find("Ground").transform.localScale.x / 2.0f - 1.0f;
            endZ = zPosB;
        }else {
            if(dX == 0 && dZ == 0){
                return;
            }
            buildNewHallway(roomB, roomA, wallPiece);
            return;
        }

        //verticall hallway
        float direction = endZ > currentZ ? 1 : -1;
        while(direction * (endZ - currentZ) > 0) {
            GameObject hallwayPiece = Instantiate(wallPiece, new Vector3(currentX , 0, currentZ), Quaternion.identity) as GameObject;
            currentZ += (dZ > 0 ? 2 : -2);
            if(torchNum != torchDensity) {
                destroyTorches(hallwayPiece);
            }
            torchNum++;
            if(torchNum > torchDensity) {
                torchNum -= torchDensity;
            }
        }
        if(direction * (endZ - currentZ) == -1) {
            currentZ += (dZ > 0 ? -1 : 1);
            GameObject hallwayPiece = Instantiate(wallPiece, new Vector3(currentX , 0, currentZ), Quaternion.identity) as GameObject;
            if(torchNum != torchDensity) {
                destroyTorches(hallwayPiece);
            }
            torchNum++;
            if(torchNum > torchDensity) {
                torchNum -= torchDensity;
            }
        }

        //horizontal hallway
        direction = endX > currentX ? 1 : -1;
        while(direction * (endX - currentX) >= 0) {
            GameObject hallwayPiece = Instantiate(wallPiece, new Vector3(currentX , 0, currentZ), Quaternion.identity) as GameObject;
            currentX += (dX > 0 ? 2 : -2);
            if(torchNum != torchDensity) {
                destroyTorches(hallwayPiece);
            }
            torchNum++;
            if(torchNum > torchDensity) {
                torchNum -= torchDensity;
            }
        }
        if(direction * (endX - currentX) == -1) {
            currentX += (dX > 0 ? -1 : 1);
            GameObject hallwayPiece = Instantiate(wallPiece, new Vector3(currentX , 0, currentZ), Quaternion.identity) as GameObject;
            if(torchNum != torchDensity) {
                destroyTorches(hallwayPiece);
            }
            torchNum++;
            if(torchNum > torchDensity) {
                torchNum -= torchDensity;
            }
        }
    }



    public GameObject getTopLeftRoom() {
        if(leftSection.height == -1 && rightSection.height == -1) {
            return room;
        }else {
            return leftSection.getTopLeftRoom();
        }
    }
    public GameObject getTopRightRoom() {
        if(leftSection.height == -1 || rightSection.height == -1) {
            return room;
        }else {
            if(leftSection.x < rightSection.x) return rightSection.getTopRightRoom();
            else return leftSection.getTopRightRoom();
        }
    }
    public GameObject getBottomLeftRoom() {
        if(leftSection.height == -1 || rightSection.height == -1) {
            return room;
        }else {
            if(leftSection.y > rightSection.y) return rightSection.getBottomLeftRoom();
            else return leftSection.getBottomLeftRoom();
        }
    }
    public GameObject getBottomRightRoom() {
        if(leftSection.height == -1 || rightSection.height == -1) {
            return room;
        }else {
            return rightSection.getBottomRightRoom();
        }
    }

    public void destroyTorches(GameObject hallway) {
        Destroy(hallway.transform.Find("NorthTorch").gameObject);
         Destroy(hallway.transform.Find("SouthTorch").gameObject);
        Destroy(hallway.transform.Find("EastTorch").gameObject);
        Destroy(hallway.transform.Find("WestTorch").gameObject);
    }
    
}
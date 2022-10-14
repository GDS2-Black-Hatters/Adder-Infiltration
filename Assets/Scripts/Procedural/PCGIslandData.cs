using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Island", fileName = "NewProceduralIslandData")]
public class PCGIslandData : PCGeneratableSO
{
    [Header("General Prefabs")]
    [SerializeField] private GameObject roadwayGroundPrefab;
    [SerializeField] private AINode aiNodePrefab;

    protected Vector2Int islandSize;
    protected int maxChunkSizeLowerBound = 9;
    protected int expectedChunkSizeVariation = 10;

    [Header("Island Chunks")]
    public PCGChunkDataBase[] requiredChunks;
    public PCGChunkDataBase[] availableFreeUseChunks;

    public void Initilize(Vector2Int islandSize, int maxChunkSizeLowerBound, int expectedChunkSizeVariation)
    {
        this.islandSize = islandSize;
        this.maxChunkSizeLowerBound = maxChunkSizeLowerBound;
        this.expectedChunkSizeVariation = expectedChunkSizeVariation;
    }

    public override GameObject Generate(Transform parentTransform)
    {
        //Create root
        Transform root = new GameObject(name).transform;

        //Instantiate ground
        GameObject ground = Instantiate(roadwayGroundPrefab, root.position, root.rotation, root);
        //Add 1.5 to generate boarder ground area for enemies patrol
        float calculatedMult = GlobalConst.chunkCellSizeUnitMultiplier * 0.1f;
        Vector3 scale = new Vector3(islandSize.x + 1.5f, 1, islandSize.y + 1.5f) * calculatedMult;
        scale.y = 1;
        ground.transform.localScale = scale;

        //Generate chunks via binary partition
        ChunkTransform[] chunkTransforms = BinaryArrayPartition.GetPartitionedChunks(islandSize.x, islandSize.y, () => { return Mathf.FloorToInt(AdvanceRandom.ExponentialRandom(maxChunkSizeLowerBound, expectedChunkSizeVariation)); });

        //Grab all requiredChunks before generation and add them in first.
        List<ChunkTransform> filledChunks = new();
        List<ChunkTransform> usableChunks = new();
        foreach (PCGChunkDataBase requiredChunk in requiredChunks)
        {
            usableChunks.Clear();
            foreach (ChunkTransform chunkTransform in chunkTransforms) //check through each transform to see if they are usable
            {
                if(requiredChunk.CanGenerateInTransform(chunkTransform) && !filledChunks.Contains(chunkTransform))
                {
                    usableChunks.Add(chunkTransform);
                }
            }

            if(usableChunks.Count <= 0) Debug.LogError("No valid usable chunk for a required chunk, generation parameter might be too strict.");

            ChunkTransform useTransform = usableChunks[Random.Range(0, usableChunks.Count)]; //select a random usable one

            filledChunks.Add(useTransform);
            GenerateChunk(requiredChunk, useTransform, root);
        }

        //Fill in the rest of the chunks.
        List<PCGChunkDataBase> usableChunkData = new();
        foreach (ChunkTransform chunkTransform in chunkTransforms)
        {
            if(filledChunks.Contains(chunkTransform))
            {
                continue; //Skip if chunk is already filled.
            }

            usableChunkData.Clear();
            foreach(PCGChunkDataBase availableChunk in availableFreeUseChunks) //filter out unusable data
            {
                if(availableChunk.CanGenerateInTransform(chunkTransform))
                {
                    usableChunkData.Add(availableChunk);
                }
            }

            if (usableChunkData.Count <= 0)
            {
                Debug.LogError("No usable chunkdata for a chunk, consider adding in chunks with no generation requirements.");
                continue;
            }

            GenerateChunk(usableChunkData[Random.Range(0, usableChunkData.Count)], chunkTransform, root);
        }

#region AINodeGen
        //Generate nodes for AI navigation.
        //float aiNodeGenStartTime = Time.realtimeSinceStartup;
        NodeNeighbour GetOrCreateNode(ref Dictionary<Vector2Int, NodeNeighbour> nodeDict, Vector2Int coordV2Int)
        {
            if(!nodeDict.ContainsKey(coordV2Int))
            {
                nodeDict.Add(coordV2Int, new(Instantiate<AINode>(aiNodePrefab, root.TransformPoint(GridPosToV3(coordV2Int)), Quaternion.identity)));
            }
            return nodeDict[coordV2Int];
        } 
        
        //Prep dictionary
        Dictionary<Vector2Int, NodeNeighbour> aiNavNodes = new();
        
        //Create a parent object for ai nodes
        Transform aiNodeParent = new GameObject("AINodes").transform;
        aiNodeParent.SetParent(root);
        aiNodeParent.localPosition = Vector3.zero;

        //Create corner nodes for every transform
        foreach (ChunkTransform chunkTransform in chunkTransforms)
        {
            //Generate nodes at corners of chunks
            NodeNeighbour[] chunkNodes = new NodeNeighbour[4];
            chunkNodes[0] = GetOrCreateNode(ref aiNavNodes, chunkTransform.upperLeft - Vector2Int.one);
            chunkNodes[0].nodeSelf.transform.SetParent(aiNodeParent);
            chunkNodes[1] = GetOrCreateNode(ref aiNavNodes, new(chunkTransform.bottomRight.x + 1, chunkTransform.upperLeft.y - 1));
            chunkNodes[1].nodeSelf.transform.SetParent(aiNodeParent);
            chunkNodes[2] = GetOrCreateNode(ref aiNavNodes, chunkTransform.bottomRight + Vector2Int.one);
            chunkNodes[2].nodeSelf.transform.SetParent(aiNodeParent);
            chunkNodes[3] = GetOrCreateNode(ref aiNavNodes, new(chunkTransform.upperLeft.x - 1, chunkTransform.bottomRight.y + 1));
            chunkNodes[3].nodeSelf.transform.SetParent(aiNodeParent);

            //set node neighbours
            chunkNodes[0].updateNeighbour(NodeNeighbour.Direction.right, chunkNodes[1]);
            chunkNodes[0].updateNeighbour(NodeNeighbour.Direction.down, chunkNodes[3]);
            chunkNodes[1].updateNeighbour(NodeNeighbour.Direction.left, chunkNodes[0]);
            chunkNodes[1].updateNeighbour(NodeNeighbour.Direction.down, chunkNodes[2]);
            chunkNodes[2].updateNeighbour(NodeNeighbour.Direction.up, chunkNodes[1]);
            chunkNodes[2].updateNeighbour(NodeNeighbour.Direction.left, chunkNodes[3]);
            chunkNodes[3].updateNeighbour(NodeNeighbour.Direction.right, chunkNodes[2]);
            chunkNodes[3].updateNeighbour(NodeNeighbour.Direction.up, chunkNodes[0]);
        }

        //Convert the node network to a simpler form
        List<AINode> nodeList = new();
        foreach (NodeNeighbour nb in aiNavNodes.Values)
        {
            //apply the neighbours, null check is done to prevent errors
            nb.nodeSelf.AddNeighbour(nb.nodeUp?.nodeSelf);            
            nb.nodeSelf.AddNeighbour(nb.nodeDown?.nodeSelf);            
            nb.nodeSelf.AddNeighbour(nb.nodeLeft?.nodeSelf);            
            nb.nodeSelf.AddNeighbour(nb.nodeRight?.nodeSelf);            
        
            nodeList.Add(nb.nodeSelf);
        }
#endregion

        root.SetParent(parentTransform);
        root.localPosition = Vector3.zero;
        root.localRotation = Quaternion.identity;
        return root.gameObject;
    }

    private void GenerateChunk(PCGChunkDataBase chunkData, ChunkTransform chunkTransform, Transform rootTransform)
    {
            //create a copy of chunk data so we don't modify the scriptable object on disk
            PCGChunkDataBase chunkDataCopy = Instantiate(chunkData);

            //Initilize and generate chunk
            chunkDataCopy.Initilize(chunkTransform);
            GameObject chunkObj = chunkDataCopy.Generate(rootTransform);
            
            //move chunk to new position
            chunkObj.transform.localPosition = GridPosToV3(chunkTransform.ChunkCenter);
            chunkObj.transform.localRotation = Quaternion.identity;
    }

    private Vector3 GridPosToV3(Vector2 gridCord)
    {
        return new Vector3(gridCord.x - (islandSize.x - 1) * 0.5f, 0, gridCord.y - (islandSize.y - 1) * 0.5f) * GlobalConst.chunkCellSizeUnitMultiplier;
    }

    private class IslandChunk
    {
        public ChunkTransform chunkTransform;
        public PCGChunkDataBase chunkData;
    }

    private class NodeNeighbour
    {
        public enum Direction
        { up = 1, down = -1, left = 2, right = -2}

        public NodeNeighbour(AINode selfNode)
        {
            this.nodeSelf = selfNode;
        }

        public AINode nodeSelf;

        public NodeNeighbour nodeUp;
        public NodeNeighbour nodeDown;
        public NodeNeighbour nodeLeft;
        public NodeNeighbour nodeRight;
    
        public void updateNeighbour(Direction direction, NodeNeighbour newNeighbour)
        {
            switch (direction)
            {
                case Direction.up:
                    updateNeighbour(direction, ref nodeUp, newNeighbour);
                    return;
                case Direction.down:
                    updateNeighbour(direction, ref nodeDown, newNeighbour);
                    return;
                case Direction.left:
                    updateNeighbour(direction, ref nodeLeft, newNeighbour);
                    return;
                case Direction.right:
                    updateNeighbour(direction, ref nodeRight, newNeighbour);
                    return;
                
                default:
                    return;
            }
        }

        private void updateNeighbour(Direction direction, ref NodeNeighbour originalNeighbour, NodeNeighbour newNeighbour)
        {
            if(originalNeighbour == null)
            {
                originalNeighbour = newNeighbour;
                originalNeighbour.updateNeighbour((Direction)(-(int)direction), this); //inform neighbour of potential changes in the graph.
                return;
            }
            if(originalNeighbour == newNeighbour)
            {
                return;
            }
            //if the original neighbour is closer than the new neighbour, update their neighbour instead.
            if(Vector3.Distance(nodeSelf.transform.position, originalNeighbour.nodeSelf.transform.position) < Vector3.Distance(nodeSelf.transform.position, newNeighbour.nodeSelf.transform.position))
            {
                originalNeighbour.updateNeighbour(direction, newNeighbour);
                newNeighbour.updateNeighbour((Direction)(-(int)direction), originalNeighbour); //update the new neighbour as well to inform it about a better neighbour.
            }
            else //if the original is furthur away, tell it to update it's opposite direction.
            {
                NodeNeighbour prevOriginalNeighbour = originalNeighbour; //infinite loop if this is not done
                originalNeighbour = newNeighbour;
                originalNeighbour.updateNeighbour(direction, prevOriginalNeighbour);
                prevOriginalNeighbour.updateNeighbour((Direction)(-(int)direction), newNeighbour);
            }
        }
    }
}

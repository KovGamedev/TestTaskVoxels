using UnityEngine;

namespace VoxelDestruction
{
    public struct VoxelData
    {
        public Vector3Int Size;
        public Voxel[] Blocks;

        public VoxelData(Vector3Int length, Voxel[] blocks)
        {
            Size = length;

            Blocks = blocks;
        }

        public int[] GetVoxels()
        {
            int[] v = new int[Blocks.Length];

            for (int i = 0; i < v.Length; i++)
                v[i] = Blocks[i].active ? 1 : 0;

            return v;
        }
    }   
}
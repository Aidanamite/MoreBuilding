using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BlockCreator;
using static MoreBuilding.Main;


namespace MoreBuilding
{
    public static class GeneratedMeshes
    {
        public static Mesh Empty;
        public static Mesh Foundation;
        public static Mesh FoundationTriangle;
        public static Mesh FoundationTriangleMirrored;
        public static Mesh Floor;
        public static Mesh FloorTriangle;
        public static Mesh FloorTriangleMirrored;
        public static Mesh FloorHalf;
        public static Mesh FloorHalfTriangle;
        public static Mesh FloorHalfTriangleMirrored;
        public static Mesh Wall;
        public static Mesh WallDiagonal;
        public static Mesh WallHalf;
        public static Mesh WallHalfDiagonal;
        public static Mesh WallV;
        public static Mesh WallSlope;
        public static Mesh WallSlopeInverted;
        public static Mesh Fence;
        public static Mesh FenceDiagonal;
        public static Mesh FenceConnector;
        public static Mesh Gate;
        public static Mesh GateDiagonal;
        public static Mesh Door;
        public static Mesh DoorDiagonal;
        public static Mesh Window;
        public static Mesh WindowDiagonal;
        public static Mesh WindowHalf;
        public static Mesh WindowHalfDiagonal;
        public static Mesh Roof;
        public static Mesh RoofDiagonal;
        public static Mesh RoofDiagonalAlt;
        public static Mesh RoofCorner;
        public static Mesh RoofCornerInverted;
        public static Mesh RoofV0;
        public static Mesh RoofV1;
        public static Mesh RoofV2I;
        public static Mesh RoofV2L;
        public static Mesh RoofV3;
        public static Mesh RoofV4;
        public static Mesh Pillar;
        public static Mesh PillarHalf;
        public static Mesh PillarHorizontal;
        public static Mesh PillarHorizontalHalf;
        static GeneratedMeshes()
        {
            createdObjects.Add(Empty = new MeshBuilder().ToMesh("Empty"));
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize,-HalfFloorHeight * 2 / 3,-HalfBlockSize), new Vector3(HalfBlockSize,0,HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1),
                    (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1)
                    );
                createdObjects.Add(Foundation = builder.ToMesh("Foundation"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -HalfFloorHeight * 2 / 3, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1),
                    (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1),
                    slice: (null, Axis.Z,Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -HalfFloorHeight * 2 / 3, -HalfBlockSize), new Vector3(0, 0, 0),
                    slice: ((0, 0.3333333f, 0.9f, 1), Axis.Z, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(0, -HalfFloorHeight * 2 / 3, 0), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    slice: ((0, 0.3333333f, 0.9f, 1), Axis.Z, Axis.NX)
                    );
                createdObjects.Add(FoundationTriangle = builder.ToMesh("FoundationTriangle"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -HalfFloorHeight * 2 / 3, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1),
                    (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1),
                    slice: (null, Axis.NZ, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -HalfFloorHeight * 2 / 3, 0), new Vector3(0, 0, HalfBlockSize),
                    slice: ((0, 0.3333333f, 0.9f, 1), Axis.NZ, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(0, -HalfFloorHeight * 2 / 3, -HalfBlockSize), new Vector3(HalfBlockSize, 0, 0),
                    slice: ((0, 0.3333333f, 0.9f, 1), Axis.NZ, Axis.NX)
                    );
                createdObjects.Add(FoundationTriangleMirrored = builder.ToMesh("FoundationTriangleMirrored"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.2f, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90)
                    );
                createdObjects.Add(Floor = builder.ToMesh("Floor"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.2f, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    slice: ((0, 0, 1, .1f), Axis.Z, Axis.NX)
                    );
                createdObjects.Add(FloorTriangle = builder.ToMesh("FloorTriangle"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.2f, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    slice: ((0, 0, 1, .1f), Axis.NZ, Axis.NX)
                    );
                createdObjects.Add(FloorTriangleMirrored = builder.ToMesh("FloorTriangleMirrored"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(HalfBlockSize, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2)
                    );
                createdObjects.Add(Wall = builder.ToMesh("Wall"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, -0.01f, -0.06f), new Vector3(0, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0, 0, 0.9f, 2), null,
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2)
                    );
                builder.AddBox(
                    new Vector3(0, -0.01f, -0.06f), new Vector3(DiagonalHalfBlockSize, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    (0, 0, 0.9f, 2), null
                    );
                createdObjects.Add(WallDiagonal = builder.ToMesh("WallDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(HalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1)
                    );
                createdObjects.Add(WallHalf = builder.ToMesh("WallHalf"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, -0.01f, -0.06f), new Vector3(0, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0, 0, 0.9f, 1), null,
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(0, -0.01f, -0.06f), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), null
                    );
                createdObjects.Add(WallHalfDiagonal = builder.ToMesh("WallHalfDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(0, HalfFloorHeight / 2 - 0.01f, 0.06f),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0.45f, 0, 0.9f, 0.5f), null,
                    (0, 0, 0.45f, 0.5f), (0.45f, 0, 1, 0.5f),
                    slice: ((1, 1, 0.9f, 0.5f), Axis.Y, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(0, -0.01f, -0.06f), new Vector3(HalfBlockSize, HalfFloorHeight / 2 - 0.01f, 0.06f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.45f, 0.5f), (0.45f, 0, 1, 0.5f),
                    (0.45f, 0, 0.9f, 0.5f), null,
                    slice: ((0.9f, 0, 1, 0.5f), Axis.Y, Axis.X)
                    );
                createdObjects.Add(WallV = builder.ToMesh("WallV"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(HalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    slice: ((0.9f, 0, 1, 1), Axis.Y, Axis.X)
                    );
                createdObjects.Add(WallSlope = builder.ToMesh("WallSlope"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, 0.01f, -0.06f), new Vector3(HalfBlockSize, HalfFloorHeight + 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    slice: ((0.9f, 0, 1, 1), Axis.Y, Axis.X)
                    );
                createdObjects.Add(WallSlopeInverted = builder.ToMesh("WallSlopeInverted"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, HalfFloorHeight / 4 - 0.01f, -0.04f), new Vector3(HalfBlockSize, HalfFloorHeight * 7 / 12 - 0.01f, 0.04f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), null,
                    (0, 0.6666666f, 0.9f, 1), null
                    );
                builder.AddBox(
                    new Vector3(-0.05f - HalfBlockSize, -0.01f, -0.05f), new Vector3(0.05f - HalfBlockSize, HalfFloorHeight / 3 * 2 - 0.01f, 0.05f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.05f, -0.01f, -0.05f), new Vector3(HalfBlockSize + 0.05f, HalfFloorHeight / 3 * 2 - 0.01f, 0.05f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                createdObjects.Add(Fence = builder.ToMesh("Fence"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, HalfFloorHeight / 4 - 0.01f, -0.04f), new Vector3(0, HalfFloorHeight * 7 / 12 - 0.01f, 0.04f),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0, 0.6666666f, 0.9f, 1), null,
                    (0, 0.6666666f, 0.9f, 1), null
                    );
                builder.AddBox(
                    new Vector3(0, HalfFloorHeight / 4 - 0.01f, -0.04f), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight * 7 / 12 - 0.01f, 0.04f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), null,
                    (0, 0.6666666f, 0.9f, 1), null
                    );
                builder.AddBox(
                    new Vector3(-0.05f - DiagonalHalfBlockSize, -0.01f, -0.05f), new Vector3(0.05f - DiagonalHalfBlockSize, HalfFloorHeight / 3 * 2 - 0.01f, 0.05f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.05f, -0.01f, -0.05f), new Vector3(DiagonalHalfBlockSize + 0.05f, HalfFloorHeight / 3 * 2 - 0.01f, 0.05f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                createdObjects.Add(FenceDiagonal = builder.ToMesh("FenceDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-0.08f, -0.011f, -0.08f), new Vector3(0.08f, HalfFloorHeight / 3 * 2 - 0.009f, 0.08f),
                    null, null,
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.3333333f, 0, 0.6666666f, 1.9f),
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.3333333f, 0, 0.6666666f, 1.9f),
                    modifyUV: (x, y) => x.Rotate(90)
                    );
                builder.AddBox(
                    new Vector3(-0.08f, -0.011f, -0.08f), new Vector3(0.08f, HalfFloorHeight / 3 * 2 - 0.009f, 0),
                    (0.75f, 0.6666666f, 0.9f, 0.3333333f), (0.75f, 0.6666666f, 0.9f, 0.3333333f)
                    );
                builder.AddBox(
                    new Vector3(-0.08f, -0.011f, 0), new Vector3(0.08f, HalfFloorHeight / 3 * 2 - 0.009f, 0.08f),
                    (0, 0.3333333f, 0.15f, 0.6666666f), (0, 0.3333333f, 0.15f, 0.6666666f)
                    );
                createdObjects.Add(FenceConnector = builder.ToMesh("FenceConnector"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, HalfFloorHeight / 12 - 0.01f, -0.06f), new Vector3(0, HalfFloorHeight - 0.011f, 0.06f),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0.45f, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.45f, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, HalfFloorHeight / 12 - 0.01f, -0.06f), new Vector3(HalfBlockSize- 0.1f , HalfFloorHeight - 0.011f, 0.06f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.45f, 1), (0.9f, 0, 1, 1),
                    (0.45f, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(0.1f - HalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.1f, -0.01f, -0.06f), new Vector3(HalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                createdObjects.Add(Gate = builder.ToMesh("Gate"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(0.1f - DiagonalHalfBlockSize, HalfFloorHeight / 12 - 0.01f, -0.06f), new Vector3(0, HalfFloorHeight - 0.011f, 0.06f),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, HalfFloorHeight / 12 - 0.01f, -0.06f), new Vector3(DiagonalHalfBlockSize - 0.1f, HalfFloorHeight - 0.011f, 0.06f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, -0.01f, -0.06f), new Vector3(0.1f - DiagonalHalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.1f, -0.01f, -0.06f), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                createdObjects.Add(GateDiagonal = builder.ToMesh("GateDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, 0.04f, -0.06f), new Vector3(0, FullFloorHeight - 0.06f, 0.06f),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0.45f, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    (0, 0, 0.45f, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, 0.04f, -0.06f), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight - 0.06f, 0.06f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.45f, 2), (0.9f, 0, 1, 2),
                    (0.45f, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(0.1f - HalfBlockSize, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.1f, -0.01f, -0.06f), new Vector3(HalfBlockSize, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, -0.01f, -0.06f), new Vector3(HalfBlockSize - 0.1f, 0.04f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.9f, 1, 1), null,
                    (0, 0.9f, 1, 1), null,
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x,
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, FullFloorHeight - 0.06f, -0.06f), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.9f, 1, 1), null,
                    (0, 0.9f, 1, 1), null,
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x,
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                createdObjects.Add(Door = builder.ToMesh("Door"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, 0.04f, -0.06f), new Vector3(0, FullFloorHeight - 0.06f, 0.06f),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0.45f, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    (0, 0, 0.45f, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, 0.04f, -0.06f), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight - 0.06f, 0.06f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.45f, 2), (0.9f, 0, 1, 2),
                    (0.45f, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, -0.01f, -0.06f), new Vector3(0.1f - HalfBlockSize, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.9f, 0, 1, 2),
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.9f, 0, 1, 2),
                    modifyUV: (x,y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x,
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.1f, -0.01f, -0.06f), new Vector3(DiagonalHalfBlockSize, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.9f, 0, 1, 2),
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.9f, 0, 1, 2),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x,
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, -0.01f, -0.06f), new Vector3(HalfBlockSize - 0.1f, 0.04f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.9f, 1, 1), null,
                    (0, 0.9f, 1, 1), null,
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x,
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, FullFloorHeight - 0.06f, -0.06f), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.9f, 1, 1), null,
                    (0, 0.9f, 1, 1), null,
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x,
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                createdObjects.Add(DoorDiagonal = builder.ToMesh("DoorDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(HalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, FullFloorHeight / 6 * 5 - 0.01f, -0.06f), new Vector3(HalfBlockSize, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), (0.9f, 0.6666666f, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), (0.9f, 0.6666666f, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, HalfFloorHeight - 0.01f, -0.06f), new Vector3(0.05f - HalfBlockSize, FullFloorHeight / 6 * 5 - 0.01f, 0.06f),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f)
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.05f, HalfFloorHeight - 0.01f, -0.06f), new Vector3(HalfBlockSize, FullFloorHeight / 6 * 5 - 0.01f, 0.06f),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f)
                    );
                createdObjects.Add(Window = builder.ToMesh("Window"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, -0.01f, -0.06f), new Vector3(0, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), null,
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(0, -0.01f, -0.06f), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), null
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, FullFloorHeight / 6 * 5 - 0.01f, -0.06f), new Vector3(0, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), null,
                    (0, 0.6666666f, 0.9f, 1), (0.9f, 0.6666666f, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(0, FullFloorHeight / 6 * 5 - 0.01f, -0.06f), new Vector3(DiagonalHalfBlockSize, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), (0.9f, 0.6666666f, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), null
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, HalfFloorHeight - 0.01f, -0.06f), new Vector3(0.05f - DiagonalHalfBlockSize, FullFloorHeight / 6 * 5 - 0.01f, 0.06f),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f)
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.05f, HalfFloorHeight - 0.01f, -0.06f), new Vector3(DiagonalHalfBlockSize, FullFloorHeight / 6 * 5 - 0.01f, 0.06f),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f)
                    );
                createdObjects.Add(WindowDiagonal = builder.ToMesh("WindowDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(0.2f - HalfBlockSize, HalfFloorHeight / 2 - 0.01f, 0.06f),
                    null, (0.9f, 1 - (0.2f / BlockSize), 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, HalfFloorHeight / 2 - 0.01f, -0.06f), new Vector3(0.2f - HalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 0.2f / BlockSize), null,
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.2f, -0.01f, -0.06f), new Vector3(HalfBlockSize, HalfFloorHeight / 2 - 0.01f, 0.06f),
                    null, (0.9f, 0, 1, 0.2f / BlockSize),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.2f, HalfFloorHeight / 2 - 0.01f, -0.06f), new Vector3(HalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 1 - (0.2f / BlockSize), 1, 1), null,
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(0.2f - HalfBlockSize, -0.01f, -0.06f), new Vector3(HalfBlockSize - 0.2f, 0.29f, 0.06f),
                    (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize)), (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                builder.AddBox(
                    new Vector3(0.2f - HalfBlockSize, HalfFloorHeight - 0.31f, -0.06f), new Vector3(HalfBlockSize - 0.2f, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize)), (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                createdObjects.Add(WindowHalf = builder.ToMesh("WindowHalf"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, -0.01f, -0.06f), new Vector3(0.2f - DiagonalHalfBlockSize, HalfFloorHeight / 2 - 0.01f, 0.06f),
                    null, (0.9f, 1 - (0.2f / DiagonalBlockSize), 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, HalfFloorHeight / 2 - 0.01f, -0.06f), new Vector3(0.2f - DiagonalHalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 0.2f / DiagonalBlockSize), null,
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.2f, -0.01f, -0.06f), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight / 2 - 0.01f, 0.06f),
                    null, (0.9f, 0, 1, 0.2f / DiagonalBlockSize),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.2f, HalfFloorHeight / 2 - 0.01f, -0.06f), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 1 - (0.2f / DiagonalBlockSize), 1, 1), null,
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(0.2f - DiagonalHalfBlockSize, -0.01f, -0.06f), new Vector3(0, 0.29f, 0.06f),
                    (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)), (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                builder.AddBox(
                    new Vector3(0, -0.01f, -0.06f), new Vector3(DiagonalHalfBlockSize - 0.2f, 0.29f, 0.06f),
                    (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)), (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                builder.AddBox(
                    new Vector3(0.2f - DiagonalHalfBlockSize, HalfFloorHeight - 0.31f, -0.06f), new Vector3(0, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)), (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                builder.AddBox(
                    new Vector3(0, HalfFloorHeight - 0.31f, -0.06f), new Vector3(DiagonalHalfBlockSize - 0.2f, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)), (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                createdObjects.Add(WindowHalfDiagonal = builder.ToMesh("WindowHalfDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.2f, 0), new Vector3(HalfBlockSize, 0, BlockSize),
                    (0, 0, 1, 0.9f), (0, 0, 1, 0.9f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyVert: x  => new  Vector3(x.x, x.y + (x.z / BlockSize * HalfFloorHeight), x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90)
                    );
                createdObjects.Add(Roof = builder.ToMesh("Roof"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.2f, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyVert: x => new Vector3(x.x, x.y + (new Vector2(x.z / DiagonalHalfBlockSize, x.x / DiagonalHalfBlockSize).Magnitude(Vector2.one) * HalfFloorHeight), x.z).Rotate(0,135,0),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.ModifyAround(new Vector2(0.45f, 0.5f), z => (z / new Vector2(0.9f, 1) / DiagonalMagnitude).Rotate(y.IsNegative() ? 135 : 45) * new Vector2(0.9f, 2)) + new Vector2(0,0.5f) : x.Rotate(-90),
                    slice: ((0, 0, 1, .1f), Axis.NX,Axis.NZ)
                    );
                createdObjects.Add(RoofDiagonal = builder.ToMesh("RoofDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, -0.2f, -DiagonalHalfBlockSize), new Vector3(0, 0, 0),
                    (0, 0.45f, 1, 0.9f), (0, 0.45f, 1, 0.9f),
                    (0, 0, 1, .1f), null,
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyVert: x => new Vector3(x.x, x.y +( -x.z / DiagonalHalfBlockSize * HalfFloorHeight), x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                    slice: ((0, 0, 1, .1f), Axis.NX, Axis.Z)
                    );
                builder.AddBox(
                    new Vector3(0, -0.2f, -DiagonalHalfBlockSize), new Vector3(DiagonalHalfBlockSize, 0, 0),
                    (0, 0, 1, 0.45f), (0, 0, 1, 0.45f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), null,
                    modifyVert: x => new Vector3(x.x, x.y + (-x.z / DiagonalHalfBlockSize * HalfFloorHeight), x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                    slice: ((0, 0, 1, .1f), Axis.X, Axis.Z)
                    );
                createdObjects.Add(RoofDiagonalAlt = builder.ToMesh("RoofDiagonalAlt"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.2f, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 1, 0.9f), (0, 0, 1, 0.9f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyVert: x=> new Vector3(x.x, x.y + Mathf.Min(-x.x / BlockSize + 0.5f, x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                    slice: (null,Axis.X,Axis.Z)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.2f, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyVert: x => new Vector3(x.x, x.y + Mathf.Min(-x.x / BlockSize + 0.5f, x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    slice: (null, Axis.NX, Axis.NZ), uniqueOffset: 3
                    );
                createdObjects.Add(RoofCorner = builder.ToMesh("RoofCorner"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.2f, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyVert: x => new Vector3(x.x, x.y + Mathf.Max(x.x / BlockSize + 0.5f, x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    slice: (null, Axis.NX, Axis.Z)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.2f, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 1, 0.9f), (0, 0, 1, 0.9f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyVert: x => new Vector3(x.x, x.y + Mathf.Max(x.x / BlockSize + 0.5f, x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                    slice: (null, Axis.X, Axis.NZ), uniqueOffset: 3
                    );
                createdObjects.Add(RoofCornerInverted = builder.ToMesh("RoofCornerInverted"));
            }
        }

        public static List<Vector2> UV(this (float minX, float minY, float maxX, float maxY) face, Func<Vector2, Vector2> modifyUV) => new List<Vector2>
        {
            modifyUV(new Vector2(face.maxX, face.maxY)),
            modifyUV(new Vector2(face.maxX, face.minY)),
            modifyUV(new Vector2(face.minX, face.minY)),
            modifyUV(new Vector2(face.minX, face.maxY))
        };

        public static Axis[][] GetFacePoints(Axis direction)
        {
            var p = new Axis[4][];
            for (int i = 0; i < p.Length; i++)
            {
                var a = new[] { Axis.X, Axis.Y, Axis.Z };
                a[(int)direction.ToPositive()] = direction;
                var f = (int)direction.ToPositive().Next(direction.ToPositive() == Axis.X ? 2 : 1);
                if (direction.IsNegative() ^ (direction.ToPositive() == Axis.X))
                    a[f] = a[f].Opposite();
                var l = (int)direction.ToPositive().Next(direction.ToPositive() == Axis.X ? 1 : 2);
                if (i < 2)
                    a[f] = a[f].Opposite();
                if (0 < i && i < 3)
                    a[l] = a[l].Opposite();
                p[i] = a;
            }
            return p;
        }
        public static void AddBox(this MeshBuilder builder, Vector3 min, Vector3 max, (float minX, float minY, float maxX, float maxY)? top = null, (float minX, float minY, float maxX, float maxY)? bottom = null, (float minX, float minY, float maxX, float maxY)? north = null, (float minX, float minY, float maxX, float maxY)? east = null, (float minX, float minY, float maxX, float maxY)? south = null, (float minX, float minY, float maxX, float maxY)? west = null, Func<Vector3,Vector3> modifyVert = null, Func<Vector2, Axis, Vector2> modifyUV = null, ((float minX, float minY, float maxX, float maxY)? face, Axis axis1, Axis axis2)? slice = null, Func<Axis[],IEnumerable<Vertex.Weight>> getBoneWeights = null, int uniqueOffset = 0)
        {
            //var log = "";
            if (modifyVert == null)
                modifyVert = x => x;
            if (modifyUV == null)
                modifyUV = (x, y) => x;
            if (getBoneWeights == null)
                getBoneWeights = x => default;
            var slices = slice == null || slice.Value.axis1 == slice.Value.axis2 || slice.Value.axis1.Opposite() == slice.Value.axis2 ? new Axis[0] : new[] { slice.Value.axis1, slice.Value.axis2 };
            var keep = new HashSet<Axis>() { Axis.X, Axis.Y, Axis.Z, Axis.NX, Axis.NY, Axis.NZ };
            keep.RemoveWhere(slices.Contains);
            void AddFace(Axis direction, (float minX, float minY, float maxX, float maxY) face)
            {
                if (slices.Contains(direction))
                    return;
                var u = face.UV(x => modifyUV(x,direction));
                var p = new List<(Axis[] a, Vertex v)>();
                //log += "\nGetting face " + direction;
                foreach (var a in GetFacePoints(direction))
                {
                    p.Add((a, new Vertex(modifyVert(new Vector3((a[0].IsNegative() ? min : max).x, (a[1].IsNegative() ? min : max).y, (a[2].IsNegative() ? min : max).z)), u.Take(), getBoneWeights(a), unique: (int)direction.ToPositive() + uniqueOffset)));
                    //log += $"\nIncluding corner {a[0]} - {a[1]} - {a[2]} >> {p[p.Count - 1].v.Location}";
                }
                if (slice == null || slices.Contains(direction.Opposite()))
                    builder.AddSquare(p[0].v, p[1].v, p[2].v, p[3].v);
                else
                {
                    p.RemoveAll(x => slices.Contains(x.a[(int)direction.Next().ToPositive()]) && slices.Contains(x.a[(int)direction.Next(2).ToPositive()]));
                    builder.AddTriangle(p[0].v, p[1].v, p[2].v);
                }
            }
            if (top != null)
                AddFace(Axis.Y, top.Value);
            if (bottom != null)
                AddFace(Axis.NY, bottom.Value);
            if (north != null)
                AddFace(Axis.Z, north.Value);
            if (east != null)
                AddFace(Axis.X, east.Value);
            if (south != null)
                AddFace(Axis.NZ, south.Value);
            if (west != null)
                AddFace(Axis.NX, west.Value);
            if (slice?.face != null) {
                var face = slice.Value.face.Value;
                var d = slices.Contains(Axis.X) ? Axis.X : slices.Contains(Axis.Z) ? Axis.Z : slices.Contains(Axis.NX) ? Axis.NX : Axis.NZ;
                var o = slices[(Array.IndexOf(slices, d) + 1) % 2];
                var u = face.UV(x => modifyUV(x, Axis.None));
                var p = new List<(Axis[] a, Vertex v)>();
                //log += "\nGetting slice face " + d;
                foreach (var a in GetFacePoints(d))
                {
                    if (a.Contains(o))
                        a[(int)d.ToPositive()] = a[(int)d.ToPositive()].Opposite();
                    p.Add((a, new Vertex(modifyVert(new Vector3((a[0].IsNegative() ? min : max).x, (a[1].IsNegative() ? min : max).y, (a[2].IsNegative() ? min : max).z)), u.Take(), getBoneWeights(a), unique: 3 + uniqueOffset)));
                    //log += $"\nIncluding corner {a[0]} - {a[1]} - {a[2]} >> {p[p.Count - 1].v.Location}";
                }
                builder.AddSquare(p[0].v, p[1].v, p[2].v, p[3].v);
            }
            //Debug.Log(log);
        }
    }
}
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
        public const float WallOffset = -0.01f;
        public const float FloorThickness = 0.2f;
        public const float HalfWallThickness = 0.06f;
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
                    generation: (null, Axis.Z,Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -HalfFloorHeight * 2 / 3, -HalfBlockSize), new Vector3(0, 0, 0),
                    generation: ((0, 0.3333333f, 0.9f, 1), Axis.Z, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(0, -HalfFloorHeight * 2 / 3, 0), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    generation: ((0, 0.3333333f, 0.9f, 1), Axis.Z, Axis.NX)
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
                    generation: (null, Axis.NZ, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -HalfFloorHeight * 2 / 3, 0), new Vector3(0, 0, HalfBlockSize),
                    generation: ((0, 0.3333333f, 0.9f, 1), Axis.NZ, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(0, -HalfFloorHeight * 2 / 3, -HalfBlockSize), new Vector3(HalfBlockSize, 0, 0),
                    generation: ((0, 0.3333333f, 0.9f, 1), Axis.NZ, Axis.NX)
                    );
                createdObjects.Add(FoundationTriangleMirrored = builder.ToMesh("FoundationTriangleMirrored"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
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
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    generation: ((0, 0, 1, .1f), Axis.Z, Axis.NX)
                    );
                createdObjects.Add(FloorTriangle = builder.ToMesh("FloorTriangle"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    generation: ((0, 0, 1, .1f), Axis.NZ, Axis.NX)
                    );
                createdObjects.Add(FloorTriangleMirrored = builder.ToMesh("FloorTriangleMirrored"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2)
                    );
                createdObjects.Add(Wall = builder.ToMesh("Wall"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0, 0, 0.9f, 2), null,
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2)
                    );
                builder.AddBox(
                    new Vector3(0, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    (0, 0, 0.9f, 2), null
                    );
                createdObjects.Add(WallDiagonal = builder.ToMesh("WallDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1)
                    );
                createdObjects.Add(WallHalf = builder.ToMesh("WallHalf"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0, 0, 0.9f, 1), null,
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(0, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), null
                    );
                createdObjects.Add(WallHalfDiagonal = builder.ToMesh("WallHalfDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0, HalfFloorHeight / 2 + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0.45f, 0, 0.9f, 0.5f), null,
                    (0, 0, 0.45f, 0.5f), (0.45f, 0, 1, 0.5f),
                    generation: ((1, 1, 0.9f, 0.5f), Axis.Y, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(0, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight / 2 + WallOffset, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.45f, 0.5f), (0.45f, 0, 1, 0.5f),
                    (0.45f, 0, 0.9f, 0.5f), null,
                    generation: ((0.9f, 0, 1, 0.5f), Axis.Y, Axis.X)
                    );
                createdObjects.Add(WallV = builder.ToMesh("WallV"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    generation: ((0.9f, 0, 1, 1), Axis.Y, Axis.X)
                    );
                createdObjects.Add(WallSlope = builder.ToMesh("WallSlope"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, 0.01f, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight - WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    generation: ((0.9f, 0, 1, 1), Axis.Y, Axis.X)
                    );
                createdObjects.Add(WallSlopeInverted = builder.ToMesh("WallSlopeInverted"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, HalfFloorHeight / 4 + WallOffset, -0.04f), new Vector3(HalfBlockSize, HalfFloorHeight * 7 / 12 + WallOffset, 0.04f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), null,
                    (0, 0.6666666f, 0.9f, 1), null
                    );
                builder.AddBox(
                    new Vector3(-0.05f - HalfBlockSize, WallOffset, -0.05f), new Vector3(0.05f - HalfBlockSize, HalfFloorHeight / 3 * 2 + WallOffset, 0.05f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.05f, WallOffset, -0.05f), new Vector3(HalfBlockSize + 0.05f, HalfFloorHeight / 3 * 2 + WallOffset, 0.05f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                createdObjects.Add(Fence = builder.ToMesh("Fence"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, HalfFloorHeight / 4 + WallOffset, -0.04f), new Vector3(0, HalfFloorHeight * 7 / 12 + WallOffset, 0.04f),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0, 0.6666666f, 0.9f, 1), null,
                    (0, 0.6666666f, 0.9f, 1), null
                    );
                builder.AddBox(
                    new Vector3(0, HalfFloorHeight / 4 + WallOffset, -0.04f), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight * 7 / 12 + WallOffset, 0.04f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), null,
                    (0, 0.6666666f, 0.9f, 1), null
                    );
                builder.AddBox(
                    new Vector3(-0.05f - DiagonalHalfBlockSize, WallOffset, -0.05f), new Vector3(0.05f - DiagonalHalfBlockSize, HalfFloorHeight / 3 * 2 + WallOffset, 0.05f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.05f, WallOffset, -0.05f), new Vector3(DiagonalHalfBlockSize + 0.05f, HalfFloorHeight / 3 * 2 + WallOffset, 0.05f),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                createdObjects.Add(FenceDiagonal = builder.ToMesh("FenceDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-0.08f, WallOffset - 0.001f, -0.08f), new Vector3(0.08f, HalfFloorHeight / 3 * 2 + WallOffset + 0.001f, 0.08f),
                    null, null,
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.3333333f, 0, 0.6666666f, 1.9f),
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.3333333f, 0, 0.6666666f, 1.9f),
                    modifyUV: (x, y) => x.Rotate(90)
                    );
                builder.AddBox(
                    new Vector3(-0.08f, WallOffset - 0.001f, -0.08f), new Vector3(0.08f, HalfFloorHeight / 3 * 2 + WallOffset + 0.001f, 0),
                    (0.75f, 0.6666666f, 0.9f, 0.3333333f), (0.75f, 0.6666666f, 0.9f, 0.3333333f)
                    );
                builder.AddBox(
                    new Vector3(-0.08f, WallOffset - 0.001f, 0), new Vector3(0.08f, HalfFloorHeight / 3 * 2 + WallOffset + 0.001f, 0.08f),
                    (0, 0.3333333f, 0.15f, 0.6666666f), (0, 0.3333333f, 0.15f, 0.6666666f)
                    );
                createdObjects.Add(FenceConnector = builder.ToMesh("FenceConnector"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, HalfFloorHeight / 12 + WallOffset, -HalfWallThickness), new Vector3(0, HalfFloorHeight - 0.001f + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0.45f, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.45f, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, HalfFloorHeight / 12 + WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize- 0.1f , HalfFloorHeight - 0.001f + WallOffset, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.45f, 1), (0.9f, 0, 1, 1),
                    (0.45f, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.1f - HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.1f, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
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
                    new Vector3(0.1f - DiagonalHalfBlockSize, HalfFloorHeight / 12 + WallOffset, -HalfWallThickness), new Vector3(0, HalfFloorHeight - 0.001f + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, HalfFloorHeight / 12 + WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize - 0.1f, HalfFloorHeight - 0.001f + WallOffset, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.1f - DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.1f, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
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
                    new Vector3(0.1f - HalfBlockSize, 0.05f, -HalfWallThickness), new Vector3(0, FullFloorHeight - 0.05f, HalfWallThickness),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0.45f, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    (0, 0, 0.45f, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, 0.05f, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight - 0.05f, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.45f, 2), (0.9f, 0, 1, 2),
                    (0.45f, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.1f - HalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.1f, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, 0.05f, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.9f, 1, 1), null,
                    (0, 0.9f, 1, 1), null,
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x,
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, FullFloorHeight - 0.05f, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight + WallOffset, HalfWallThickness),
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
                    new Vector3(0.1f - HalfBlockSize, 0.05f, -HalfWallThickness), new Vector3(0, FullFloorHeight - 0.05f, HalfWallThickness),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0.45f, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    (0, 0, 0.45f, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, 0.05f, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight - 0.05f, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.45f, 2), (0.9f, 0, 1, 2),
                    (0.45f, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.1f - HalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.9f, 0, 1, 2),
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.9f, 0, 1, 2),
                    modifyUV: (x,y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x,
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.1f, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.9f, 0, 1, 2),
                    (0.3333333f, 0, 0.6666666f, 1.9f), (0.9f, 0, 1, 2),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x,
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, 0.05f, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.9f, 1, 1), null,
                    (0, 0.9f, 1, 1), null,
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x,
                    getBoneWeights: x => new Vertex.Weight[] { (0, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, FullFloorHeight - 0.05f, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight + WallOffset, HalfWallThickness),
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
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), (0.9f, 0.6666666f, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), (0.9f, 0.6666666f, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, HalfFloorHeight + WallOffset, -HalfWallThickness), new Vector3(0.05f - HalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, HalfWallThickness),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f)
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.05f, HalfFloorHeight + WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, HalfWallThickness),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f)
                    );
                createdObjects.Add(Window = builder.ToMesh("Window"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), null,
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(0, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), null
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, -HalfWallThickness), new Vector3(0, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), null,
                    (0, 0.6666666f, 0.9f, 1), (0.9f, 0.6666666f, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(0, FullFloorHeight / 6 * 5 + WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), (0.9f, 0.6666666f, 1, 1),
                    (0, 0.6666666f, 0.9f, 1), null
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, -HalfWallThickness), new Vector3(0.05f - DiagonalHalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, HalfWallThickness),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f)
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.05f, HalfFloorHeight + WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, HalfWallThickness),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f)
                    );
                createdObjects.Add(WindowDiagonal = builder.ToMesh("WindowDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.2f - HalfBlockSize, HalfFloorHeight / 2 + WallOffset, HalfWallThickness),
                    null, (0.9f, 1 - (0.2f / BlockSize), 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, HalfFloorHeight / 2 + WallOffset, -HalfWallThickness), new Vector3(0.2f - HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 0.2f / BlockSize), null,
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.2f, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight / 2 + WallOffset, HalfWallThickness),
                    null, (0.9f, 0, 1, 0.2f / BlockSize),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.2f, HalfFloorHeight / 2 + WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 1 - (0.2f / BlockSize), 1, 1), null,
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(0.2f - HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize - 0.2f, 0.29f, HalfWallThickness),
                    (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize)), (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                builder.AddBox(
                    new Vector3(0.2f - HalfBlockSize, HalfFloorHeight - 0.31f, -HalfWallThickness), new Vector3(HalfBlockSize - 0.2f, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize)), (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                createdObjects.Add(WindowHalf = builder.ToMesh("WindowHalf"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.2f - DiagonalHalfBlockSize, HalfFloorHeight / 2 + WallOffset, HalfWallThickness),
                    null, (0.9f, 1 - (0.2f / DiagonalBlockSize), 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, HalfFloorHeight / 2 + WallOffset, -HalfWallThickness), new Vector3(0.2f - DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 0.2f / DiagonalBlockSize), null,
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.2f, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight / 2 + WallOffset, HalfWallThickness),
                    null, (0.9f, 0, 1, 0.2f / DiagonalBlockSize),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0, 1, 0.5f),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.2f, HalfFloorHeight / 2 + WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 1 - (0.2f / DiagonalBlockSize), 1, 1), null,
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    (0.3333333f, 0, 0.6666666f, 0.9f), (0.9f, 0.5f, 1, 1),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(0.2f - DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0, 0.3f + WallOffset, HalfWallThickness),
                    (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)), (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                builder.AddBox(
                    new Vector3(0, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize - 0.2f, 0.3f + WallOffset, HalfWallThickness),
                    (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)), (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                builder.AddBox(
                    new Vector3(0.2f - DiagonalHalfBlockSize, HalfFloorHeight - 0.3f + WallOffset, -HalfWallThickness), new Vector3(0, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)), (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                builder.AddBox(
                    new Vector3(0, HalfFloorHeight - 0.3f + WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize - 0.2f, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)), (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize)),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                    );
                createdObjects.Add(WindowHalfDiagonal = builder.ToMesh("WindowHalfDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, 0), new Vector3(HalfBlockSize, 0, BlockSize),
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
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyVert: x => new Vector3(x.x, x.y + (new Vector2(x.z / DiagonalHalfBlockSize, x.x / DiagonalHalfBlockSize).Magnitude(Vector2.one) * HalfFloorHeight), x.z).Rotate(0,135,0),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.ModifyAround(new Vector2(0.45f, 0.5f), z => (z / new Vector2(0.9f, 1) / DiagonalMagnitude).Rotate(y.IsNegative() ? 135 : 45) * new Vector2(0.9f, 2)) + new Vector2(0,0.5f) : x.Rotate(-90),
                    generation: ((0, 0, 1, .1f), Axis.NX,Axis.NZ)
                    );
                createdObjects.Add(RoofDiagonal = builder.ToMesh("RoofDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, -FloorThickness, -DiagonalHalfBlockSize), new Vector3(0, 0, 0),
                    (0, 0.45f, 1, 0.9f), (0, 0.45f, 1, 0.9f),
                    (0, 0, 1, .1f), null,
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyVert: x => new Vector3(x.x, x.y +( -x.z / DiagonalHalfBlockSize * HalfFloorHeight), x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                    generation: ((0, 0, 1, .1f), Axis.NX, Axis.Z)
                    );
                builder.AddBox(
                    new Vector3(0, -FloorThickness, -DiagonalHalfBlockSize), new Vector3(DiagonalHalfBlockSize, 0, 0),
                    (0, 0, 1, 0.45f), (0, 0, 1, 0.45f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), null,
                    modifyVert: x => new Vector3(x.x, x.y + (-x.z / DiagonalHalfBlockSize * HalfFloorHeight), x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                    generation: ((0, 0, 1, .1f), Axis.X, Axis.Z)
                    );
                createdObjects.Add(RoofDiagonalAlt = builder.ToMesh("RoofDiagonalAlt"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyVert: x=> new Vector3(x.x, x.y + Mathf.Min(-x.x / BlockSize + 0.5f, x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    generation: new DivideGeneration(Axis.X, Axis.Z, (x, y, z) => !y ? new Vertex(z, uniqueModify: w => w.Unique + 3, uvModify: w => new Vector2(0.9f - (w.UV.y * 0.9f), w.UV.x / 0.9f)) : z)
                    );
                createdObjects.Add(RoofCorner = builder.ToMesh("RoofCorner"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyVert: x => new Vector3(x.x, x.y + Mathf.Max(x.x / BlockSize + 0.5f, x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    generation: new DivideGeneration(Axis.NX, Axis.Z, (x,y,z) => y ? new Vertex(z,uniqueModify: w => w.Unique + 3, uvModify: w => new Vector2(0.9f - (w.UV.y * 0.9f), w.UV.x / 0.9f)) : z)
                    );
                createdObjects.Add(RoofCornerInverted = builder.ToMesh("RoofCornerInverted"));
            }
        }

        public static List<Vector2> UV(this Face face, Func<Vector2, Vector2> modifyUV) => new List<Vector2>
        {
            modifyUV(new Vector2(face.maxX, face.maxY)),
            modifyUV(new Vector2(face.maxX, face.minY)),
            modifyUV(new Vector2(face.minX, face.minY)),
            modifyUV(new Vector2(face.minX, face.maxY))
        };

        public static void AddBox(this MeshBuilder builder, Vector3 min, Vector3 max, Face top = null, Face bottom = null, Face north = null, Face east = null, Face south = null, Face west = null, Func<Vector3,Vector3> modifyVert = null, Func<Vector2, Axis, Vector2> modifyUV = null, DefaultGeneration generation = null, Func<Axis[],IEnumerable<Vertex.Weight>> getBoneWeights = null, int uniqueOffset = 0)
        {
            if (generation == null)
                generation = new DefaultGeneration(builder, modifyVert, modifyUV, getBoneWeights);
            else
            {
                generation.builder = builder;
                generation.modifyVert = modifyVert;
                generation.modifyUV = modifyUV;
                generation.getBoneWeights = getBoneWeights;
            }
            generation.AddBox(min, max, top, bottom, north, east, south, west, uniqueOffset);
        }
    }

    public class Face
    {
        public float minX;
        public float minY;
        public float maxX;
        public float maxY;
        public int submesh;
        public Face(float MinX, float MinY, float MaxX, float MaxY, int Submesh = 0) => (minX, minY, maxX, maxY, submesh) = (MinX, MinY, MaxX, MaxY, Submesh);
        public static implicit operator Face((float, float, float, float) value) => new Face(value.Item1, value.Item2, value.Item3, value.Item4);
        public static implicit operator Face((float, float, float, float, int) value) => new Face(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
    }

    public class DefaultGeneration
    {
        public MeshBuilder builder;
        public Func<Vector3, Vector3> modifyVert;
        public Func<Vector2, Axis, Vector2> modifyUV;
        public Func<Axis[], IEnumerable<Vertex.Weight>> getBoneWeights;
        public Vector3 ModifyVert(Vector3 original) => modifyVert?.Invoke(original) ?? original;
        public Vector2 ModifyUV(Vector2 original, Axis axis) => modifyUV?.Invoke(original,axis) ?? original;
        public IEnumerable<Vertex.Weight> GetBoneWeights(Axis[] axes) => getBoneWeights?.Invoke(axes);
        public virtual bool ShouldGenerate(Axis direction, Face face) => face != null;
        public DefaultGeneration() { }
        public DefaultGeneration(MeshBuilder Builder, Func<Vector3, Vector3> ModifyVert = null, Func<Vector2, Axis, Vector2> ModifyUV = null, Func<Axis[], IEnumerable<Vertex.Weight>> GetBoneWeights = null)
            => (builder, modifyVert, modifyUV, getBoneWeights) = (Builder, ModifyVert, ModifyUV, GetBoneWeights);


        public void AddBox(Vector3 min, Vector3 max, Face top = null, Face bottom = null, Face north = null, Face east = null, Face south = null, Face west = null, int uniqueOffset = 0)
        {
            AddFace(min, max, Axis.Y, top, 0 + uniqueOffset);
            AddFace(min, max, Axis.NY, bottom, 0 + uniqueOffset);
            AddFace(min, max, Axis.Z, north, 1 + uniqueOffset);
            AddFace(min, max, Axis.X, east, 2 + uniqueOffset);
            AddFace(min, max, Axis.NZ, south, 1 + uniqueOffset);
            AddFace(min, max, Axis.NX, west, 2 + uniqueOffset);
            PostGeneration(min, max, uniqueOffset);
        }
        public void AddFace(Vector3 min, Vector3 max, Axis direction, Face face, int unique, Action<Axis[]> beforeAdd = null, Axis? final = null)
        {
            if (!ShouldGenerate(final ?? direction, face))
                return;
            var u = face.UV(x => ModifyUV(x, final ?? direction));
            var p = new List<(Axis[] a, Vertex v)>();
            foreach (var a in GetFacePoints(direction))
            {
                beforeAdd?.Invoke(a);
                p.Add((a, new Vertex(ModifyVert(new Vector3((a[0].IsNegative() ? min : max).x, (a[1].IsNegative() ? min : max).y, (a[2].IsNegative() ? min : max).z)), u.Take(), GetBoneWeights(a), unique: unique)));
            }
            AddFace(final ?? direction, p, face.submesh);
        }
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
        public virtual void AddFace(Axis direction, List<(Axis[] a, Vertex v)> values, int submesh) => builder.AddSquare(values[0].v, values[1].v, values[2].v, values[3].v,submesh);
        public virtual void PostGeneration(Vector3 min, Vector3 max, int uniqueOffset) { }

        public static implicit operator DefaultGeneration((Face, Axis, Axis) value) => new SlicedGeneration(value.Item1, value.Item2, value.Item3);
        public static implicit operator DefaultGeneration((Axis, Axis) value) => new DivideGeneration(value.Item1, value.Item2);
    }
    public class SlicedGeneration : DefaultGeneration
    {
        Axis[] slices;
        Face slice;
        public SlicedGeneration(MeshBuilder Builder, Face alongSlice, Axis axis1, Axis axis2, Func<Vector3, Vector3> ModifyVert = null, Func<Vector2, Axis, Vector2> ModifyUV = null, Func<Axis[], IEnumerable<Vertex.Weight>> GetBoneWeights = null) : base(Builder, ModifyVert, ModifyUV, GetBoneWeights) => Setup(alongSlice, axis1, axis2);
        public SlicedGeneration(Face alongSlice, Axis axis1, Axis axis2) => Setup(alongSlice, axis1, axis2);
        void Setup(Face alongSlice, Axis axis1, Axis axis2) => (slice, slices) = (alongSlice, axis1.ToPositive() == axis2.ToPositive() ? new Axis[0] : new[] { axis1, axis2 });
        public override bool ShouldGenerate(Axis direction, Face face) => !slices.Contains(direction) && base.ShouldGenerate(direction, face);
        public override void AddFace(Axis direction, List<(Axis[] a, Vertex v)> values, int submesh)
        {
            if (direction == Axis.None || slices.Contains(direction.Opposite()))
                base.AddFace(direction, values,submesh);
            else
            {
                values.RemoveAll(x => slices.Contains(x.a[(int)direction.Next().ToPositive()]) && slices.Contains(x.a[(int)direction.Next(2).ToPositive()]));
                builder.AddTriangle(values[0].v, values[1].v, values[2].v,submesh);
            }
        }
        public override void PostGeneration(Vector3 min, Vector3 max, int uniqueOffset)
        {
            base.PostGeneration(min, max, uniqueOffset);
            var d = slices.Contains(Axis.X) ? Axis.X : slices.Contains(Axis.Z) ? Axis.Z : slices.Contains(Axis.NX) ? Axis.NX : Axis.NZ;
            var o = slices[(Array.IndexOf(slices, d) + 1) % 2];
            AddFace(min, max, d, slice, 3 + uniqueOffset, a =>
            {
                if (a.Contains(o))
                    a[(int)d.ToPositive()] = a[(int)d.ToPositive()].Opposite();
            }, final: Axis.None);
        }
    }

    public class DivideGeneration : DefaultGeneration
    {
        Axis[] directions;
        Func<Axis, bool, Vertex, Vertex> tweakPoint;
        public Vertex TweakPoint(Axis axis, bool innerSide, Vertex original) => tweakPoint?.Invoke(axis,innerSide,original) ?? original;
        public DivideGeneration(MeshBuilder Builder, Axis Axis1, Axis Axis2, Func<Vector3, Vector3> ModifyVert = null, Func<Vector2, Axis, Vector2> ModifyUV = null, Func<Axis[], IEnumerable<Vertex.Weight>> GetBoneWeights = null, Func<Axis, bool, Vertex, Vertex> TweakPoint = null) : base(Builder, ModifyVert, ModifyUV, GetBoneWeights) => Setup(Axis1, Axis2, TweakPoint);
        public DivideGeneration(Axis Axis1, Axis Axis2, Func<Axis, bool, Vertex, Vertex> TweakPoint = null) => Setup(Axis1, Axis2, TweakPoint);
        void Setup(Axis axis1, Axis axis2, Func<Axis, bool, Vertex, Vertex> TweakPoint) => (tweakPoint, directions) = (TweakPoint, axis1.ToPositive() == axis2.ToPositive() ? new Axis[0] : new[] { axis1, axis2 });
        public override void AddFace(Axis direction, List<(Axis[] a, Vertex v)> values, int submesh)
        {
            if (directions.Contains(direction) || directions.Contains(direction.Opposite()))
                base.AddFace(direction, values, submesh);
            else
            {
                var ind = values.FindIndex(x => directions.Contains(x.a[(int)direction.Next().ToPositive()]) && directions.Contains(x.a[(int)direction.Next(2).ToPositive()]));
                builder.AddTriangle(
                    TweakPoint(direction, false, values.GetWrapped(ind + 1).v),
                    TweakPoint(direction, false, values.GetWrapped(ind + 2).v),
                    TweakPoint(direction, false, values.GetWrapped(ind + 3).v),
                    submesh
                );
                builder.AddTriangle(
                    TweakPoint(direction, true, values.GetWrapped(ind + 3).v),
                    TweakPoint(direction, true, values[ind].v),
                    TweakPoint(direction, true, values.GetWrapped(ind + 1).v),
                    submesh
                );
            }
        }
    }
}
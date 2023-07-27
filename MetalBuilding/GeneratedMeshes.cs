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
        public const float WallOffset = -0.07f;
        public const float FloorThickness = 0.2f;
        public const float HalfWallThickness = 0.06f;
        public const float HalfPillarThickness = 0.075f;
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
        public static Mesh Ladder;
        public static Mesh LadderHalf;
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
                    generation: ((0, 0.3333333f, 0.9f, 1, 2, 1), Axis.Z,Axis.NX)
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
                    generation: ((0, 0.3333333f, 0.9f, 1, 2, 1), Axis.NZ, Axis.NX)
                    );
                createdObjects.Add(FoundationTriangleMirrored = builder.ToMesh("FoundationTriangleMirrored"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (1, 0, 1.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, 0.1f, Axis.Y), (0, 0, 1, 0.1f, Axis.Y),
                    (0, 0, 1, 0.1f, Axis.Y), (0, 0, 1, 0.1f, Axis.Y),
                    modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90)
                    );
                createdObjects.Add(Floor = builder.ToMesh("Floor"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (1, 0, 1.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, 0.1f, Axis.Y), (0, 0, 1, 0.1f, Axis.Y),
                    (0, 0, 1, 0.1f, Axis.Y), (0, 0, 1, 0.1f, Axis.Y),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    generation: ((0, 0, 1, 0.1f, Axis.Y), Axis.Z, Axis.NX)
                    );
                createdObjects.Add(FloorTriangle = builder.ToMesh("FloorTriangle"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (1, 0, 1.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, 0.1f, Axis.Y), (0, 0, 1, 0.1f, Axis.Y),
                    (0, 0, 1, 0.1f, Axis.Y), (0, 0, 1, 0.1f, Axis.Y),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    generation: ((0, 0, 1, 0.1f, Axis.Y), Axis.NZ, Axis.NX)
                    );
                createdObjects.Add(FloorTriangleMirrored = builder.ToMesh("FloorTriangleMirrored"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2, Axis.X),
                    (1, 0, 1.9f, 2), (0.9f, 0, 1, 2, Axis.NX)
                    );
                createdObjects.Add(Wall = builder.ToMesh("Wall"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2, Axis.X),
                    (1, 0, 1.9f, 2), (0.9f, 0, 1, 2, Axis.NX)
                    );
                createdObjects.Add(WallDiagonal = builder.ToMesh("WallDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1, Axis.X),
                    (1, 0, 1.9f, 1), (0.9f, 0, 1, 1, Axis.NX)
                    );
                createdObjects.Add(WallHalf = builder.ToMesh("WallHalf"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1, Axis.X),
                    (1, 0, 1.9f, 1), (0.9f, 0, 1, 1, Axis.NX)
                    );
                createdObjects.Add(WallHalfDiagonal = builder.ToMesh("WallHalfDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0, HalfFloorHeight / 2 + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 0.5f, Axis.NX), (0.9f, 0, 1, 0.5f, Axis.X),
                    (0.45f, 0, 0.9f, 0.5f), null,
                    (1, 0, 1.45f, 0.5f), (0.45f, 0, 1, 0.5f, Axis.NX),
                    generation: ((1, 1, 0.9f, 0.5f, Axis.NX), Axis.Y, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(0, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight / 2 + WallOffset, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1, Axis.NX), (0.9f, 0.5f, 1, 1, Axis.X),
                    (0, 0, 0.45f, 0.5f), (0.45f, 0, 1, 0.5f, Axis.X),
                    (1.45f, 0, 1.9f, 0.5f), null,
                    generation: ((0.9f, 0, 1, 0.5f, Axis.X), Axis.Y, Axis.X)
                    );
                createdObjects.Add(WallV = builder.ToMesh("WallV"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1, Axis.X),
                    (1, 0, 1.9f, 1), (0.9f, 0, 1, 1, Axis.NX),
                    generation: ((0.9f, 0, 1, 1, Axis.X), Axis.Y, Axis.X)
                    );
                createdObjects.Add(WallSlope = builder.ToMesh("WallSlope"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight - WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1, Axis.X),
                    (1, 0, 1.9f, 1), (0.9f, 0, 1, 1, Axis.NX),
                    generation: ((0.9f, 0, 1, 1, Axis.X), Axis.Y, Axis.X)
                    );
                createdObjects.Add(WallSlopeInverted = builder.ToMesh("WallSlopeInverted"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, HalfFloorHeight / 4 + WallOffset, -0.04f), new Vector3(HalfBlockSize, HalfFloorHeight * 7 / 12 + WallOffset, 0.04f),
                    (1.9f, 0, 2, 1), (1.9f, 0, 2, 1),
                    (1, 0.6666666f, 1.9f, 1), null,
                    (1, 0.6666666f, 1.9f, 1), null
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
                    new Vector3(-DiagonalHalfBlockSize, HalfFloorHeight / 4 + WallOffset, -0.04f), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight * 7 / 12 + WallOffset, 0.04f),
                    (1.9f, 0, 2, 0.5f), (1.9f, 0, 2, 0.5f),
                    (1, 0.6666666f, 1.9f, 1, 2, 1), null,
                    (1, 0.6666666f, 1.9f, 1, 2, 1), null
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
                    new Vector3(-0.08f, WallOffset - 0.001f, -0.08f), new Vector3(0.08f, HalfFloorHeight / 4 * 3 + WallOffset + 0.001f, 0.08f),
                    (0.75f, 0.6666666f, 0.9f, 0.3333333f, 2, 1, () => (0, 0.3333333f, 0.15f, 0.6666666f)), (0.75f, 0.6666666f, 0.9f, 0.3333333f, 2, 1, () => (0, 0.3333333f, 0.15f, 0.6666666f)),
                    (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2), (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2),
                    (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2), (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(90)
                    );
                createdObjects.Add(FenceConnector = builder.ToMesh("FenceConnector"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, HalfFloorHeight / 12 + WallOffset, -HalfWallThickness), new Vector3(0, HalfFloorHeight - 0.001f + WallOffset, HalfWallThickness),
                    (1.9f, 0, 2, 0.5f), (1.9f, 0, 2, 0.5f),
                    (1.45f, 0, 1.9f, 1), (1.9f, 0, 2, 1),
                    (1, 0, 1.45f, 1), (1.9f, 0, 2, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, HalfFloorHeight / 12 + WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize- 0.1f , HalfFloorHeight - 0.001f + WallOffset, HalfWallThickness),
                    (1.9f, 0.5f, 2, 1), (1.9f, 0.5f, 2, 1),
                    (1, 0, 1.45f, 1), (1.9f, 0, 2, 1),
                    (1.45f, 0, 1.9f, 1), (1.9f, 0, 2, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.1f - HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.1f, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                createdObjects.Add(Gate = builder.ToMesh("Gate"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(0.1f - DiagonalHalfBlockSize, HalfFloorHeight / 12 + WallOffset, -HalfWallThickness), new Vector3(0, HalfFloorHeight - 0.001f + WallOffset, HalfWallThickness),
                    (1.9f, 0, 2, 0.5f), (1.9f, 0, 2, 0.5f),
                    (1, 0, 1.9f, 1), (1.9f, 0, 2, 1),
                    (1, 0, 1.9f, 1), (1.9f, 0, 2, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, HalfFloorHeight / 12 + WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize - 0.1f, HalfFloorHeight - 0.001f + WallOffset, HalfWallThickness),
                    (1.9f, 0.5f, 2, 1), (1.9f, 0.5f, 2, 1),
                    (1, 0, 1.9f, 1), (1.9f, 0, 2, 1),
                    (1, 0, 1.9f, 1), (1.9f, 0, 2, 1),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.1f - DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.1f, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.9f, 1, 1), (0.9f, 0.9f, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                    );
                createdObjects.Add(GateDiagonal = builder.ToMesh("GateDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, 0.05f, -HalfWallThickness), new Vector3(0, FullFloorHeight - 0.1f, HalfWallThickness),
                    (0.9f, 0, 1, 0.5f, Axis.NX), (0.9f, 0, 1, 0.5f, Axis.X),
                    (0.45f, 0, 0.9f, 2), (0.9f, 0, 1, 2, Axis.X),
                    (1, 0, 1.45f, 2), (0.9f, 0, 1, 2, Axis.NX),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, 0.05f, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight - 0.1f, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1, Axis.NX), (0.9f, 0.5f, 1, 1, Axis.X),
                    (0, 0, 0.45f, 2), (0.9f, 0, 1, 2, Axis.X),
                    (1.45f, 0, 1.9f, 2), (0.9f, 0, 1, 2, Axis.NX),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.1f - HalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.9f, 1, 1, Axis.NX), (0.9f, 0.9f, 1, 1, Axis.X),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2, Axis.X),
                    (1.9f, 0, 2, 2), (0.9f, 0, 1, 2, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.1f, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.9f, 1, 1, Axis.NX), (0.9f, 0.9f, 1, 1, Axis.X),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2, Axis.X),
                    (1.9f, 0, 2, 2), (0.9f, 0, 1, 2, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, 0.05f, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0.9f, 1, 1), null,
                    (0, 1.9f, 1, 2), null,
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, FullFloorHeight - 0.1f, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0.9f, 1, 1), null,
                    (0, 1.9f, 1, 2), null,
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                createdObjects.Add(Door = builder.ToMesh("Door"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, 0.05f, -HalfWallThickness), new Vector3(0, FullFloorHeight - 0.1f, HalfWallThickness),
                    (0.9f, 0, 1, 0.5f, Axis.NX), (0.9f, 0, 1, 0.5f, Axis.X),
                    (0.45f, 0, 0.9f, 2), (0.9f, 0, 1, 2, Axis.X),
                    (1, 0, 1.45f, 2), (0.9f, 0, 1, 2, Axis.NX),
                    getBoneWeights: x => new Vertex.Weight[] { (2, 1f) }
                    );
                builder.AddBox(
                    new Vector3(0, 0.05f, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight - 0.1f, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1, Axis.NX), (0.9f, 0.5f, 1, 1, Axis.X),
                    (0, 0, 0.45f, 2), (0.9f, 0, 1, 2, Axis.X),
                    (1.45f, 0, 1.9f, 2), (0.9f, 0, 1, 2, Axis.NX),
                    getBoneWeights: x => new Vertex.Weight[] { (1, 1f) }
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.1f - HalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1, Axis.NX), (0.9f, 0.5f, 1, 1, Axis.X),
                    (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2), (0.9f, 0, 1, 2, Axis.X),
                    (0.3333333f, 1, 0.6666666f, 1.9f, 1, 2), (0.9f, 0, 1, 2, Axis.NX),
                    modifyUV: (x,y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.1f, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.5f, 1, 1, Axis.NX), (0.9f, 0.5f, 1, 1, Axis.X),
                    (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2), (0.9f, 0, 1, 2, Axis.X),
                    (0.3333333f, 1, 0.6666666f, 1.9f, 1, 2), (0.9f, 0, 1, 2, Axis.NX),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, 0.05f, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0.9f, 1, 1), null,
                    (0, 1.9f, 1, 2), null,
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(0.1f - HalfBlockSize, FullFloorHeight - 0.1f, -HalfWallThickness), new Vector3(HalfBlockSize - 0.1f, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0.9f, 1, 1), null,
                    (0, 1.9f, 1, 2), null,
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                createdObjects.Add(DoorDiagonal = builder.ToMesh("DoorDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1, Axis.X),
                    (1, 0, 1.9f, 1), (0.9f, 0, 1, 1, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0.6666666f, 0.9f, 1), (0.9f, 0.6666666f, 1, 1, Axis.X),
                    (1, 0.6666666f, 1.9f, 1), (0.9f, 0.6666666f, 1, 1, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, HalfFloorHeight + WallOffset, -HalfWallThickness), new Vector3(0.05f - HalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, HalfWallThickness),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f, Axis.X),
                    (1.9f, 0, 2, 1), (0.9f, 0, 1, 0.6666666f, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.05f, HalfFloorHeight + WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, HalfWallThickness),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f, Axis.X),
                    (1.9f, 0, 2, 1), (0.9f, 0, 1, 0.6666666f, Axis.NX)
                    );
                createdObjects.Add(Window = builder.ToMesh("Window"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0, 0.9f, 1, 2, 1), (0.9f, 0, 1, 1, Axis.X),
                    (1, 0, 1.9f, 1, 2, 1), (0.9f, 0, 1, 1, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, FullFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 1, Axis.NX), (0.9f, 0, 1, 1, Axis.X),
                    (0, 0.6666666f, 0.9f, 1, 2, 1), (0.9f, 0.6666666f, 1, 1, Axis.X),
                    (1, 0.6666666f, 1.9f, 1, 2, 1), (0.9f, 0.6666666f, 1, 1, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, -HalfWallThickness), new Vector3(0.05f - DiagonalHalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, HalfWallThickness),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f, Axis.X),
                    (1.9f, 0, 2, 1), (0.9f, 0, 1, 0.6666666f, Axis.NX)
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.05f, HalfFloorHeight + WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, FullFloorHeight / 6 * 5 + WallOffset, HalfWallThickness),
                    null, null,
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 0.6666666f, Axis.X),
                    (1.9f, 0, 2, 1), (0.9f, 0, 1, 0.6666666f, Axis.NX)
                    );
                createdObjects.Add(WindowDiagonal = builder.ToMesh("WindowDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.2f - HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 0.2f / BlockSize, Axis.NX), (0.9f, 1 - (0.2f / BlockSize), 1, 1, Axis.X),
                    (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2), (0.9f, 0, 1, 1, Axis.X),
                    (0.3333333f, 1, 0.6666666f, 1.9f, 1, 2), (0.9f, 0, 1, 1, Axis.NX),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize - 0.2f, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 1 - (0.2f / BlockSize), 1, 1, Axis.NX), (0.9f, 0, 1, 0.2f / BlockSize, Axis.X),
                    (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2), (0.9f, 0, 1, 1, Axis.X),
                    (0.3333333f, 1, 0.6666666f, 1.9f, 1, 2), (0.9f, 0, 1, 1, Axis.NX),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(0.2f - HalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(HalfBlockSize - 0.2f, 0.29f, HalfWallThickness),
                    (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize), Axis.NX), (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize), Axis.X),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (1, 0.3333333f, 1.9f, 0.6666666f), null
                    );
                builder.AddBox(
                    new Vector3(0.2f - HalfBlockSize, HalfFloorHeight - 0.31f, -HalfWallThickness), new Vector3(HalfBlockSize - 0.2f, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize), Axis.NX), (0.9f, 0.2f / BlockSize, 1, 1 - (0.2f / BlockSize), Axis.X),
                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                    (1, 0.3333333f, 1.9f, 0.6666666f), null
                    );
                createdObjects.Add(WindowHalf = builder.ToMesh("WindowHalf"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(0.2f - DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0, 1, 0.2f / DiagonalBlockSize, Axis.NX), (0.9f, 1 - (0.2f / DiagonalBlockSize), 1, 1, Axis.X),
                    (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2), (0.9f, 0, 1, 1, Axis.X),
                    (0.3333333f, 1, 0.6666666f, 1.9f, 1, 2), (0.9f, 0, 1, 1, Axis.NX),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(DiagonalHalfBlockSize - 0.2f, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 1 - (0.2f / DiagonalBlockSize), 1, 1, Axis.NX), (0.9f, 0, 1, 0.2f / DiagonalBlockSize, Axis.X),
                    (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2), (0.9f, 0, 1, 1, Axis.X),
                    (0.3333333f, 1, 0.6666666f, 1.9f, 1, 2), (0.9f, 0, 1, 1, Axis.NX),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                    );
                builder.AddBox(
                    new Vector3(0.2f - DiagonalHalfBlockSize, WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize - 0.2f, 0.3f + WallOffset, HalfWallThickness),
                    (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize), Axis.NX), (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize), Axis.X),
                    (0, 0.3333333f, 0.9f, 0.6666666f, 2, 1), null,
                    (1, 0.3333333f, 1.9f, 0.6666666f, 2, 1), null
                    );
                builder.AddBox(
                    new Vector3(0.2f - DiagonalHalfBlockSize, HalfFloorHeight - 0.3f + WallOffset, -HalfWallThickness), new Vector3(DiagonalHalfBlockSize - 0.2f, HalfFloorHeight + WallOffset, HalfWallThickness),
                    (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize), Axis.NX), (0.9f, 0.2f / DiagonalBlockSize, 1, 1 - (0.2f / DiagonalBlockSize), Axis.X),
                    (0, 0.3333333f, 0.9f, 0.6666666f, 2, 1), null,
                    (1, 0.3333333f, 1.9f, 0.6666666f, 2, 1), null
                    );
                createdObjects.Add(WindowHalfDiagonal = builder.ToMesh("WindowHalfDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, 0), new Vector3(HalfBlockSize, 0, BlockSize),
                    (0, 1, 1, 1.9f), (0, 0, 1, 0.9f),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    modifyVert: x  => new  Vector3(x.x, x.y + (x.z / BlockSize * HalfFloorHeight), x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90)
                    );
                createdObjects.Add(Roof = builder.ToMesh("Roof"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (1, 0, 1.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    modifyVert: x => new Vector3(x.x, x.y + (new Vector2(x.z / DiagonalHalfBlockSize, x.x / DiagonalHalfBlockSize).Magnitude(Vector2.one) * HalfFloorHeight), x.z).Rotate(0,135,0),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.ModifyAround(y.IsNegative() ? new Vector2(0.45f, 0.5f) : new Vector2(1.45f, 0.5f), z => (z / new Vector2(0.9f, 1) / DiagonalMagnitude).Rotate(y.IsNegative() ? 135 : 45) * new Vector2(0.9f, 2)) + new Vector2(0,0.5f) : x.Rotate(-90),
                    generation: ((0, 0, 1, .1f, Axis.Y), Axis.NX,Axis.NZ)
                    );
                createdObjects.Add(RoofDiagonal = builder.ToMesh("RoofDiagonal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-DiagonalHalfBlockSize, -FloorThickness, -DiagonalHalfBlockSize), new Vector3(0, 0, 0),
                    (0, 1.45f, 1, 1.9f), (0, 0.45f, 1, 0.9f),
                    (0, 0, 1, .1f, Axis.Y), null,
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    modifyVert: x => new Vector3(x.x, x.y +( -x.z / DiagonalHalfBlockSize * HalfFloorHeight), x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                    generation: ((0, 0, 1, .1f, Axis.Y), Axis.NX, Axis.Z)
                    );
                builder.AddBox(
                    new Vector3(0, -FloorThickness, -DiagonalHalfBlockSize), new Vector3(DiagonalHalfBlockSize, 0, 0),
                    (0, 1, 1, 1.45f), (0, 0, 1, 0.45f),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), null,
                    modifyVert: x => new Vector3(x.x, x.y + (-x.z / DiagonalHalfBlockSize * HalfFloorHeight), x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                    generation: ((0, 0, 1, .1f, Axis.Y), Axis.X, Axis.Z)
                    );
                createdObjects.Add(RoofDiagonalAlt = builder.ToMesh("RoofDiagonalAlt"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (1, 0, 1.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    modifyVert: x=> new Vector3(x.x, x.y + Mathf.Min(-x.x / BlockSize + 0.5f, x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    generation: new DivideGeneration(Axis.X, Axis.Z, (x, y, z) => !y ? new Vertex(z, uniqueModify: w => w.Unique + 3, uvModify: w => w.UV.ModifyAround(x.IsNegative() ? default : new Vector2(1,0), u => new Vector2(0.9f - (u.y * 0.9f), u.x / 0.9f))) : z)
                    );
                createdObjects.Add(RoofCorner = builder.ToMesh("RoofCorner"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (1, 0, 1.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    modifyVert: x => new Vector3(x.x, x.y + Mathf.Max(x.x / BlockSize + 0.5f, x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    generation: new DivideGeneration(Axis.NX, Axis.Z, (x,y,z) => y ? new Vertex(z,uniqueModify: w => w.Unique + 3, uvModify: w => w.UV.ModifyAround(x.IsNegative() ? default : new Vector2(1, 0), u => new Vector2(0.9f - (u.y * 0.9f), u.x / 0.9f))) : z)
                    );
                createdObjects.Add(RoofCornerInverted = builder.ToMesh("RoofCornerInverted"));
            }
            {
                var builder = new MeshBuilder();
                var flip = false;
                var generator = new DivideGeneration(
                    builder, Axis.NX, Axis.Z,
                    ModifyVert: x => new Vector3(x.x, x.y + (0.5f - Mathf.Max(Mathf.Abs(x.x / BlockSize), Mathf.Abs(x.z / BlockSize))) * HalfFloorHeight, x.z),
                    ModifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    TweakPoint: (x, y, z) => !y ^ flip ? new Vertex(z, uniqueModify: w => w.Unique + 3, uvModify: w => w.UV.ModifyAround(x.IsNegative() ? new Vector2(0.45f, 0.5f) : new Vector2(1.45f, 0.5f), v=> (v / new Vector2(0.9f,1)).Rotate(90) * new Vector2(0.9f, 1))) : z
                );
                generator.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(0, 0, 0),
                    (1.45f, 0, 1.9f, 0.5f), (0, 0, 0.45f, 0.5f),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                flip = true;
                generator.AddBox(
                    new Vector3(0, -FloorThickness, 0), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (1.9f, 0.5f, 1.45f, 0), (0.45f, 0.5f, 0, 0),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                generator.SetDirections(Axis.X, Axis.Z);
                generator.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, 0), new Vector3(0, 0, HalfBlockSize),
                    (1, 0, 1.45f, 0.5f), (0.45f, 0, 0.9f, 0.5f),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                flip = false;
                generator.AddBox(
                    new Vector3(0, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, 0),
                    (1.45f, 0.5f, 1, 0), (0.9f, 0.5f, 0.45f, 0),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                createdObjects.Add(RoofV0 = builder.ToMesh("RoofV0"));
            }
            {
                var builder = new MeshBuilder();
                {
                    var generator = new DivideGeneration(
                        builder, Axis.NX, Axis.Z,
                        ModifyVert: x => new Vector3(x.x, x.y + (0.5f - Mathf.Max(Mathf.Abs(x.x / BlockSize), Mathf.Abs(x.z / BlockSize))) * HalfFloorHeight, x.z),
                        ModifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                        TweakPoint: (x, y, z) => y ? new Vertex(z, uniqueModify: w => w.Unique + 3, uvModify: w => w.UV.ModifyAround(x.IsNegative() ? new Vector2(0.45f, 0.5f) : new Vector2(1.45f, 0.5f), v => (v / new Vector2(0.9f, 1)).Rotate(90) * new Vector2(0.9f, 1))) : z
                    );
                    generator.AddBox(
                        new Vector3(0, -FloorThickness, 0), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                        (1.9f, 0.5f, 1.45f, 0), (0.45f, 0.5f, 0, 0),
                        (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                        (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                        );
                    generator.SetDirections(Axis.X, Axis.Z);
                    generator.AddBox(
                        new Vector3(-HalfBlockSize, -FloorThickness, 0), new Vector3(0, 0, HalfBlockSize),
                        (1, 0, 1.45f, 0.5f), (0.45f, 0, 0.9f, 0.5f),
                        (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                        (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                        );
                }
                {
                    var generator = new DefaultGeneration(
                        builder,
                        ModifyVert: x => new Vector3(x.x, x.y + (0.5f - Mathf.Abs(x.x / BlockSize)) * HalfFloorHeight, x.z),
                        ModifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90));
                    generator.AddBox(
                        new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(0, 0, 0),
                        (1.45f, 0, 1.9f, 0.5f), (0, 0, 0.45f, 0.5f),
                        (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                        (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                        );
                    generator.AddBox(
                        new Vector3(0, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, 0),
                        (1.45f, 0.5f, 1, 0), (0.9f, 0.5f, 0.45f, 0),
                        (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                        (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                        );
                }
                createdObjects.Add(RoofV1 = builder.ToMesh("RoofV1"));
            }
            {
                var builder = new MeshBuilder();
                var generator = new DefaultGeneration(
                    builder,
                    ModifyVert: x => new Vector3(x.x, x.y + (0.5f - Mathf.Abs(x.x / BlockSize)) * HalfFloorHeight, x.z),
                    ModifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90));
                generator.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(0, 0, HalfBlockSize),
                    (1, 0, 1.9f, 0.5f), (0, 0, 0.9f, 0.5f),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                generator.AddBox(
                    new Vector3(0, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (1.9f, 0.5f, 1, 0), (0.9f, 0.5f, 0, 0),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                createdObjects.Add(RoofV2I = builder.ToMesh("RoofV2I"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, 0), new Vector3(0, 0, HalfBlockSize),
                    (1, 0, 1.45f, 0.5f), (0.45f, 0, 0.9f, 0.5f),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    modifyVert: x => new Vector3(x.x, x.y + (0.5f - Mathf.Min(Mathf.Abs(x.x / BlockSize), Mathf.Abs(x.z / BlockSize))) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    generation: new DivideGeneration(Axis.X, Axis.Z, (x, y, z) => !y ? new Vertex(z, uniqueModify: w => w.Unique + 3, uvModify: w => w.UV.ModifyAround(x.IsNegative() ? new Vector2(0.45f, 0.5f) : new Vector2(1.45f, 0.5f), v => (v / new Vector2(0.9f, 1)).Rotate(90) * new Vector2(0.9f, 1))) : z)
                    );
                builder.AddBox(
                    new Vector3(0, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, 0),
                    (1.45f, 0.5f, 1, 0), (0.9f, 0.5f, 0.45f, 0),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    modifyVert: x => new Vector3(x.x, x.y + (0.5f - Mathf.Max(Mathf.Abs(x.x / BlockSize), Mathf.Abs(x.z / BlockSize))) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    generation: new DivideGeneration(Axis.X, Axis.Z, (x, y, z) => !y ? new Vertex(z, uniqueModify: w => w.Unique + 3, uvModify: w => w.UV.ModifyAround(x.IsNegative() ? new Vector2(0.45f, 0.5f) : new Vector2(1.45f, 0.5f), v => (v / new Vector2(0.9f, 1)).Rotate(90) * new Vector2(0.9f, 1))) : z)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(0, 0, 0),
                    (0.5f, 1, 1, 1.45f), (0, 0, 0.5f, 0.45f),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    modifyVert: x => new Vector3(x.x, x.y + (0.5f - Mathf.Abs(x.z / BlockSize)) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90)
                    );
                builder.AddBox(
                    new Vector3(0, -FloorThickness, 0), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (1.9f, 0.5f, 1.45f, 0), (0.45f, 0.5f, 0, 0),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    modifyVert: x => new Vector3(x.x, x.y + (0.5f - Mathf.Abs(x.x / BlockSize)) * HalfFloorHeight, x.z),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90)
                    );
                createdObjects.Add(RoofV2L = builder.ToMesh("RoofV2L"));
            }
            {
                var builder = new MeshBuilder();
                var generator = new DivideGeneration(
                    builder, Axis.NX, Axis.Z,
                    ModifyVert: x => new Vector3(x.x, x.y + (0.5f - Mathf.Min(Mathf.Abs(x.x / BlockSize), Mathf.Abs(x.z / BlockSize))) * HalfFloorHeight, x.z),
                    ModifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    TweakPoint: (x, y, z) => !y ? new Vertex(z, uniqueModify: w => w.Unique + 3, uvModify: w => w.UV.ModifyAround(x.IsNegative() ? new Vector2(0.45f, 0.5f) : new Vector2(1.45f, 0.5f), v => (v / new Vector2(0.9f, 1)).Rotate(90) * new Vector2(0.9f, 1))) : z
                );
                generator.AddBox(
                    new Vector3(0, -FloorThickness, 0), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (1.9f, 0.5f, 1.45f, 0), (0.45f, 0.5f, 0, 0),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                generator.SetDirections(Axis.X, Axis.Z);
                generator.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, 0), new Vector3(0, 0, HalfBlockSize),
                    (1, 0, 1.45f, 0.5f), (0.45f, 0, 0.9f, 0.5f),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, 0),
                    (0.5f, 1.9f, 0, 1), (0, 0, 0.5f, 0.9f),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                    modifyVert: x => new Vector3(x.x, x.y + (0.5f - Mathf.Abs(x.z / BlockSize)) * HalfFloorHeight, x.z)
                    );
                createdObjects.Add(RoofV3 = builder.ToMesh("RoofV3"));
            }
            {
                var builder = new MeshBuilder();
                var flip = false;
                var generator = new DivideGeneration(
                    builder, Axis.NX, Axis.Z,
                    ModifyVert: x => new Vector3(x.x, x.y + (0.5f - Mathf.Min(Mathf.Abs(x.x / BlockSize), Mathf.Abs(x.z / BlockSize))) * HalfFloorHeight, x.z),
                    ModifyUV: (x, y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                    TweakPoint: (x, y, z) => y ^ flip ? new Vertex(z, uniqueModify: w => w.Unique + 3, uvModify: w => w.UV.ModifyAround(x.IsNegative() ? new Vector2(0.45f, 0.5f) : new Vector2(1.45f, 0.5f), v => (v / new Vector2(0.9f, 1)).Rotate(90) * new Vector2(0.9f, 1))) : z
                );
                generator.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, -HalfBlockSize), new Vector3(0, 0, 0),
                    (1.45f, 0, 1.9f, 0.5f), (0, 0, 0.45f, 0.5f),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                flip = true;
                generator.AddBox(
                    new Vector3(0, -FloorThickness, 0), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (1.9f, 0.5f, 1.45f, 0), (0.45f, 0.5f, 0, 0),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                generator.SetDirections(Axis.X, Axis.Z);
                generator.AddBox(
                    new Vector3(-HalfBlockSize, -FloorThickness, 0), new Vector3(0, 0, HalfBlockSize),
                    (1, 0, 1.45f, 0.5f), (0.45f, 0, 0.9f, 0.5f),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                flip = false;
                generator.AddBox(
                    new Vector3(0, -FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, 0, 0),
                    (1.45f, 0.5f, 1, 0), (0.9f, 0.5f, 0.45f, 0),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y),
                    (0, 0, 1, .1f, Axis.Y), (0, 0, 1, .1f, Axis.Y)
                    );
                createdObjects.Add(RoofV4 = builder.ToMesh("RoofV4"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfPillarThickness, -0.001f, -HalfPillarThickness), new Vector3(HalfPillarThickness, FullFloorHeight - 0.001f, HalfPillarThickness),
                    (0.9f, 0.9f, 1, 1, Axis.NX), (0.9f, 0.9f, 1, 1, Axis.X),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2, Axis.X),
                    (1.9f, 0, 2, 2), (0.9f, 0, 1, 2, Axis.NX)
                    );
                createdObjects.Add(Pillar = builder.ToMesh("Pillar"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfPillarThickness, -0.001f, -HalfPillarThickness), new Vector3(HalfPillarThickness, HalfFloorHeight - 0.001f, HalfPillarThickness),
                    (0.9f, 0.9f, 1, 1, Axis.NX), (0.9f, 0.9f, 1, 1, Axis.X),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1, Axis.X),
                    (1.9f, 0, 2, 1), (0.9f, 0, 1, 1, Axis.NX)
                    );
                createdObjects.Add(PillarHalf = builder.ToMesh("PillarHalf"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfPillarThickness, 0, -HalfPillarThickness), new Vector3(HalfPillarThickness, BlockSize * 2, HalfPillarThickness),
                    (0.9f, 0.9f, 1, 1, Axis.NX), (0.9f, 0.9f, 1, 1, Axis.X),
                    (0.9f, 0, 1, 2), (0.9f, 0, 1, 2, Axis.X),
                    (1.9f, 0, 2, 2), (0.9f, 0, 1, 2, Axis.NX)
                    );
                createdObjects.Add(PillarHorizontal = builder.ToMesh("PillarHorizontal"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfPillarThickness, 0, -HalfPillarThickness), new Vector3(HalfPillarThickness, BlockSize, HalfPillarThickness),
                    (0.9f, 0.9f, 1, 1, Axis.NX), (0.9f, 0.9f, 1, 1, Axis.X),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1, Axis.X),
                    (1.9f, 0, 2, 1), (0.9f, 0, 1, 1, Axis.NX)
                    );
                createdObjects.Add(PillarHorizontalHalf = builder.ToMesh("PillarHorizontalHalf"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize * 0.4f, WallOffset, -0.2f), new Vector3(-HalfBlockSize * 0.4f + HalfWallThickness, FullFloorHeight + WallOffset, 0),
                    (0.6666666f, 1.9f, 1, 2), (0.6666666f, 1.9f, 1, 2),
                    null, (0.3333333f, 1, 0.6666666f, 1.9f, 1, 4),
                    (1.9f, 0, 2, 1.3333333f), (0.3333333f, 1, 0.6666666f, 1.9f, 1, 4),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x : x.Rotate(90)
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize * 0.4f - HalfWallThickness, WallOffset, -0.2f), new Vector3(HalfBlockSize * 0.4f, FullFloorHeight + WallOffset, 0),
                    (0.6666666f, 1.9f, 1, 2), (0.6666666f, 1.9f, 1, 2),
                    null, (0.3333333f, 1, 0.6666666f, 1.9f, 1, 4),
                    (1.9f, 0, 2, 1.3333333f), (0.3333333f, 1, 0.6666666f, 1.9f, 1, 4),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x : x.Rotate(90)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize * 0.4f, WallOffset + 0.0001f, -HalfWallThickness / 2), new Vector3(HalfBlockSize * 0.4f, FullFloorHeight + WallOffset - 0.0001f, 0),
                    (1.9f, 0, 2, 1), (1.9f, 0, 2, 1),
                    (1, 0, 1.9f, 4), null,
                    (1, 0, 1.9f, 4), null
                    );
                var n = 6;
                for (var i = 0.5f; i < n; i++)
                {
                    var p = i / n;
                    builder.AddBox(
                        new Vector3(-HalfBlockSize * 0.4f + HalfWallThickness, FullFloorHeight * p + WallOffset - 0.03f, -0.2f), new Vector3(HalfBlockSize * 0.4f - HalfWallThickness, FullFloorHeight * p + WallOffset + 0.03f, -0.14f),
                        (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                        (0, 0.9f, 1, 1), null,
                        (0, 0.9f, 1, 1), null,
                        modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                        );
                }
                createdObjects.Add(Ladder = builder.ToMesh("Ladder"));
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize * 0.4f, WallOffset, -0.2f), new Vector3(-HalfBlockSize * 0.4f + HalfWallThickness, HalfFloorHeight + WallOffset, 0),
                    (0.6666666f, 1.9f, 1, 2), (0.6666666f, 1.9f, 1, 2),
                    null, (0.3333333f, 1, 0.6666666f, 1.9f, 1, 2),
                    (1.9f, 0, 2, 0.6666666f), (0.3333333f, 1, 0.6666666f, 1.9f, 1, 2),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x : x.Rotate(90)
                    );
                builder.AddBox(
                    new Vector3(HalfBlockSize * 0.4f - HalfWallThickness, WallOffset, -0.2f), new Vector3(HalfBlockSize * 0.4f, HalfFloorHeight + WallOffset, 0),
                    (0.6666666f, 1.9f, 1, 2), (0.6666666f, 1.9f, 1, 2),
                    null, (0.3333333f, 1, 0.6666666f, 1.9f, 1, 2),
                    (1.9f, 0, 2, 0.6666666f), (0.3333333f, 1, 0.6666666f, 1.9f, 1, 2),
                    modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x : x.Rotate(90)
                    );
                builder.AddBox(
                    new Vector3(-HalfBlockSize * 0.4f, WallOffset + 0.0001f, -HalfWallThickness / 2), new Vector3(HalfBlockSize * 0.4f, HalfFloorHeight + WallOffset - 0.0001f, 0),
                    (1.9f, 0, 2, 1), (1.9f, 0, 2, 1),
                    (1, 0, 1.9f, 2), null,
                    (1, 0, 1.9f, 2), null
                    );
                var n = 3;
                for (var i = 0.5f; i < n; i++)
                {
                    var p = i / n;
                    builder.AddBox(
                        new Vector3(-HalfBlockSize * 0.4f + HalfWallThickness, HalfFloorHeight * p + WallOffset - 0.03f, -0.2f), new Vector3(HalfBlockSize * 0.4f - HalfWallThickness, HalfFloorHeight * p + WallOffset + 0.03f, -0.14f),
                        (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                        (0, 0.9f, 1, 1), null,
                        (0, 0.9f, 1, 1), null,
                        modifyUV: (x, y) => y.ToPositive() == Axis.Z ? x.Rotate(90) : x
                        );
                }
                createdObjects.Add(LadderHalf = builder.ToMesh("LadderHalf"));
            }
        }

        public static List<Vector2> UV(this Face_Base face, Func<Vector2, Vector2> modifyUV) => new List<Vector2>
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

        public static float Lerp(this float start, float target, float progress) => (target - start) * Mathf.Clamp(progress, 0, 1) + start;

        public static IEnumerable<Vertex.Weight> Lerp(this IEnumerable<Vertex.Weight> start, IEnumerable<Vertex.Weight> target, float progress)
        {
            if (progress >= 1)
                return target;
            if (progress <= 0)
                return start;
            if (start == null && target == null)
                return null;
            if (start == null)
                start = Vertex.Weight.Defaults;
            if (target == null)
                target = Vertex.Weight.Defaults;
            var l = new HashSet<Vertex.Weight>();
            foreach (var b in start)
                l.Add(new Vertex.Weight(b.BoneIndex, b.Strength * progress));
            foreach (var b in target)
            {
                var s = 0f;
                if (l.TryGetValue(b, out var b2)) {
                    s = b2.Strength;
                    l.Remove(b2);
                }
                l.Add(new Vertex.Weight(b.BoneIndex, b.Strength * (1 - progress) + s));
            }
            return l;
        }

        public static int Skip(this int value, int skip) => value >= skip ? value + 1 : value;

        public static int Mod(this int value, int mod) => (value %= mod) < 0 ? value + mod : value;
    }

    public abstract class Face_Base
    {
        public float minX;
        public float minY;
        public float maxX;
        public float maxY;
        public int submesh;
        public Face_Base(float MinX, float MinY, float MaxX, float MaxY, int Submesh = 0) => (minX, minY, maxX, maxY, submesh) = (MinX, MinY, MaxX, MaxY, Submesh);
    }
    public class Face : Face_Base
    {
        public int subdivisionX;
        public int subdivisionY;
        public Func<int, int, SubFace> subdivisionFace;
        public Face(float MinX, float MinY, float MaxX, float MaxY, int SubDivisionX = 1, int SubDivisionY = 1, int Submesh = 0, Func<int, int, SubFace> SubdivisionFace = null) : base(MinX, MinY, MaxX, MaxY, Submesh) => (subdivisionX, subdivisionY, subdivisionFace) = (SubDivisionX, SubDivisionY, SubdivisionFace);
        public static implicit operator Face((float, float, float, float) value) => new Face(value.Item1, value.Item2, value.Item3, value.Item4);
        public static implicit operator Face((float, float, float, float, int) value) => new Face(value.Item1, value.Item2, value.Item3, value.Item4, Submesh: value.Item5);
        public static implicit operator Face((float, float, float, float, int, int) value) => new Face(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
        public static implicit operator Face((float, float, float, float, int, int, int) value) => new Face(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
        public static implicit operator Face((float, float, float, float, int, int, Func<int, int, SubFace>) value) => new Face(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, SubdivisionFace: value.Item7);
        public static implicit operator Face((float, float, float, float, int, int, int, Func<int, int, SubFace>) value) => new Face(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, value.Item8);
        public static implicit operator Face((float, float, float, float, int, int, Func<(int x, int y), SubFace>) value) => (value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, ConvertDelegate(value.Item7));
        public static implicit operator Face((float, float, float, float, int, int, int, Func<(int x, int y), SubFace>) value) => (value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ConvertDelegate(value.Item8));
        public static implicit operator Face((float, float, float, float, int, int, Func<SubFace>) value) => (value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, ConvertDelegate(value.Item7));
        public static implicit operator Face((float, float, float, float, int, int, int, Func<SubFace>) value) => (value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ConvertDelegate(value.Item8));
        public static implicit operator Face((float, float, float, float, Axis) value) => (value.Item1,value.Item2,value.Item3, value.Item4, value.Item5, 0);
        public static implicit operator Face((float, float, float, float, Axis, int) value)
        {
            var isY = value.Item5.ToPositive() == Axis.Y;
            var isN = value.Item5.IsNegative();
            var v1 = isY ? value.Item2 : value.Item1;
            var v2 = isY ? value.Item4 : value.Item3;
            var y__ = v1;
            var _y_ = v1.Lerp(v2, 0.5f);
            var __y = v2;
            var Y___ = _y_ + (isN ? 1 : 0);
            var _Y__ = __y + (isN ? 1 : 0);
            var __Y_ = y__ + (isN ? 0 : 1);
            var ___Y = _y_ + (isN ? 0 : 1);
            return isY
                ? (Face)(value.Item1, Y___, value.Item3, _Y__, 1, 2, value.Item6, () => (value.Item1, __Y_, value.Item3, ___Y))
                : (Y___, value.Item2, _Y__, value.Item4, 2, 1, value.Item6, () => (__Y_, value.Item2, ___Y, value.Item4));
        }
        protected static Func<int, int, SubFace> ConvertDelegate(Func<(int x, int y), SubFace> original)
        {
            if (original == null)
                return null;
            return (x, y) => original((x, y));
        }
        protected static Func<int, int, SubFace> ConvertDelegate(Func<SubFace> original)
        {
            if (original == null)
                return null;
            return (x, y) => original();
        }
    }
    public class SubFace : Face_Base
    {
        public SubFace(float MinX, float MinY, float MaxX, float MaxY, int Submesh = 0) : base(MinX, MinY, MaxX, MaxY, Submesh) { }
        public static implicit operator SubFace((float, float, float, float) value) => new SubFace(value.Item1, value.Item2, value.Item3, value.Item4);
        public static implicit operator SubFace((float, float, float, float, int) value) => new SubFace(value.Item1, value.Item2, value.Item3, value.Item4, Submesh: value.Item5);
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
            var v = new List<(Axis[] a, Vector3 v, Vector2 u, IEnumerable<Vertex.Weight> w)>();
                    foreach (var a in GetFacePoints(direction))
                    {
                        beforeAdd?.Invoke(a);
                        v.Add((a, new Vector3((a[0].IsNegative() ? min : max).x, (a[1].IsNegative() ? min : max).y, (a[2].IsNegative() ? min : max).z), u.Take(), GetBoneWeights(a)));
                    }
            var p = new List<(Axis[] a, Vertex v, float x, float y)>();
            (Vector3 v, IEnumerable<Vertex.Weight> w) GetSubPoint(float x, float y) =>
            (
                Vector3.Lerp(Vector3.Lerp(v[0].v, v[1].v, y), Vector3.Lerp(v[3].v, v[2].v, y), x),
                v[0].w.Lerp(v[1].w, y).Lerp(v[3].w.Lerp(v[2].w, y), x)
            );
            for (int i = 0; i < face.subdivisionX; i++)
                for (int j = 0; j < face.subdivisionY; j++)
                {
                    var lx = (float)i / face.subdivisionX;
                    var ly = (float)j / face.subdivisionY;
                    var ux = (i + 1f) / face.subdivisionX;
                    var uy = (j + 1f) / face.subdivisionY;
                    var rf = (i > 0 || j > 0 ? face.subdivisionFace?.Invoke(i, j) : null)?.UV(x => ModifyUV(x, final ?? direction));
                    var sp = GetSubPoint(lx, ly);
                    p.Add((v[0].a, new Vertex(ModifyVert(sp.v), rf == null ? v[0].u : rf.Take(), sp.w, unique: unique), lx, ly));
                    sp = GetSubPoint(lx, uy);
                    p.Add((v[1].a, new Vertex(ModifyVert(sp.v), rf == null ? v[1].u : rf.Take(), sp.w, unique: unique), lx, uy));
                    sp = GetSubPoint(ux, uy);
                    p.Add((v[2].a, new Vertex(ModifyVert(sp.v), rf == null ? v[2].u : rf.Take(), sp.w, unique: unique), ux, uy));
                    sp = GetSubPoint(ux, ly);
                    p.Add((v[3].a, new Vertex(ModifyVert(sp.v), rf == null ? v[3].u : rf.Take(), sp.w, unique: unique), ux, ly));
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
        public virtual void AddFace(Axis direction, List<(Axis[] a, Vertex v, float x, float y)> values, int submesh)
        {
            for (int i = 0; i < values.Count; i += 4)
                builder.AddSquare(values[i].v, values[i + 1].v, values[i + 2].v, values[i + 3].v, submesh);
        }
        public virtual void PostGeneration(Vector3 min, Vector3 max, int uniqueOffset) { }

        public static implicit operator DefaultGeneration((Face, Axis, Axis) value) => new SlicedGeneration(value.Item1, value.Item2, value.Item3);
        public static implicit operator DefaultGeneration((Axis, Axis) value) => new DivideGeneration(value.Item1, value.Item2);
    }
    public class SlicedGeneration : DefaultGeneration
    {
        Axis[] slices;
        public void SetDirections(Axis Axis1, Axis Axis2) => slices = Axis1.ToPositive() == Axis2.ToPositive() ? new Axis[0] : new[] { Axis1, Axis2 };
        public Face slice;
        public SlicedGeneration(MeshBuilder Builder, Face alongSlice, Axis axis1, Axis axis2, Func<Vector3, Vector3> ModifyVert = null, Func<Vector2, Axis, Vector2> ModifyUV = null, Func<Axis[], IEnumerable<Vertex.Weight>> GetBoneWeights = null) : base(Builder, ModifyVert, ModifyUV, GetBoneWeights) => Setup(alongSlice, axis1, axis2);
        public SlicedGeneration(Face alongSlice, Axis axis1, Axis axis2) => Setup(alongSlice, axis1, axis2);
        void Setup(Face alongSlice, Axis axis1, Axis axis2)
        {
            slice = alongSlice;
            SetDirections(axis1, axis2);
        }
        public override bool ShouldGenerate(Axis direction, Face face) => !slices.Contains(direction) && base.ShouldGenerate(direction, face);
        public override void AddFace(Axis direction, List<(Axis[] a, Vertex v, float x, float y)> values, int submesh)
        {
            if (direction == Axis.None || slices.Contains(direction.Opposite()))
                base.AddFace(direction, values,submesh);
            else
                for (int i = 0; i < values.Count; i += 4)
                {
                    var ind = values.FindIndex(i, 4, x => slices.Contains(x.a[(int)direction.Next().ToPositive()]) && slices.Contains(x.a[(int)direction.Next(2).ToPositive()]));
                    bool Check(int index)
                    {
                        index = index.Mod(4);
                        return ((values[ind].x < values[(ind + 2).Mod(4) + i].x ? 1 - values[index + i].x : values[index + i].x) + (values[ind].y < values[(ind + 2).Mod(4) + i].y ? 1 - values[index + i].y : values[index + i].y)) > 1;
                    }
                    if (!Check(ind))
                        builder.AddSquare(values[i].v, values[i + 1].v, values[i + 2].v, values[i + 3].v, submesh);
                    else if (!Check(ind + 1) && !Check(ind - 1))
                        builder.AddTriangle(values[i.Skip(ind)].v, values[(i + 1).Skip(ind)].v, values[(i + 2).Skip(ind)].v, submesh);
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
        public void SetDirections(Axis Axis1, Axis Axis2) => directions = Axis1.ToPositive() == Axis2.ToPositive() ? new Axis[0] : new[] { Axis1, Axis2 };
        public Func<Axis, bool, Vertex, Vertex> tweakPoint;
        public Vertex TweakPoint(Axis axis, bool innerSide, Vertex original) => tweakPoint?.Invoke(axis,innerSide,original) ?? original;
        public DivideGeneration(MeshBuilder Builder, Axis Axis1, Axis Axis2, Func<Vector3, Vector3> ModifyVert = null, Func<Vector2, Axis, Vector2> ModifyUV = null, Func<Axis[], IEnumerable<Vertex.Weight>> GetBoneWeights = null, Func<Axis, bool, Vertex, Vertex> TweakPoint = null) : base(Builder, ModifyVert, ModifyUV, GetBoneWeights) => Setup(Axis1, Axis2, TweakPoint);
        public DivideGeneration(Axis Axis1, Axis Axis2, Func<Axis, bool, Vertex, Vertex> TweakPoint = null) => Setup(Axis1, Axis2, TweakPoint);
        void Setup(Axis axis1, Axis axis2, Func<Axis, bool, Vertex, Vertex> TweakPoint)
        {
            tweakPoint = TweakPoint;
            SetDirections(axis1, axis2);
        }
        public override void AddFace(Axis direction, List<(Axis[] a, Vertex v, float x, float y)> values, int submesh)
        {
            if (directions.Contains(direction) || directions.Contains(direction.Opposite()))
                base.AddFace(direction, values, submesh);
            else
                for (int i = 0; i < values.Count; i += 4)
                {
                    var ind = values.FindIndex(i,4,x => directions.Contains(x.a[(int)direction.Next().ToPositive()]) && directions.Contains(x.a[(int)direction.Next(2).ToPositive()]));
                    builder.AddTriangle(
                        TweakPoint(direction, false, values[(ind + 1).Mod(4) + i].v),
                        TweakPoint(direction, false, values[(ind + 2).Mod(4) + i].v),
                        TweakPoint(direction, false, values[(ind + 3).Mod(4) + i].v),
                        submesh
                    );
                    builder.AddTriangle(
                        TweakPoint(direction, true, values[(ind + 3).Mod(4) + i].v),
                        TweakPoint(direction, true, values[ind].v),
                        TweakPoint(direction, true, values[(ind + 1).Mod(4) + i].v),
                        submesh
                    );
                }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreBuilding
{
    public static class TextEnumExtentionMethods
    {
        public static string ToText(this Enum v, params object[] args) => string.Format(v.GetText(), args.Cast(x => x is Enum e ? e.GetText() : x.ToString()));
        public static string GetText(this Enum v)
        {
            var f = v.GetType().GetField(v.ToString(), ~System.Reflection.BindingFlags.Default);
            if (f == null)
                return v.ToString();
            var a = f.GetCustomAttributes(false);
            foreach (var i in a)
                if (i is TextAttribute t && t)
                    return t.Text;
            return v.ToString();
        }
    }

    public enum UniqueName
    {
        [Text("ScrapMetal")]
        ScrapMetal,
        [Text("Metal")]
        SolidMetal,
        [Text("Glass")]
        Glass,
        [Text("Block_{0}")]
        Base_,
        [Text("{0}_Mirrored")]
        _Mirror,
        [Text("Triangular_{0}")]
        Triangle_,
        [Text("{0}_Half")]
        _Half,
        [Text(Base_, "Roof_{0}")]
        Roof_,
        [Text(Base_, "Upgrade_{0}")]
        Upgrade,
        [Text(Base_, "Foundation_{0}")]
        Foundation,
        [Text(Foundation, Triangle_)]
        TriangleFoundation,
        [Text(TriangleFoundation, _Mirror)]
        TriangleFoundationMirrored,
        [Text(Base_, "Floor_{0}")]
        Floor,
        [Text(Floor, Triangle_)]
        TriangleFloor,
        [Text(TriangleFloor, _Mirror)]
        TriangleFloorMirrored,
        [Text(Base_, "Wall_{0}")]
        Wall,
        [Text(Wall, _Half)]
        WallHalf,
        [Text(Wall, "VSlope_{0}")]
        WallV,
        [Text(Wall, "Slope_{0}")]
        WallSlope,
        [Text(WallSlope, "{0}_Inverted")]
        WallSlopeInverted,
        [Text(Wall, "Fence_{0}")]
        Fence,
        [Text(Wall, "Gate_{0}")]
        Gate,
        [Text(Wall, "Door_{0}")]
        Door,
        [Text(Wall, "Window_{0}")]
        Window,
        [Text(Window, _Half)]
        WindowHalf,
        [Text(Roof_, "{0}_Straight")]
        RoofStraight,
        [Text(Roof_, "{0}_Corner")]
        RoofCorner,
        [Text(Roof_, "{0}_InvCorner")]
        RoofCornerInverted,
        [Text(Roof_, "{0}_Pyramid")]
        RoofV0,
        [Text(Roof_, "{0}_EndCap")]
        RoofV1,
        [Text(Roof_, "{0}_StraightV")]
        RoofV2I,
        [Text(Roof_, "{0}_LJunction")]
        RoofV2L,
        [Text(Roof_, "{0}_TJunction")]
        RoofV3,
        [Text(Roof_, "{0}_XJunction")]
        RoofV4,
        [Text(Base_, "Pillar_{0}")]
        Pillar,
        [Text(Pillar, _Half)]
        PillarHalf,
        [Text(Base_, "HorizontalPillar_{0}")]
        PillarHorizontal,
        [Text(PillarHorizontal, _Half)]
        PillarHorizontalHalf,
        [Text(Base_, "Ladder_{0}")]
        Ladder,
        [Text(Ladder, _Half)]
        LadderHalf,
        [Text(Base_, "HalfFloor_{0}")]
        FloorHalf,
        [Text(FloorHalf, Triangle_)]
        TriangleFloorHalf,
        [Text(TriangleFloorHalf, _Mirror)]
        TriangleHalfFloorMirrored,
        [Text(Base_, "Stair_{0}")]
        Stair,
        [Text(Stair, _Half)]
        StairHalf
    }

    public enum Localization
    {
        [Text(Language.french, "Scrap Metal")]
        [Text("Scrap Metal")]
        ScrapMetal,
        [Text("Solid Metal")]
        SolidMetal,
        [Text("Glass")]
        Glass,
        [Text("{0}@{1}")]
        _Base_,
        [Text("Triangular {0}")]
        Triangle_,
        [Text("{0} Half")]
        _Half,
        [Text("Half {0}")]
        Half_,
        [Text("Provides support for additional floors")]
        _Support,
        [Text("Covers your head")]
        _Roof,
        [Text(_Base_, "{0}-Junction", _Roof)]
        _RoofJunction,
        [Text(_Base_,"Replace with {0}","")]
        Upgrade,
        [Text(_Base_,"{0} Foundation", "Used to expand your raft on the bottom floor")]
        Foundation,
        [Text(Foundation, Triangle_)]
        TriangleFoundation,
        [Text(_Base_, "{0} Floor", "Used to build additional floors and roof. Cannot be built in thin air")]
        Floor,
        [Text(Floor, Triangle_)]
        TriangleFloor,
        [Text(_Base_, "{0} Wall", _Support)]
        Wall,
        [Text(Wall, Half_)]
        WallHalf,
        [Text(_Base_, "Pyramid {0} Wall","Fills pyramid holes")]
        WallV,
        [Text(_Base_, "Triangular {0} Wall", "Fills triangular holes")]
        WallSlope,
        [Text(_Base_, "{0} Fence", "Keeps you from falling off the raft")]
        Fence,
        [Text(_Base_, "{0} Gate", _Support)]
        Gate,
        [Text(_Base_, "{0} Door", _Support)]
        Door,
        [Text(_Base_, "{0} Window", _Support)]
        Window,
        [Text(Window, Half_)]
        WindowHalf,
        [Text(_Base_, "{0} Roof", _Roof)]
        RoofStraight,
        [Text(_Base_, "{0} Roof Corner", _Roof)]
        RoofCorner,
        [Text(RoofCorner, "Inverted {0}")]
        RoofCornerInverted,
        [Text(_Base_, "{0} Roof Pyramid", _Roof)]
        RoofV0,
        [Text(_Base_, "{0} Roof Endcap", _Roof)]
        RoofV1,
        [Text(_Base_, "{0} Double Roof", _Roof)]
        RoofV2I,
        [Text(_RoofJunction, "{0} L")]
        RoofV2L,
        [Text(_RoofJunction, "{0} T")]
        RoofV3,
        [Text(_RoofJunction, "{0} X")]
        RoofV4,
        [Text(_Base_, "{0} Pillar", _Support)]
        Pillar,
        [Text(Pillar, Half_)]
        PillarHalf,
        [Text(Pillar, "Horizontal {0}")]
        PillarHorizontal,
        [Text(PillarHorizontal, Half_)]
        PillarHorizontalHalf,
        [Text(_Base_, "{0} Ladder", "A good way to go up and down")]
        Ladder,
        [Text(Ladder, _Half)]
        LadderHalf,
        [Text(_Base_, "Raised {0} Floor", "A floor half the height of a wall")]
        FloorHalf,
        [Text(FloorHalf, Triangle_)]
        TriangleFloorHalf,
        [Text(_Base_, "{0} Stair", "Great to get from one floor to another")]
        Stair,
        [Text(_Base_, "{0} Half Stair", "Allows you to reach half a wall height up")]
        StairHalf
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class TextAttribute : Attribute
    {
        public static Language? forcedContext;
        Language? language;
        string text;
        Enum parent;
        object[] children;
        public virtual string Text => children == null || children.Length == 0 ? (parent?.GetText() ?? text) : string.Format((parent?.GetText() ?? text), children.Cast(x => x is Enum e ? e.GetText() : x.ToString()));
        public TextAttribute(string text, params object[] children) => (this.text, this.children) = (text, children);
        public TextAttribute(string text) => this.text = text;
        public TextAttribute(Language language, string text, params object[] children) => (this.language, this.text, this.children) = (language, text, children);
        public TextAttribute(Language language, string text) => (this.language, this.text) = (language, text);
        public TextAttribute(Language language, object parent, params object[] children) => (this.language, this.parent, this.children) = (language, parent as Enum, children);
        public TextAttribute(Language language, object parent) => (this.language, this.parent) = (language, parent as Enum);
        public TextAttribute(object parent, params object[] children) => (this.parent, this.children) = (parent as Enum, children);
        public TextAttribute(object parent) => this.parent = parent as Enum;
        public static implicit operator bool(TextAttribute attribute) => attribute.language == null ? true : (forcedContext?.GetText() ?? I2.Loc.LocalizationManager.CurrentLanguage) == attribute.language.GetText();
    }

    public enum Language
    {
        [Text("English")]
        english,
        [Text("Svenska")]
        swedish,
        [Text("Français")]
        french,
        [Text("Italiano")]
        italian,
        [Text("Deutsch")]
        german,
        [Text("Español")]
        spanish,
        [Text("Polski")]
        polish,
        [Text("Português-Brasil")]
        portuguese,
        [Text("中文")]
        chinese,
        [Text("日本語")]
        japanese,
        [Text("한국어")]
        korean,
        [Text("Pусский")]
        russian
    }
}
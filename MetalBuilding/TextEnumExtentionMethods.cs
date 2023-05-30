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
        [Text("Scrap Metal")]
        ScrapMetal,
        [Text("Solid Metal")]
        SolidMetal,
        [Text("Glass")]
        Glass,
        [Text("{0}@{1}")]
        _Base_,
        [Text(Language.swedish, "Triangulärt {0}")]
        [Text(Language.french, "{0} triangulaire")]
        [Text(Language.italian, "triangolare {0}")]
        [Text(Language.german, "Dreieckiger {0}")]
        [Text(Language.spanish, "{0} triangular")]
        [Text(Language.polish, "Trójkątna {0}")]
        [Text(Language.portuguese, "triangular {0}")]
        [Text(Language.chinese, "三角{0}")]
        [Text(Language.japanese, "三角の{0}")]
        [Text(Language.korean, "삼각 {0}")]
        [Text(Language.russian, "Треугольный {0}")]
        [Text("Triangular {0}")]
        Triangle_,
        [Text(Language.swedish, "{0} halv")]
        [Text(Language.french, "Demi-{0}")]
        [Text(Language.italian, "Mezzo {0}")]
        [Text(Language.german, "{0} halbe")]
        [Text(Language.spanish, "{0} mediano")]
        [Text(Language.polish, "Pół {0}")]
        [Text(Language.portuguese, "Meia-{0}")]
        [Text(Language.chinese, "{0}半高")]
        [Text(Language.japanese, "{0}の半分")]
        [Text(Language.korean, "반 {0}")]
        [Text(Language.russian, "Половина {0}")]
        [Text("{0} Half")]
        _Half,
        [Text(Language.swedish, "Halv {0}")]
        [Text(Language.french, "Demi-{0}")]
        [Text(Language.italian, "Mezza {0}")]
        [Text(Language.german, "Halbe {0}")]
        [Text(Language.spanish, "Media {0}")]
        [Text(Language.polish, "Pół {0}")]
        [Text(Language.portuguese, "Meia-{0}")]
        [Text(Language.chinese, "半高{0}")]
        [Text(Language.japanese, "半分の{0}")]
        [Text(Language.korean, "반 {0}")]
        [Text(Language.russian, "Половина {0}")]
        [Text("Half {0}")]
        Half_,
        [Text(Language.swedish, "Förser extra våningar med stöd")]
        [Text(Language.french, "Permet de soutenir les étages supplémentaires.")]
        [Text(Language.italian, "Sostiene gli altri piani.")]
        [Text(Language.german, "Eine Stütze für zusätzliche Etagen.")]
        [Text(Language.spanish, "Proporciona un soporte para construir pisos adicionales.")]
        [Text(Language.polish, "Podpiera dodatkowe piętra.")]
        [Text(Language.portuguese, "Pode apoiar andares adicionais.")]
        [Text(Language.chinese, "支撑扩建的房层。")]
        [Text(Language.japanese, "追加のフロアを支えます。")]
        [Text(Language.korean, "추가로 만든 바닥을 지지합니다.")]
        [Text(Language.russian, "Обеспечивают поддержку новых этажей.")]
        [Text("Provides support for additional floors.")]
        _Support,
        [Text(Language.swedish, "Täcker ditt huvud.")]
        [Text(Language.french, "Vous protège.")]
        [Text(Language.italian, "Per avere qualcosa sopra la testa.")]
        [Text(Language.german, "Ein Dach über dem Kopf.")]
        [Text(Language.spanish, "Te mantiene a cubierto.")]
        [Text(Language.polish, "Chroni twoją głowę.")]
        [Text(Language.portuguese, "Protege sua cabeça.")]
        [Text(Language.chinese, "为你遮风挡雨。")]
        [Text(Language.japanese, "頭上をカバーします。")]
        [Text(Language.korean, "머리를 덮습니다.")]
        [Text(Language.russian, "Крыша над головой.")]
        [Text("Covers your head.")]
        _Roof,
        [Text(Language.swedish, _Base_, "{0}-format {1}", _Roof)]
        [Text(Language.french, _Base_, "Jonction en {0} en {1}", _Roof)]
        [Text(Language.italian, _Base_, "Giunzione a {0} in {1}", _Roof)]
        [Text(Language.german, _Base_, "{1} {0}-Verbindung", _Roof)]
        [Text(Language.spanish, _Base_, "Unión en forma de {0} de {1}", _Roof)]
        [Text(Language.polish, _Base_, "{1} {0}", _Roof)]
        [Text(Language.portuguese, _Base_, "Junção em {0} de {1}", _Roof)]
        [Text(Language.chinese, _Base_, "{1}{0}型交叉", _Roof)]
        [Text(Language.japanese, _Base_, "{1}の{0}字屋根", _Roof)]
        [Text(Language.korean, _Base_, "{1} {0} 접합부", _Roof)]
        [Text(Language.russian, _Base_, "{1} {0}-соединение", _Roof)]
        [Text(_Base_, "{1} {0}-Junction", _Roof)]
        _RoofJunction,
        [Text(Language.swedish, _Base_, "Byt ut till {0}", "")]
        [Text(Language.french, _Base_, "Remplacer par du {0}", "")]
        [Text(Language.italian, _Base_, "Sostituisci con {0}", "")]
        [Text(Language.german, _Base_, "Ersetzen durch {0}", "")]
        [Text(Language.spanish, _Base_, "Reemplazar por {0}", "")]
        [Text(Language.polish, _Base_, "Zastąp {0}", "")]
        [Text(Language.portuguese, _Base_, "Substituir por {0}", "")]
        [Text(Language.chinese, _Base_, "{0}替换", "")]
        [Text(Language.japanese, _Base_, "{0}と置き換える", "")]
        [Text(Language.korean, _Base_, "{0} 교체", "")]
        [Text(Language.russian, _Base_, "Заменить {0}", "")]
        [Text(_Base_,"Replace with {0}","")]
        Upgrade,
        [Text(Language.swedish, _Base_, "{0} Grund", "Används för att expandera din flotte på bottenvåningen.")]
        [Text(Language.french, _Base_, "Fondation {0}", "Permet d'agrandir votre radeau au niveau inférieur.")]
        [Text(Language.italian, _Base_, "Fondamenta {0}", "Usale per espandere la zattera al piano inferiore.")]
        [Text(Language.german, _Base_, "{0} Fundament", "Wird gebraucht, um dein Floß im unteren Stockwerk zu erweitern.")]
        [Text(Language.spanish, _Base_, "Cimientos {0}", "Sirven para expandir la base o planta baja de tu balsa.")]
        [Text(Language.polish, _Base_, "{0} Podpory", "Pozwalają rozszerzać dolny pokład twojej tratwy.")]
        [Text(Language.portuguese, _Base_, "Lastro {0}", "Para expandir a sua jangada no andar de baixo.")]
        [Text(Language.chinese, _Base_, "{0}基础", "用来扩建木筏的底层结构。")]
        [Text(Language.japanese, _Base_, "{0}土台", "いかだの底を拡げるのに使います。")]
        [Text(Language.korean, _Base_, "{0} 토대", "바닥에서 뗏목을 넓힐 때 사용합니다.")]
        [Text(Language.russian, _Base_, "{0} Фундамент", "Позволяет достраивать нижний этаж плота.")]
        [Text(_Base_,"{0} Foundation", "Used to expand your raft on the bottom floor.")]
        Foundation,
        [Text(Foundation, Triangle_)]
        TriangleFoundation,
        [Text(Language.swedish, _Base_, "{0}golv", "Används för att bygga extra våningar och tak. Kan inte byggas mitt i luften.")]
        [Text(Language.french, _Base_, "Parquet en {0}", "Permet de construire des étages et des toits supplémentaires. Doit être posé sur une structure.")]
        [Text(Language.italian, _Base_, "Pavimento in {0}", "Usalo per creare piani addizionali e un tetto. Va sostenuto da qualcosa, non dall'aria.")]
        [Text(Language.german, _Base_, "{0}boden", "Nötig für den Bau zusätzlicher Etagen und Dächer. Kann nicht in der Luft gebaut werden.")]
        [Text(Language.spanish, _Base_, "Suelo de {0}", "Sirve para construir pisos adicionales y el tejado. No se puede construir en medio del aire.")]
        [Text(Language.polish, _Base_, "{0} podłoga", "Wykorzystywana do budowy kolejnych pięter i dachu. Nie można jej stawiać w powietrzu.")]
        [Text(Language.portuguese, _Base_, "Piso de {0}", "Para construir andares adicionais e teto.")]
        [Text(Language.chinese, _Base_, "{0}地板", "用来建更高房层和屋顶。不能作为木筏屋的第一层。")]
        [Text(Language.japanese, _Base_, "{0}フロア", "追加のフロアや屋根を作るのに使います。空気が薄いと作ることができません。")]
        [Text(Language.korean, _Base_, "{0} 바닥", "바닥과 지붕을 추가로 만들 때 사용합니다. 공기가 희박한 곳에서 만들 수 없습니다.")]
        [Text(Language.russian, _Base_, "{0} настил", "Позволяет достраивать новые этажи и крышу. Нельзя использовать без основы.")]
        [Text(_Base_, "{0} Floor", "Used to build additional floors and roof. Cannot be built in thin air.")]
        Floor,
        [Text(Floor, Triangle_)]
        TriangleFloor,
        [Text(Language.swedish, _Base_, "{0}vägg", _Support)]
        [Text(Language.french, _Base_, "Mur en {0}", _Support)]
        [Text(Language.italian, _Base_, "Parete in {0}", _Support)]
        [Text(Language.german, _Base_, "{0}wand", _Support)]
        [Text(Language.spanish, _Base_, "Pared de {0}", _Support)]
        [Text(Language.polish, _Base_, "{0} ściana", _Support)]
        [Text(Language.portuguese, _Base_, "Parede de {0}", _Support)]
        [Text(Language.chinese, _Base_, "{0}墙", _Support)]
        [Text(Language.japanese, _Base_, "{0}の壁", _Support)]
        [Text(Language.korean, _Base_, "{0} 벽", _Support)]
        [Text(Language.russian, _Base_, "{0} стена", _Support)]
        [Text(_Base_, "{0} Wall", _Support)]
        Wall,
        [Text(Wall, Half_)]
        WallHalf,
        [Text(Language.swedish, _Base_, "Pyramidformad {0}vägg", "Fyller i pyramidformade hål.")]
        [Text(Language.french, _Base_, "Mur en {0} pyramidal", "Permet de boucher les trous pyramidaux.")]
        [Text(Language.italian, _Base_, "Parete piramidale in {0}", "Riempie i buchi a forma piramidale.")]
        [Text(Language.german, _Base_, "Pyramidenförmige {0}wand", "Füllt pyramidenförmige Löcher.")]
        [Text(Language.spanish, _Base_, "Pared de {0} piramidal", "Rellena agujeros piramidales.")]
        [Text(Language.polish, _Base_, "Piramidalna {0} ściana", "Wypełnia dziury w kształcie piramidy.")]
        [Text(Language.portuguese, _Base_, "Parede piramidal de {0}", "Cobre buracos piramidais.")]
        [Text(Language.chinese, _Base_, "角锥{0}墙", "")]
        [Text(Language.japanese, _Base_, "ピラミッド型の{0}の壁", "ピラミッド型の穴にはまります。")]
        [Text(Language.korean, _Base_, "피라미드 {0} 벽", "피라미드 구멍을 채웁니다.")]
        [Text(Language.russian, _Base_, "Пирамидальная {0} стена", "Заполняет пирамидальные промежутки.")]
        [Text(_Base_, "Pyramid {0} Wall","Fills pyramid holes.")]
        WallV,
        [Text(Language.swedish, _Base_, "Triangelformad {0}vägg", "Fyller i triangelformade hål.")]
        [Text(Language.french, _Base_, "Mur en {0} triangulaire", "Permet de boucher les trous triangulaires.")]
        [Text(Language.italian, _Base_, "Parete triangolare in {0}", "Riempie i buchi a forma triangolare.")]
        [Text(Language.german, _Base_, "Dreieckige {0}wand", "Füllt dreieckige Löcher.")]
        [Text(Language.spanish, _Base_, "Pared triangular de {0}", "Rellena huecos triangulares.")]
        [Text(Language.polish, _Base_, "Trójkątna {0} ściana", "Wypełnia trójkątne dziury.")]
        [Text(Language.portuguese, _Base_, "Parede triangular de {0}", "Cobre buracos triangulares.")]
        [Text(Language.chinese, _Base_, "三角{0}墙", "能装在三角形的洞里。")]
        [Text(Language.japanese, _Base_, "三角の{0}の壁", "三角の穴にはまります。")]
        [Text(Language.korean, _Base_, "삼각 {0} 벽", "삼각형 구멍을 채웁니다.")]
        [Text(Language.russian, _Base_, "Треугольная {0} стена", "Заполняет треугольные промежутки.")]
        [Text(_Base_, "Triangular {0} Wall", "Fills triangular holes.")]
        WallSlope,
        [Text(Language.swedish, _Base_, "{0}staket", "Ser till att du inte ramlar av din flotte.")]
        [Text(Language.french, _Base_, "Clôture en {0}", "Vous évite de tomber de votre radeau.")]
        [Text(Language.italian, _Base_, "Balaustra in {0}", "Ti evita di cadere dalla zattera.")]
        [Text(Language.german, _Base_, "{0}zaun", "Bewahrt dich davor, vom Floß zu fallen.")]
        [Text(Language.spanish, _Base_, "Valla de {0}", "Evita que te caigas por la borda.")]
        [Text(Language.polish, _Base_, "Ogrodzenie z {0}", "Zabezpiecza przed wypadnięciem z tratwy.")]
        [Text(Language.portuguese, _Base_, "Cerca de {0}", "Evita que você caia da jangada.")]
        [Text(Language.chinese, _Base_, "{0}栅栏", "让你不会从木筏上掉下去。")]
        [Text(Language.japanese, _Base_, "{0}のフェンス", "いかだから落ちるのを防ぎます。")]
        [Text(Language.korean, _Base_, "{0} 울타리", "뗏목에서 떨어지지 않도록 합니다.")]
        [Text(Language.russian, _Base_, "{0} ограждение", "Не дает упасть за борт.")]
        [Text(_Base_, "{0} Fence", "Keeps you from falling off the raft.")]
        Fence,
        [Text(Language.swedish, _Base_, "{0}grind", _Support)]
        [Text(Language.french, _Base_, "Portail en {0}", _Support)]
        [Text(Language.italian, _Base_, "Cancello in {0}", _Support)]
        [Text(Language.german, _Base_, "{0}tor", _Support)]
        [Text(Language.spanish, _Base_, "Verja de {0}", _Support)]
        [Text(Language.polish, _Base_, "{0} brama", _Support)]
        [Text(Language.portuguese, _Base_, "Portão de {0}", _Support)]
        [Text(Language.chinese, _Base_, "{0}大门", _Support)]
        [Text(Language.japanese, _Base_, "{0}の門", _Support)]
        [Text(Language.korean, _Base_, "{0} 출입구", _Support)]
        [Text(Language.russian, _Base_, "{0} ворота", _Support)]
        [Text(_Base_, "{0} Gate", _Support)]
        Gate,
        [Text(Language.swedish, _Base_, "{0}dörr", _Support)]
        [Text(Language.french, _Base_, "Porte en {0}", _Support)]
        [Text(Language.italian, _Base_, "Porta in {0}", _Support)]
        [Text(Language.german, _Base_, "{0}tür", _Support)]
        [Text(Language.spanish, _Base_, "Puerta de {0}", _Support)]
        [Text(Language.polish, _Base_, "{0} drzwi", _Support)]
        [Text(Language.portuguese, _Base_, "Porta de {0}", _Support)]
        [Text(Language.chinese, _Base_, "{0}门", _Support)]
        [Text(Language.japanese, _Base_, "{0}のドア", _Support)]
        [Text(Language.korean, _Base_, "{0} 문", _Support)]
        [Text(Language.russian, _Base_, "{0} дверь", _Support)]
        [Text(_Base_, "{0} Door", _Support)]
        Door,
        [Text(Language.swedish, _Base_, "{0}fönster", _Support)]
        [Text(Language.french, _Base_, "Fenêtre en {0}", _Support)]
        [Text(Language.italian, _Base_, "Finestra in {0}", _Support)]
        [Text(Language.german, _Base_, "{0}fenster", _Support)]
        [Text(Language.spanish, _Base_, "Ventana de {0}", _Support)]
        [Text(Language.polish, _Base_, "{0} okno", _Support)]
        [Text(Language.portuguese, _Base_, "Janela de {0}", _Support)]
        [Text(Language.chinese, _Base_, "{0}窗", _Support)]
        [Text(Language.japanese, _Base_, "{0}の窓", _Support)]
        [Text(Language.korean, _Base_, "{0} 창", _Support)]
        [Text(Language.russian, _Base_, "{0} окно", _Support)]
        [Text(_Base_, "{0} Window", _Support)]
        Window,
        [Text(Window, Half_)]
        WindowHalf,
        [Text(Language.swedish, _Base_, "{0}tak", _Roof)]
        [Text(Language.french, _Base_, "Toit en {0}", _Roof)]
        [Text(Language.italian, _Base_, "Tetto in {0}", _Roof)]
        [Text(Language.german, _Base_, "{0}dach", _Roof)]
        [Text(Language.spanish, _Base_, "Tejado de {0}", _Roof)]
        [Text(Language.polish, _Base_, "{0} dach", _Roof)]
        [Text(Language.portuguese, _Base_, "Teto de {0}", _Roof)]
        [Text(Language.chinese, _Base_, "{0}制屋顶", _Roof)]
        [Text(Language.japanese, _Base_, "{0}の屋根", _Roof)]
        [Text(Language.korean, _Base_, "{0} 지붕", _Roof)]
        [Text(Language.russian, _Base_, "{0} крыша", _Roof)]
        [Text(_Base_, "{0} Roof", _Roof)]
        RoofStraight,
        [Text(Language.swedish, _Base_, "{0}takhörna", _Roof)]
        [Text(Language.french, _Base_, "Angle de toit en {0}", _Roof)]
        [Text(Language.italian, _Base_, "Angolo per tetto in {0}", _Roof)]
        [Text(Language.german, _Base_, "{0}dachecke", _Roof)]
        [Text(Language.spanish, _Base_, "Esquina de tejado de {0}", _Roof)]
        [Text(Language.polish, _Base_, "Róg {0} dachu", _Roof)]
        [Text(Language.portuguese, _Base_, "Teto de {0} de canto", _Roof)]
        [Text(Language.chinese, _Base_, "{0}制屋顶外延", _Roof)]
        [Text(Language.japanese, _Base_, "{0}の屋根の角", _Roof)]
        [Text(Language.korean, _Base_, "{0} 지붕 모서리", _Roof)]
        [Text(Language.russian, _Base_, "{0} угол крыши", _Roof)]
        [Text(_Base_, "{0} Roof Corner", _Roof)]
        RoofCorner,
        [Text(Language.swedish, RoofCorner, "Inverterad {0}")]
        [Text(Language.french, RoofCorner, "inversé {0}")]
        [Text(Language.italian, RoofCorner, "{0} invertito")]
        [Text(Language.german, RoofCorner, "umgekehrte {0}")]
        [Text(Language.spanish, _Base_, "Esquina invertida de tejado de {0}", _Roof)]
        [Text(Language.polish, _Base_, "Odwrócony róg {0} dachu", _Roof)]
        [Text(Language.portuguese, _Base_, "Teto de {0} de canto invertido", _Roof)]
        [Text(Language.chinese, RoofCorner, "翻转的{0}")]
        [Text(Language.japanese, RoofCorner, "逆さまの{0}")]
        [Text(Language.korean, RoofCorner, "뒤집힌 {0}")]
        [Text(Language.russian, RoofCorner, "Выгнутый {0}")]
        [Text(RoofCorner, "Inverted {0}")]
        RoofCornerInverted,
        [Text(Language.swedish, _Base_, "Pyramidformat {0}tak", _Roof)]
        [Text(Language.french, _Base_, "Toit pyramidal en {0}", _Roof)]
        [Text(Language.italian, _Base_, "Tetto in {0} piramidale", _Roof)]
        [Text(Language.german, _Base_, "{0}dachpyramide", _Roof)]
        [Text(Language.spanish, _Base_, "Tejado de {0} piramidal", _Roof)]
        [Text(Language.polish, _Base_, "Piramida {0} dachu", _Roof)]
        [Text(Language.portuguese, _Base_, "Teto piramidal de {0}", _Roof)]
        [Text(Language.chinese, _Base_, "{0}屋顶角锥", _Roof)]
        [Text(Language.japanese, _Base_, "ピラミッド型の{0}の屋根", _Roof)]
        [Text(Language.korean, _Base_, "{0} 지붕 피라미드", _Roof)]
        [Text(Language.russian, _Base_, "{0} пирамида для крыши", _Roof)]
        [Text(_Base_, "{0} Roof Pyramid", _Roof)]
        RoofV0,
        [Text(Language.swedish, _Base_, "{0}slutbit", _Roof)]
        [Text(Language.french, _Base_, "Bouchon d'extrémité en {0} pour toit", _Roof)]
        [Text(Language.italian, _Base_, "Colmo per tetto in {0}", _Roof)]
        [Text(Language.german, _Base_, "{0}dachortgang", _Roof)]
        [Text(Language.spanish, _Base_, "Remate de tejado de {0}", _Roof)]
        [Text(Language.polish, _Base_, "Zakończenie kalenicy {0} dachu", _Roof)]
        [Text(Language.portuguese, _Base_, "Final de cumeeira de teto de {0}", _Roof)]
        [Text(Language.chinese, _Base_, "{0}屋顶封端", _Roof)]
        [Text(Language.japanese, _Base_, "{0}の屋根の棟", _Roof)]
        [Text(Language.korean, _Base_, "{0} 지붕 마감부", _Roof)]
        [Text(Language.russian, _Base_, "{0} заглушка для крыши", _Roof)]
        [Text(_Base_, "{0} Roof Endcap", _Roof)]
        RoofV1,
        [Text(Language.swedish, _Base_, "Dubbelt {0}tak", _Roof)]
        [Text(Language.french, _Base_, "Double toit en {0}", _Roof)]
        [Text(Language.italian, _Base_, "Doppio tetto in {0}", _Roof)]
        [Text(Language.german, _Base_, "{0}giebeldach", _Roof)]
        [Text(Language.spanish, _Base_, "Tejado hastial de {0}", _Roof)]
        [Text(Language.polish, _Base_, "{0} dach dwuspadowy", _Roof)]
        [Text(Language.portuguese, _Base_, "Telhado de {0} duplo", _Roof)]
        [Text(Language.chinese, _Base_, "{0}人字屋顶", _Roof)]
        [Text(Language.japanese, _Base_, "{0}の切妻屋根", _Roof)]
        [Text(Language.korean, _Base_, "{0} 이중 지붕", _Roof)]
        [Text(Language.russian, _Base_, "{0} двойная крыша", _Roof)]
        [Text(_Base_, "{0} Double Roof", _Roof)]
        RoofV2I,
        [Text(Language.polish, _RoofJunction, "naroże", "{0}")]
        [Text(_RoofJunction, "L", "{0}")]
        RoofV2L,
        [Text(Language.polish, _RoofJunction, "trójnik", "{0}")]
        [Text(_RoofJunction, "T", "{0}")]
        RoofV3,
        [Text(Language.polish, _RoofJunction, "krzyżak", "{0}")]
        [Text(_RoofJunction, "X", "{0}")]
        RoofV4,
        [Text(Language.swedish, _Base_, "{0} träpelare", _Support)]
        [Text(Language.french, _Base_, "Poteau en {0}", _Support)]
        [Text(Language.italian, _Base_, "Pilastro in {0}", _Support)]
        [Text(Language.german, _Base_, "{0}säule", _Support)]
        [Text(Language.spanish, _Base_, "Pilar de {0}", _Support)]
        [Text(Language.polish, _Base_, "{0} filar", _Support)]
        [Text(Language.portuguese, _Base_, "Coluna de {0}", _Support)]
        [Text(Language.chinese, _Base_, "{0}柱子", _Support)]
        [Text(Language.japanese, _Base_, "{0}の柱", _Support)]
        [Text(Language.korean, _Base_, "{0} 기둥", _Support)]
        [Text(Language.russian, _Base_, "{0} колонна", _Support)]
        [Text(_Base_, "{0} Pillar", _Support)]
        Pillar,
        [Text(Pillar, Half_)]
        PillarHalf,
        [Text(Language.swedish, Pillar, "Horisontell {0}")]
        [Text(Language.french, Pillar, "{0} horizontal")]
        [Text(Language.italian, Pillar, "{0} orizzontale")]
        [Text(Language.german, Pillar, "Horizontale {0}")]
        [Text(Language.spanish, Pillar, "{0} horizontales")]
        [Text(Language.polish, "Poziomy {0}", Pillar)]
        [Text(Language.portuguese, Pillar, "{0} horizontal")]
        [Text(Language.chinese, Pillar, "水平{0}")]
        [Text(Language.japanese, Pillar, "水平の{0}")]
        [Text(Language.korean, Pillar, "수평 {0}")]
        [Text(Language.russian, _Base_, "{0} балка", _Support)]
        [Text(Pillar, "Horizontal {0}")]
        PillarHorizontal,
        [Text(PillarHorizontal, Half_)]
        PillarHorizontalHalf,
        [Text(Language.swedish, _Base_, "{0} Stege", "Ett bra sätt att gå mellan våningar på.")]
        [Text(Language.french, _Base_, "Échelle en {0}", "Pratique pour monter et descendre.")]
        [Text(Language.italian, _Base_, "Scala in {0}", "Un ottimo modo per salire e scendere.")]
        [Text(Language.german, _Base_, "{0}leiter", "Ein guter Weg nach oben und unten.")]
        [Text(Language.spanish, _Base_, "Escalera de {0}", "Una buena manera de subir y bajar.")]
        [Text(Language.polish, _Base_, "{0} drabina", "Można po niej wygodnie wchodzić i schodzić.")]
        [Text(Language.portuguese, _Base_, "Escada {0}", "Um bom jeito de subir e descer.")]
        [Text(Language.chinese, _Base_, "{0}梯子", "上下楼的好选择。")]
        [Text(Language.japanese, _Base_, "{0}のはしご", "上り下りに良いです。")]
        [Text(Language.korean, _Base_, "{0} 사다리", "위아래로 이동할 때 필요합니다.")]
        [Text(Language.russian, _Base_, "{0} стремянка", "Удобно подниматься и спускаться.")]
        [Text(_Base_, "{0} Ladder", "A good way to go up and down.")]
        Ladder,
        [Text(Ladder, _Half)]
        LadderHalf,
        [Text(Language.swedish, _Base_, "Upphöjt {0} golv", "Ett golv som är en halv vägghöjd.")]
        [Text(Language.french, _Base_, "Sol surélevé {0}", "Situé à mi-hauteur d'un mur.")]
        [Text(Language.italian, _Base_, "Piano rialzato {0}", "Un piano alto quanto mezza parete.")]
        [Text(Language.german, _Base_, "{0} erhöhter Boden", "Ein Boden, der halb so hoch ist wie eine Wand.")]
        [Text(Language.spanish, _Base_, "Suelo {0} elevado", "Suelo con una altura media.")]
        [Text(Language.polish, _Base_, "Podniesiona {0} podłoga", "Podłoga wyrastająca z połowy wysokości ściany.")]
        [Text(Language.portuguese, _Base_, "Piso {0} levantado", "Tem metade da altura de uma parede.")]
        [Text(Language.chinese, _Base_, "{0}地台", "有半墙高。")]
        [Text(Language.japanese, _Base_, "{0}の上げ底", "壁の半分の高さのフロア。")]
        [Text(Language.korean, _Base_, "올라온 {0} 바닥", "벽의 절반 높이 바닥입니다.")]
        [Text(Language.russian, _Base_, "Приподнятый {0} пол", "Пол высотой до середины стены.")]
        [Text(_Base_, "Raised {0} Floor", "A floor half the height of a wall.")]
        FloorHalf,
        [Text(FloorHalf, Triangle_)]
        TriangleFloorHalf,
        [Text(Language.swedish, _Base_, "{0} trappa", "Perfekt för att gå från en våning till en annan.")]
        [Text(Language.french, _Base_, "Escalier {0}", "Permet de passer d'un étage à l’autre.")]
        [Text(Language.italian, _Base_, "Scala {0}", "L'ideale per passare da un piano all'altro.")]
        [Text(Language.german, _Base_, "{0} Treppe", "Großartig, um von einer Etage zur anderen zu gelangen.")]
        [Text(Language.spanish, _Base_, "Escaleras {0}", "Van genial para moverse de un piso a otro.")]
        [Text(Language.polish, _Base_, "{0} rampa", "Ułatwia poruszanie się między piętrami.")]
        [Text(Language.portuguese, _Base_, "Escada {0}", "Ótima para ir de um andar para o outro.")]
        [Text(Language.chinese, _Base_, "{0}楼梯", "很方便上下楼。")]
        [Text(Language.japanese, _Base_, "{0}の階段", "階を移動するのに最適です。")]
        [Text(Language.korean, _Base_, "{0} 계단", "")]
        [Text(Language.russian, _Base_, "{0} лестница", "Нужна, чтобы ходить между этажами.")]
        [Text(_Base_, "{0} Stair", "Great to get from one floor to another.")]
        Stair,
        [Text(Language.swedish, _Base_, "{0} halvtrappa", "Tillåter dig att nå en halvväggs höjd uppåt.")]
        [Text(Language.french, _Base_, "Demi-escalier {0}", "Monte jusqu'à mi-hauteur d'un mur.")]
        [Text(Language.italian, _Base_, "Mezza scala {0}", "Permette di raggiungere l'altezza di mezza parete.")]
        [Text(Language.german, _Base_, "{0} halbe Treppe", "Reicht eine halbe Wandhöhe nach oben.")]
        [Text(Language.spanish, _Base_, "Escaleras {0} medianas", "Te permiten alcanzar una altura media.")]
        [Text(Language.polish, _Base_, "{0} niska rampa", "Pozwala sięgnąć połowy wysokości ściany.")]
        [Text(Language.portuguese, _Base_, "Meia-escada {0}", "Alcança a metade da altura de uma parede.")]
        [Text(Language.chinese, _Base_, "{0}半高楼梯", "方便你站到半墙高的地方。")]
        [Text(Language.japanese, _Base_, "半分の{0}の階段", "壁の半分の高さまで上がることができます。")]
        [Text(Language.korean, _Base_, "반 {0} 계단", "벽 높이의 절반에 닿을 수 있습니다.")]
        [Text(Language.russian, _Base_, "Половина {0} лестницы", "Можно подняться до середины стены.")]
        [Text(_Base_, "{0} Half Stair", "Allows you to reach half a wall height up.")]
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
using System.Collections.Generic;
using System;
namespace LogicoNuevo
{
    public static class ListaCartas
    {
        public static List<Carta> cartas { get; private set; }
        public static void Iniciar()
        {
            LlenarListaDeCartas();
            ListaMazos.Iniciar();
            ListaDeCantidades.LLenar();
        }
        static void LlenarListaDeCartas()
        {
            cartas = new List<Carta>();              // Codigo  |  Tipo     | Implementada | Imagen |Probada |  Vacio  | Terminada | Efectos Visual | Lista Para Usar
            //.......................................//_____________________________________________________________________________________________|__________________
            cartas.Add(new AndOneForMe());           //   0     |  Hechizo  |      no      |   si   |   no   |         |           |                |
            cartas.Add(new Abbadon());               //   1     |  Heroe    |  Incompleta  |   si   |   si   |         |    no     |      ok        |1X
            cartas.Add(new ActOfDefiance());         //   2     |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new AghanimSanctum());        //   3     |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new AllseeingOneFavor());     //   4     |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new AltarOfTheMadMoon());     //   5     |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new Annilation());            //   6     |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new AphoticShield());         //   7     |  Hechizo  |  Incompleta  |   si   |   si   |         |           |                |
            cartas.Add(new AphoteosisBalde());       //   8     |  Item     |      no      |   si   |   si   |         |           |                |
            cartas.Add(new ArcaneAssault());         //   9     |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new ArcaneCensure());         //   10    |  Hechizo  |      si      |   si   |   si   |         |    si     |    falta       |10X
            cartas.Add(new ArmTheRebellion());       //   11    |  Hechizo  |  Incompleta  |   si   |   si   |         |           |                |11X
            cartas.Add(new AssassinApprentice());    //   12    |  Unidad   |  Incompleta  |   si   |   si   |         |   no      |                |12X
            cartas.Add(new AssassinShadow());        //   13    |  Unidad   |      si      |   si   |   si   |         |   si      |      ok        |13
            cartas.Add(new AssassinVeil());          //   14    |  Item     |  Incompleta  |   si   |   si   |         |           |                |
            cartas.Add(new Assassinate());           //   15    |  Hechizo  |      si      |   si   |   si   |         |   si      | ok             |15
            cartas.Add(new AssaultLadders());        //   16    |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new AssuredDestruction());    //   17    |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new AstralImprisonment());    //   18    |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new AtAnyCost());             //   19    |  Hechizo  |  Incompleta  |   si   |   si   |         |           |                |19X
            cartas.Add(new AvernusBlessingt());      //   20    |  Hechizo  |      si      |   si   |   si   |         |   si      |ok              |20
            cartas.Add(new Axe());                   //   21    |  Heroe    |      si      |   si   |   si   |         |   si      |ok              |21X
            cartas.Add(new BallLightning());         //   22    |  Hechizo  |      no      |   si   |   no   |         |   no      |                |
            cartas.Add(new BarbedMail());            //   23    |  Item     |      si      |   si   |   si   |         |   si      |ok              |23
            cartas.Add(new Barracks());              //   24    |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new BattlefieldControl());    //   25    |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new Vacio());                 //         |           |              |        |        | X       |           |                |
            cartas.Add(new Bellow());                //   27    |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new BerserkerCall());         //   28    |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new BetterLateThanNever());   //   29    |  Hechizo  |  Incompleta  |   si   |   si   |         |           |                |
            cartas.Add(new BitterEnemies());         //   30    |  Hechizo  |      no      |   si   |   si   |         |           |                |
            cartas.Add(new BladeOfTheVigil());       //   31    |  Item     |      si      |   si   |   si   |         |    si     |ok              |31
            cartas.Add(new Vacio());                 //         |           |              |        |        | X       |           |                |
            cartas.Add(new BloodRage());             //   33    |  Hechizo  |      si      |   si   |   si   |         |   si      |ok              |33
            cartas.Add(new BloodSeeker());           //   34    |  Heroe    |  Incompleta  |   si   |   si   |         |   no      |                |
            cartas.Add(new BoltOfDamocles());        //   35    |  Hechizo  |  Incompleta  |   si   |   si   |         |           |falta           |35X
            cartas.Add(new Vacio());                 //         |           |              |        |        | X       |           |                |
            cartas.Add(new BountyHunter());          //   37    |  Heroe    |  Incompleta  |   si   |   si   |         |   no      |                |37X
            cartas.Add(new Vacio());                 //         |           |              |        |        | X       |           |                |
            cartas.Add(new Bristleback());           //   39    |           |  Incompleta  |   si   |   si   |         |           |                |39X
            cartas.Add(new Broadsword());            //   40    |   item    |              |        |        | X       |           |                |
            cartas.Add(new BronzeLegionare());       //   41    |           |      si      |   si   |   si   |         |   si      |ok              |41
            cartas.Add(new BurningOil());            //   42    |           |      no      |   si   |   si   |         |           |                |
            cartas.Add(new BuyingTime());            //   43    |           |      no      |   si   |   si   |         |           |                |
            cartas.Add(new CallTheReserves());       //   44    |  Hechizo  |  Incompleta  |   si   |   si   |         |           |                |44X
            cartas.Add(new CaugthUnprepared());      //   45    |           |      no      |   si   |   si   |         |           |                |
            cartas.Add(new CentaurHunter());         //   46    |  Unidad   |      si      |   si   |   si   |         |    si     |ok              |46
            cartas.Add(new CentaurWarruner());       //   47    |  Heroe    |      si      |   si   |   si   |         |           |                |47
            cartas.Add(new ChainFrost());            //   48    |  Hechizo  |      si      |   si   |   si   |         |    si     | ok             |48
            cartas.Add(new Chainmail());             //   49    |  item     |              |        |        | X       |           |                |
            cartas.Add(new ChampionOfTheAcient());   //   50    |           |              |        |        | X       |           |                |
            cartas.Add(new CheatingDeath());         //   51    |           |              |        |        | X       |           |                |
            cartas.Add(new Chen());                  //   52    |           |              |        |        | X       |           |                |
            cartas.Add(new ClaszuremeHourglass());   //   53    |           |              |        |        | X       |           |                |
            cartas.Add(new Claymore());              //   54    |           |              |        |        | X       |           |                |
            cartas.Add(new CleasingRite());          //   55    |           |              |        |        | X       |           |                |
            cartas.Add(new ClearTheDeck());          //   56    |  Hechizo  |     si       |   no   |   no   |         |    si     | no             |56
            cartas.Add(new CloakOfEndlessCarnage()); //   57    |           |              |        |        | X       |           |                |
            cartas.Add(new CollateralDamage());      //   58    |           |              |        |        | X       |           |                |
            cartas.Add(new CombatTraining());        //   59    |  Hechizo  |     si       |  no    |  no    |         |     si    |no              |59
            cartas.Add(new Compel());                //   60    |           |              |        |        | X       |           |                |
            cartas.Add(new Conflagration());         //   61    |           |              |        |        | X       |           |                |
            cartas.Add(new CoordinatedAssault());    //   62    |           |              |        |        | X       |           |                |
            cartas.Add(new CorrosiveMist());         //   63    |           |              |        |        | X       |           |                |
            cartas.Add(new CoupOfGrace());           //   64    |           |              |        |        | X       |           |                |
            cartas.Add(new CripplingBlow());         //   65    |  Hechizo  |     si       |  no    |  no    |         |     si    |no              |65
            cartas.Add(new CrystalMayden());         //   66    |           |              |        |        | X       |           |                |
            cartas.Add(new CunningPlan());           //   67    |           |              |        |        | X       |           |                |
            cartas.Add(new CurseOfAtrophy());        //   68    |  Hechizo  |     si       |  no    |  no    |         |     si    |no              |68
            cartas.Add(new CursedrSatyr());          //   69    |           |              |        |        | X       |           |                |
            cartas.Add(new DarkSeer());              //   70    |           |              |        |        | X       |           |                |
            cartas.Add(new DebbiTheCunning());       //   71    |           |              |        |        | X       |           |                |
            cartas.Add(new DefendTheWeak());         //   72    |  Hechizo  |     si       |  no    |  no    |         |     si    |no              |72
            cartas.Add(new DefensiveBloom());        //   73    |           |              |        |        | X       |           |                |
            cartas.Add(new DefensiveStance());       //   74    |  Hechizo  |     si       |  no    |  no    |         |     si    |no              |74
            cartas.Add(new DemagickingMaul());       //   75    |           |              |        |        | X       |           |                |
            cartas.Add(new DiavolicRevelation());    //   76    |           |              |        |        | X       |           |                |
            cartas.Add(new DimensionalPortal());     //   77    |           |              |        |        | X       |           |                |
            cartas.Add(new DirtyDeeds());            //   78    |           |              |        |        | X       |           |                |
            cartas.Add(new DiscipleOfNevermore());   //   79    |           |              |        |        | X       |           |                |
            cartas.Add(new DividedWeStand());        //   80    |           |              |        |        | X       |           |                |
            cartas.Add(new DivineIntervention());    //   81    |  Hechizo  |     no       |   si   |   no   |         |     no    |no              |
            cartas.Add(new DivinePurpose());         //   82    |  Hechizo  |     si       |   si   |   no   |         |     si    |no              |82
            cartas.Add(new DoubleEdge());            //   83    |  Hechizo  |     si       |   si   |   no   |         |     si    |no              |83
            cartas.Add(new DrowRanger());            //   84    |   Heroe   |     si       |   si   |   si   |         |     si    |      ok        |84
            cartas.Add(new Duel());                  //   85    |           |              |        |        | X       |           |                |
            cartas.Add(new Earthshaker());           //   86    |           |              |        |        | X       |           |                |
            cartas.Add(new EchoSlam());              //   87    |           |              |        |        | X       |           |                |
            cartas.Add(new Eclipse());               //   88    |           |              |        |        | X       |           |                |
            cartas.Add(new EmisaryOfTheQuorum());    //   89    |           |              |        |        | X       |           |                |
            cartas.Add(new Empower());               //   90    |           |              |        |        | X       |           |                |
            cartas.Add(new Enchantres());            //   91    |   Heroe   |     si       |   si   |   no   |         |     si    |      ok        |91
            cartas.Add(new EnoughMagic());           //   92    |           |              |        |        | X       |           |                |
            cartas.Add(new Enrage());                //   93    |           |              |        |        | X       |           |                |
            cartas.Add(new EscapeRoute());           //   94    |           |              |        |        | X       |           |                |
            cartas.Add(new FarvhanTheDreamer());     //   95    |           |              |        |        | X       |           |                |
            cartas.Add(new FigthThroughThePain());   //   96    |           |              |        |        | X       |           |                |
            cartas.Add(new FightingInstinct());      //   97    |  Hechizo  |     si       |  no    |  no    |         |     si    |no              |97
            cartas.Add(new FogOfWar());              //   98    |           |              |        |        | X       |           |                |
            cartas.Add(new Foresight());             //   99    |           |              |        |        | X       |           |                |
            cartas.Add(new ForwardCharge());         //   100   |           |              |        |        | X       |           |                |
            cartas.Add(new FountainFlask());         //   101   |           |              |        |        | X       |           |                |
            cartas.Add(new FracturedTimeline());     //   102   |           |              |        |        | X       |           |                |
            cartas.Add(new FriendlyFire());          //   103   |           |              |        |        | X       |           |                |
            cartas.Add(new Frosbite());              //   104   |           |              |        |        | X       |           |                |
            cartas.Add(new FurLinedMantle());        //   105   |           |              |        |        | X       |           |                |
            cartas.Add(new Gank());                  //   106   |           |              |        |        | X       |           |                |
            cartas.Add(new GlyphOfConfusion());      //   107   |           |              |        |        | X       |           |                |
            cartas.Add(new GodStrength());           //   108   |           |              |        |        | X       |           |                |
            cartas.Add(new GoldenTicket());          //   109   |           |              |        |        | X       |           |                |
            cartas.Add(new GrandMelee());            //   110   |           |              |        |        | X       |           |                |
            cartas.Add(new Grazing());               //   111   |           |              |        |        | X       |           |                |
            cartas.Add(new Gust());                  //   112   |  Hechizo  |     si       |  no    |  no    |         |     si    |no              |112
            cartas.Add(new HandOfGod());             //   113   |           |              |        |        | X       |           |                |
            cartas.Add(new HealingSalve());          //   114   |           |              |        |        | X       |           |                |
            cartas.Add(new HeartstopperAura());      //   115   |           |              |        |        | X       |           |                |
            cartas.Add(new HellbearCrippler());      //   116   |  Unidad   |     si       |  no    |  no    |         |     si    |no              |116
            cartas.Add(new HelmOfDominator());       //   117   |           |              |        |        | X       |           |                |
            cartas.Add(new HeroCape());              //   118   |           |              |        |        | X       |           |                |
            cartas.Add(new HeoricResolve());         //   119   |           |              |        |        | X       |           |                |
            cartas.Add(new HipFire());               //   120   |           |              |        |        | X       |           |                |
            cartas.Add(new HomefieldAdvantage());    //   121   |           |              |        |        | X       |           |                |
            cartas.Add(new HornOfTheAlpha());        //   122   |           |              |        |        | X       |           |                |
            cartas.Add(new HoundOfWar());            //   123   |           |              |        |        | X       |           |                |
            cartas.Add(new HowlingMind());           //   124   |           |              |        |        | X       |           |                |
            cartas.Add(new Ignate());                //   125   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   126   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   127   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   128   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   129   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   130   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   131   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   132   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   133   |           |              |        |        | X       |           |                |
            cartas.Add(new Kanna());                 //   134   |   Heroe   |     si       |  no    |  no    |         |     si    |no              |134X
            cartas.Add(new KeefeTheBlood());         //   135   |   Heroe   |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   136   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   137   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   138   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   139   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   140   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   141   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   142   |           |              |        |        | X       |           |                |
            cartas.Add(new LegionStandartBearer());  //   143   |  Unidad   |     si       |  no    |  no    |         |     si    |no              |143
            cartas.Add(new Vacio());                 //   144   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   144   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   146   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   147   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   148   |           |              |        |        | X       |           |                |
            cartas.Add(new LoyalBeast());            //   149   |  Unidad   |     si       |  no    |  no    |         |     si    |no              |149
            cartas.Add(new Luna());                  //   150   |  Heroe    |     si       |  si    |  si    |         |     no    |no              |
            cartas.Add(new Lycan());                 //   151   |  Heroe    |              |        |        | X       |           |                |
            cartas.Add(new Magnus());                //   152   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   153   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   154   |           |              |        |        | X       |           |                |
            cartas.Add(new MarrowfellBrawler());     //   155   |  Unidad   |     si       |  no    |  no    |         |     si    |no              |155
            cartas.Add(new Vacio());                 //   156   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   157   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   158   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   159   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   160   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   161   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   162   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   163   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   164   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   165   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   166   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   167   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   168   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   169   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   170   |           |              |        |        | X       |           |                |
            cartas.Add(new OgreConscript());         //   171   |  Unidad   |     si       |  no    |  no    |         |     si    |no              |171
            cartas.Add(new Vacio());                 //   172   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   173   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   174   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   175   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   176   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   177   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   178   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   179   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   180   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   181   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   182   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   183   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   184   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   185   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   186   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   187   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   188   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   189   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   190   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   191   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   192   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   193   |           |              |        |        | X       |           |                |
            cartas.Add(new ProwlerVanguard());       //   194   |  Unidad   |     si       |  no    |  no    |         |     si    |no              |194
            cartas.Add(new Vacio());                 //   195   |           |              |        |        | X       |           |                |
            cartas.Add(new RampagingHellbear());     //   196   |  Unidad   |     si       |  no    |  no    |         |     si    |no              |196
            cartas.Add(new Vacio());                 //   197   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   198   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   199   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   200   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   201   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   202   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   203   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   204   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   205   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   206   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   207   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   208   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   209   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   210   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   211   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   212   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   213   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   214   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   215   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   216   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   217   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   218   |           |              |        |        | X       |           |                |
            cartas.Add(new RoseleafDruid());         //   219   |  unidad   |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   220   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   221   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   222   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   223   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   224   |           |              |        |        | X       |           |                |
            cartas.Add(new SavageWolf());            //   225   |  unidad   |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   226   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   227   |           |              |        |        | X       |           |                |
            cartas.Add(new SelfishCleric());         //   228   |  Unidad   |     si       |  no    |  no    |         |     si    |no              |228
            cartas.Add(new Vacio());                 //   229   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   230   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   231   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   232   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   233   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   234   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   235   |           |              |        |        | X       |           |                |
            cartas.Add(new SkywrathMage());          //   236   |   Heroe   |  incompleto  |   si   |        |         |           |                |
            cartas.Add(new Vacio());                 //   237   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   238   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   239   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   240   |           |              |        |        | X       |           |                |
            cartas.Add(new Sniper());                //   241   |   Heroe   |  incompleto  |   si   |        |         |           |                |
            cartas.Add(new Vacio());                 //   242   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   243   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   244   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   245   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   246   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   247   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   248   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   249   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   250   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   251   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   252   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   253   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   254   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   255   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   256   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   257   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   258   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   259   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   260   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   261   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   262   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   263   |           |              |        |        | X       |           |                |
            cartas.Add(new ThundergodsWrath());      //   264   |  hechizo  |              |        |        | X       |           |                |
            cartas.Add(new ThunderhideAlpha());      //   265   |  Unidad   |     si       |  no    |  no    |         |     si    |no              |265
            cartas.Add(new ThunderhidePack());       //   266   |  Unidad   |     si       |  no    |  no    |         |     si    |no              |266
            cartas.Add(new Vacio());                 //   267   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   268   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   269   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   270   |           |              |        |        | X       |           |                |
            cartas.Add(new Tinker());                //   271   |   Heroe   |  incompleto  |   si   |        |         |           |                |
            cartas.Add(new Vacio());                 //   272   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   273   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   274   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   275   |           |              |        |        | X       |           |                |
            cartas.Add(new TreantProtector());       //   276   |   Heroe   |  incompleto  |   si   |        |         |           |                |
            cartas.Add(new Vacio());                 //   277   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   278   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   279   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   280   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   281   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   282   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   283   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   284   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   285   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   286   |           |              |        |        | X       |           |                |
            cartas.Add(new VerdantRefuge());         //   287   |  Hechizo  |     si       |  no    |  no    |         |     si    |no              |287
            cartas.Add(new Vacio());                 //   288   |           |              |        |        | X       |           |                |
            cartas.Add(new VhoulMartyr());           //   289   |  Unidad   |     si       |  no    |  no    |         |     si    |no              |289
            cartas.Add(new Viper());                 //   290   |   Heroe   |  incompleto  |   si   |        |         |           |                |
            cartas.Add(new Vacio());                 //   291   |           |              |        |        | X       |           |                |
            cartas.Add(new ViscousNasalGoo());       //   292   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   293   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   294   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   295   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   296   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   297   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   298   |           |              |        |        | X       |           |                |
            cartas.Add(new Vacio());                 //   299   |           |              |        |        | X       |           |                |
            cartas.Add(new Zeus());                  //   300   |   Heroe   |  incompleto  |   si   |        |         |           |                |
            cartas.Add(new Vacio());                 //   301   |           |              |        |        | X       |           |                |

        }
        public static Carta GetCarta(int n)
        {
            return Activator.CreateInstance(cartas[n].GetType()) as Carta;
        }
    }
    public static class ListaMazos
    {
        public static Mazo MazoActual;
        public static List<Mazo> mazos = new List<Mazo>();

        public static void Iniciar()
        {
            mazos = new List<Mazo>();
            MazosDefault();
        }
        static void MazosDefault()
        {
            #region Mazo Rojo-Verde Default
            Mazo temp = new Mazo("Rojo/Verde default");
            #region Heroes
            temp.AddHeroe(ListaCartas.GetCarta(95) as Heroe);
            temp.AddHeroe(ListaCartas.GetCarta(91) as Heroe);
            temp.AddHeroe(ListaCartas.GetCarta(135) as Heroe);
            temp.AddHeroe(ListaCartas.GetCarta(151) as Heroe);
            temp.AddHeroe(ListaCartas.GetCarta(47) as Heroe);
            #endregion
            #region Items
            temp.AddItems(ListaCartas.GetCarta(14) as Item);
            temp.AddItems(ListaCartas.GetCarta(23) as Item);
            temp.AddItems(ListaCartas.GetCarta(31) as Item);
            temp.AddItems(ListaCartas.GetCarta(40) as Item);
            temp.AddItems(ListaCartas.GetCarta(49) as Item);
            #endregion
            #region CartasAsignadas
            temp.AddCarta(ListaCartas.GetCarta(194));
            temp.AddCarta(ListaCartas.GetCarta(194));
            temp.AddCarta(ListaCartas.GetCarta(194));
            temp.AddCarta(ListaCartas.GetCarta(287));
            temp.AddCarta(ListaCartas.GetCarta(287));
            temp.AddCarta(ListaCartas.GetCarta(287));
            temp.AddCarta(ListaCartas.GetCarta(97));
            temp.AddCarta(ListaCartas.GetCarta(97));
            temp.AddCarta(ListaCartas.GetCarta(97));
            temp.AddCarta(ListaCartas.GetCarta(225));
            temp.AddCarta(ListaCartas.GetCarta(225));
            temp.AddCarta(ListaCartas.GetCarta(225));
            temp.AddCarta(ListaCartas.GetCarta(83));
            temp.AddCarta(ListaCartas.GetCarta(83));
            temp.AddCarta(ListaCartas.GetCarta(83));
            #endregion
            #region Agregadas
            temp.AddCarta(ListaCartas.GetCarta(289));
            temp.AddCarta(ListaCartas.GetCarta(289));
            temp.AddCarta(ListaCartas.GetCarta(289));
            temp.AddCarta(ListaCartas.GetCarta(265));
            temp.AddCarta(ListaCartas.GetCarta(265));
            temp.AddCarta(ListaCartas.GetCarta(266));
            temp.AddCarta(ListaCartas.GetCarta(266));
            temp.AddCarta(ListaCartas.GetCarta(228));
            temp.AddCarta(ListaCartas.GetCarta(228));
            temp.AddCarta(ListaCartas.GetCarta(194));
            temp.AddCarta(ListaCartas.GetCarta(194));
            temp.AddCarta(ListaCartas.GetCarta(196));
            temp.AddCarta(ListaCartas.GetCarta(196));
            temp.AddCarta(ListaCartas.GetCarta(171));
            temp.AddCarta(ListaCartas.GetCarta(171));
            temp.AddCarta(ListaCartas.GetCarta(155));
            temp.AddCarta(ListaCartas.GetCarta(155));
            temp.AddCarta(ListaCartas.GetCarta(149));
            temp.AddCarta(ListaCartas.GetCarta(149));
            temp.AddCarta(ListaCartas.GetCarta(143));
            temp.AddCarta(ListaCartas.GetCarta(143));
            temp.AddCarta(ListaCartas.GetCarta(116));
            temp.AddCarta(ListaCartas.GetCarta(116));
            temp.AddCarta(ListaCartas.GetCarta(97));
            temp.AddCarta(ListaCartas.GetCarta(97));
            temp.AddCarta(ListaCartas.GetCarta(82));
            temp.AddCarta(ListaCartas.GetCarta(82));
            temp.AddCarta(ListaCartas.GetCarta(65));
            temp.AddCarta(ListaCartas.GetCarta(65));
            temp.AddCarta(ListaCartas.GetCarta(68));
            temp.AddCarta(ListaCartas.GetCarta(68));
            temp.AddCarta(ListaCartas.GetCarta(72));
            temp.AddCarta(ListaCartas.GetCarta(72));
            temp.AddCarta(ListaCartas.GetCarta(74));
            temp.AddCarta(ListaCartas.GetCarta(74));
            temp.AddCarta(ListaCartas.GetCarta(56));
            temp.AddCarta(ListaCartas.GetCarta(56));
            temp.AddCarta(ListaCartas.GetCarta(59));
            temp.AddCarta(ListaCartas.GetCarta(59));
            temp.AddCarta(ListaCartas.GetCarta(46));
            temp.AddCarta(ListaCartas.GetCarta(46));
            temp.AddCarta(ListaCartas.GetCarta(41));
            temp.AddCarta(ListaCartas.GetCarta(41));
            temp.AddCarta(ListaCartas.GetCarta(20));
            temp.AddCarta(ListaCartas.GetCarta(20));
            temp.AddCarta(ListaCartas.GetCarta(11));
            #endregion
            mazos.Add(temp);
            #endregion

            #region Mazo Azul-Negro Default
            //No Implementado
            #endregion
        }
    }
    public static class ListaDeCantidades
    {
        public static List<int> Cantidad = new List<int>();

        public static void LLenar()
        {
            Cantidad = new List<int>();
            for (int i = 0; i < 300; i++)
            {
                Cantidad.Add(3);
            }
            CeroNoImplementadas();
            //     BaseDeDatos.CargarSalva();
        }
        static void CeroNoImplementadas()
        {
            Cantidad[1] = 0;
            Cantidad[0] = 0;
            Cantidad[21] = 0;
            Cantidad[271] = 0;
            Cantidad[37] = 0;
            Cantidad[134] = 0;
            Cantidad[150] = 0;
            Cantidad[241] = 0;
            Cantidad[236] = 0;
            Cantidad[3] = 0;
            Cantidad[4] = 0;
            Cantidad[290] = 0;
            Cantidad[5] = 0;
            Cantidad[9] = 0;
            Cantidad[43] = 0;
        }
        //void recibir array de string y convertirlo en array de int, ponerselo a una variable apara luego usarla en llenar()
    }

}
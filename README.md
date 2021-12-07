# Alkalmazásfejlesztés (VIAUMA09) házi feladat

Alapadatok:
- A csapat neve: CTORTA
- Csapattagok (név, neptun kód): Szabó Bence (C4W6E3), Hadlaczky Bence (IC0IKR) Balogh Ákos (FHX95E)
- Leadáshoz videó URL: https://youtu.be/iCH5M7ATXqg

A videó későbbi evfolyamok számára bemutatható/felhasználható videó vágásra?
- [x] Igen, névtelenül
- [ ] Igen, szerzőket megnevezve
- [ ] Nem

Leadáshoz checklist:
- 24 órával a személyes (vagy online) leadási időpont előtt 24 órával
  - Fentebb meg van adva a csapattagok neve és Neptun-kódja.
  - Videó elkészítve, benne explicit kitérve minden pontozási szempontra (lásd pontozási szempontok videó elvárásai).
  - A pontozási listában be-X-elve minden pontozási szempont, amivel foglalkoztatok.
  - A végleges verzió a master ágon van.
  - Amennyiben online időpont foglalásos bemutatás van, az időpontot le van foglalva a csapat számára.

# A házi feladat kiírása

A házi feladat célja egy diagnosztikai kliens program elkészítése egy szimulált vagy valós robothoz vagy más beágyazott rendszerhez. A kliens program grafikus felhasználói felület segítségével lehetővé teszi a robot vezérlését, valamint a tőle érkező adatok áttekinthető, hibakeresésre tényleg alkalmas megjelenítését. Szimulált robot esetén a bemutathatóság érdekében a szimulátor biztosítása (megírása, beszerzése) is a feladat része, melyre az előadáson fogunk látni példát.

## Jelentkezés módja

- A házi feladat elkészítése 3 fős csapatokban történik. Jelentkezni a 3. oktatási hét végéig kell az alábbi módon:
  - Moodle alatt az erre vonatkozó kérdőíven mindenkinek meg kell adni, hogy melyik csapatban van (mi a csapatneve).
  - A classroom.github.com rendszerben létre kell hozni a csapatot és a tagoknak be kell lépnie. Így mindenki megkapja a házi feladat elkészítésére szánt (private) repositoryt. Ehhez a meghívó URL a Moodle alatt érhető majd el.

- A 2 fős csapat is elfogadható, de a feladat mennyisége miatt 3 fő az ideális. A csapaton belüli esetleges súrlódások kezelése a csapat feladata, csak úgy, mint egy éles projektben. Az aláírás feltétele a csapatmunkában való aktív részvétel.
- Nem RobonAUT-hoz kapcsolódó házi feladatok esetében erősen javasolt, hogy beszéljetek az előadóval, nehogy véletlenül olyan megoldásba kezdjetek, amivel a szükséges pontokat nem tudjátok megszerezni.

(Sajnos élő példa miatt ha valaki a github classroomban olyan, nem szalonképes vagy offenzív csapatnevet választ, ami miatt például a github automatikusan blokkolja a tárgy organizationjét és így az összes repositoryját, az fegyelmi eljárást von maga után. De amúgy se igen akarjátok, hogy később a Google a nevetekre keresve ilyen csapatnév mellett mutasson titeket… pl. egy leendő munkáltatónak.)

## A leadás módja

A házi feladatot leadni a 14. oktatási héten kell, 2 lépésben.
- A személyes (vagy online) leadási időpont előtt 24 órával le kell adni
  - Egy max. 5 perces screen capture (vagy más módon elkészített) videót a megoldásról. (Ennek célja, hogy a leadáskor ne kelljen minden funkciót végignézni, hanem koncentrálhassunk az érdekesekre. Ezen kívül hogy ne kelljen azon aggódni, hogy például a robot éppen menni fog-e, ne kelljen hozzá feltétlenül speciális tesztpálya.) A pontozási szempontok mindegyikének explicit szerepelnie kell a videóban, a pontozási szempontnál megadott formában. A videó URL-jét ennek a fájlnak az elején kell megadni és egy böngészőben további telepítések nélkül lejátszhatónak kell lennie (pl. youtube vagy google drive megosztás).
  - A megoldásnak a repository master ágán kell lennie.
  - A leadott verzióban ebben a README.md fájlban minden olyan pontozási szempontot be kell X-elni, amire szeretnétek pontot kapni.
- A leadási időpontban személyes vagy online leadást fogunk szervezni (járványügyi szempontok függvényében), amikor megnézzük a megoldás érdekesebb részleteit és egyeztetjük a kapott pontszámot. A RobonAUT robothoz kapcsolódó megoldások kedvéért ezen az alkalmon rendelkezésre áll majd egy tesztpálya, melyen egyúttal a robotokat is lehet tesztelni, miközben megnézzük a kliens programok működését. (A leadáshoz egy saját notebookot kell hozni. Ha ez akadályba ütközik, előre jelezzétek és megoldjuk egy gépteremben.)

Azoknak a csapatoknak, melyeknek a fenti leadásokat nem sikerül határidőre teljesíteni, a pótlási héten lesz lehetőség pótleadásra, ugyanígy 2 lépésben. (A fenti 1. lépés késedelmes teljesítése esetén már csak a pótleadáson lehet leadni a házi feladatot, mivel a találkozó előtt át kell tudni nézni az előre leadott anyagokat.)

A konkrét 14. és pótlási heti időpontokat novemberben hirdetjük ki.
A félév vége és a RobonAUT verseny után (bőven hagyva időt) a kiadott repositorykat törölni fogjuk, így a verseny után érdemes egy véglegesebb repositoryt létrehozni és oda is pusholni.

## Köztes code reviewk

A félév során lesz két határidő, amikor az addig elkészült részekre lehet code reviewt kérni. Ehhez github alatt pull requestet kell készíteni és reviewernek meg kell adni a tárgyfelelőst (usernév: csorbakristof).

A code review célja a minél hasznosabb, személyes visszajelzés a csapatok számára, a saját megoldásukkal kapcsolatban. Nem kötelező, de pontozási szempont ez is.

Annak érdekében, hogy az adható visszajelzés minél hasznosabb legyen, javasoljuk, hogy a megoldásról ilyenkor is készítsetek egy rövid videót (benne a működésről, a forráskód felépítéséről, esetleges kérdéseitekről), hogy minél jobban átlássuk a megoldásotok működését. A videóra az URL-t a pull request szövegébe érdemes beleírni, mint ahogy oda való minden megjegyzés és esetleges kérdések is.

(Érdemes megemlíteni, hogy minden pull request két git branch különbségét tartalmazza. A legegyszerűbb, ha létrehoztok egy branchet a félév eleji állapotra és ehhez hasonlítjátok a beadandó állapotot.)

## Minimális követelmények (aláírás feltétele)

- Minden csapattag aktív részvétele a munkában, ami a git commit historyban is látszik (kivéve, ha páros programoztok és ezért a közös commitok egyikőtök nevében készülnek).
- A forráskód legyen áttekinthető, olvasható, esztétikus. (pl. ne legyen benne hatalmas, kikommentezett forráskód blokk, csomó üres sor egymás után, “teve” és “maci” (vagy szalonképtelen) nevű változó, ne egy óriás cpp fájlban legyen megírva az egész stb.)
- A kliens programnak C# nyelven, .NET Core alatt kell készülnie, UWP felhasználói felülettel. (A tárgy keretében UWP-vel foglalkozunk, de válaszható WPF is.)
- A házi feladathoz GIT verziókövetést kell használni a tárgy keretében létrehozott classroom.github.com-os repositoryban.
- A kliens programnak grafikus felülettel kell rendelkeznie, ami parancsokat tud küldeni és állapotot tud fogadni.
- A bemutatáskor a felhasználói felületen látszania kell, hogy a beágyazott rendszer reagál a küldött parancsokra.
- A leadáskor a master branchen lévő (végleges) verzió forduljon és működjön egy Windows 10 alapú gépen, Visual Studio 2019 alatt. Természetesen ha egy igazi robot jelenléte kell neki, akkor nem gond, ha nem működik minden funkció, de induljon el és ezt a tényt esztétikus formában jelezze. A helyes működés pedig a demó videón úgyis látszani fog.

## Pontot érő dolgok

Minden szempontnál a megadott pont a maximális adható pont, részleges megoldás kevesebb pontot is érhet. A pótleadáson maximum 50 pont szerezhető.

Architektúra, magas szintű koncepciók
- [x] 10p: MVVM architektúra (legalább 3 modell és 3 view model osztállyal) (Videóban: solution explorerben megmutatva a modell és view model osztályokat)
- Többszálúság
  - [x] 8p: Task és async-await használatával. (Videóban: forráskódban kiemelve)
  - [ ] 3p: BackgroundWorker használatával progress reporttal.
- [ ] 10p: Entity Framework használata
- [x] 5p: Hálózati kommunikáció HTTP felett
  - [x] +5p: HTTP feletti kommunikációban legalább 3 HTTP ige (get, put, delete, post stb.) használata, REST API kialakítása

Technológiák
- [ ] 5p: Canvas és Shape használata (Videóban: UI-on megmutatva)
- [x] 5p: Adatkötés használata (Videóban: xaml kód)
- [ ] 10p: Heterogén listához adatkötés (DataTemplateSelector) (Videóban: xaml kód)
- [x] 5p: Regex használat nem triviális feladatra (pl. nem Substring helyett) (Videóban: forráskód részlet)
- [ ] 5p: IValueConverter használata (Videóban: xaml kód)
- [x] 5p: ICommand (Videóban: forráskódban az ICommandot implementáló osztály)
- [ ] 5p: StaticResource használata (Videóban: xaml kód)
- [ ] 5p: Fájlba mentés és onnan betöltés (az UWP hozzáférési korlátozásokat figyelembe véve) (Videóban: használat közben a UI vagy forráskód részlet)
- [ ] 5p: Linq használata nem triviális feladatra (query vagy method syntax is lehet) (Videóban: forráskódban kiemelve)
- [x] 5p: Sorosítás JSON vagy XML formátumba (Videóban: generált XML/JSON felvillantása)
- [x] 5p: Alapos öntesztelő funkció a robot számára. A tesztet futtathatja a kliens program is, de a robot firmwareje is. A lényeg, hogy van öntesztelési funkció. (Videóban: futás közben bemutatva)
- [x] 10p: grafikon megjelenítő package (pl. oxyplot) használata nem csak alapbeállításokkal (Videóban: UI-on megmutatva)

Módszertani szempontok
- [ ] mintánként 5p: A tárgy keretében szereplő tervezési minta használata saját megvalósításban (videóban: forráskódban megmutatva). (Observer csak akkor, ha az esemény kiváltása is saját kód, pl. egy nyomógomb Click eseménykezelőjének megírása még nem elég ehhez.)
- [ ] 10p: Legalább 20% unit teszt lefedettség (Videóban: unit tesztek lefutnak és zöldek, coverage report 20% feletti számot mutat). Ha kisebb a lefedettség, arányosan kevesebb pontot ér. (UWP alkalmazásra macerás tesztet írni, a tesztelendő osztályokat egy .NET Standard 2.0 projektbe hozzátok létre és azt tudjátok hivatkozni xUnit Test projektből, ha a teszt projekt .NET Core 2.0-át céloz meg.)
- [x] 10p: DocFX segítségével, XML kommentárokkal generált dokumentáció legalább 3 áttekintő UML diagrammal. A dokumentáció fejlesztői dokumentáció. Olyan mértékben kell, hogy tartalmazza a rendszer működését, hogy abból kiderüljön, hogy egy adott funkció hogy működik és hol található a forráskódban. A repository értelemszerűen tartalmazza a dokumentáció minden forrását is. A DocFX által generált HTML dokumentáció ZIP-elve a github.com release funkciójával letölthető formában kell, hogy elérhető legyen a leadási pull request létrehozásakor. https://github.com/blog/1547-release-your-software 
- [ ] 3p: Határidőre leadott pull request az 1. code reviewra, szignifikáns mennyiségű fejlesztéssel.
- [ ] 2p: Határidőre leadott pull request a 2. code reviewra, szignifikáns mennyiségű fejlesztéssel.

További lehetőségek, amik nem részei a tananyagnak, de pontot érnek:
- [ ] 8p: Behaviour használata (nem része a tananyagnak) (Videóban: xaml kód)
- [ ] 8p: Animációk használata (nem része a tananyagnak) (Videóban: UI használat közben vagy xaml kód)
- [ ] 5p: Style használata (nem része a tananyagnak) (Videóban: xaml kód) Az 5 pont saját definiált stílusra vonatkozik, ami legalább 2 propertyt beállít. Előre gyártott stílus használata 1p.
- [ ] 5p: OpenCvSharp használata (Videóban: UI használat közben vagy forráskód részlet)
- [ ] 3p: Statikus kódelemző használata a fejlesztés során (Videóban: az elemző visszajelzéseinek felvillantása)
- [ ] 8p: Dependency Injection keretrendszer használata

Ha nincs ötleted, hogy egy szempontot hogyan lehetne a feladatodba integrálni, tedd fel a kérdést a Teams nyilvános csatornájában és beszéljünk róla!

A felsoroltakon kívüli egyéb technológiák használatáért is lehet pontokat kapni, de hogy az új pontszerzési lehetőségről mindenki időben értesüljön, ezeket legkésőbb az 5. oktatási hétig a tantárgy Teams csoportjában (publikus csatornán) kell kérni az előadótól. (Ennek főleg az a célja, hogy ha valamire nem gondoltam, hogy használni fogjátok, de igen és sok munka, akkor járjon érte plusz pont is.)

## Mínusz pontok

Van pár alapelvárás, ami pontot nem ér, de ha valaki nem tartja be, az ütközik a tantárgy munkafolyamataival és ezért mínusz pontot ér. Kérünk mindenkit, hogy figyeljen a formai kérésekre is és ezekre a retorziókra ne legyen szükség... sajnos ezt korábbi számos rossz tapasztalat miatt kellett bevezetni.

- Videó hossza több, mint 5 perc (vagy több videó készül): -5p
- Generált fájlok (fordítási eredmények, exe, generált dokumentáció) a git repositoryban: -3p
- A beadott verzió nem a master ágon van: -5p
- Ronda forráskód: forráskódban kikommetezett kódrészletek, TODO kommentárok, szalonképtelen változónevek, több üres sor egymás után: -3p

## 50-nél több pont, jegymegajánlás

A házi feladat 50 pontot ér, de a normál házi feladat leadási időpontban ennél több pont is gyűjthető a pontozási szempontok alapján, ami ugyanúgy hozzáadódik a féléves pontszámhoz. (A pótleadáson max. 50 pont szerezhető).  Amennyiben egy csapat eléri a 70 pontot, a házi feladatra megajánlott 5-ös adható.

Néhány minta demó videó (az érintett csapatok beleegyezésével), bár ezeknél még Qt/C++ volt a platform:
https://drive.google.com/drive/folders/0B4jF_XaQKmkuUm9XRWVRRGRxNjg

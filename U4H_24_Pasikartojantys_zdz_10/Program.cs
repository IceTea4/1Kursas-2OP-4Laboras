namespace U4H_24_Pasikartojantys_zdz
{
    /* U4H-24. Pasikartojantys žodžiai.
     Tekstiniame faile Knyga.txt duotas tekstas sudarytas iš žodžių, 
     atskirtų skyrikliais. Skyriklių aibė žinoma. Raskite ir spausdinkite 
     faile Rodikliai.txt:
        Nurodytą kiekį dažniausiai pasikartojančių žodžių (ne daugiau
     nei 10 žodžių), surikiuotą pagal pasikartojimo skaičių mažėjimo tvarka,
     o kai pasikartojimų skaičius sutampa – pagal abėcėlę;
        Ilgiausią sakinį (didžiausias žodžių kiekis), jo ilgį (simboliais
     ir žodžiais) ir vietą (sakinio pradžios eilutės numerį).

     Reikia teksto žodžius sulygiuoti, kad kiekvienos eilutės kiekvienas
     žodis prasidėtų fiksuotoje toje pačioje pozicijoje. Galima įterpti
     tik minimalų būtiną tarpų skaičių. Reikia šalinti iš pradinio teksto
     kelis iš eilės einančius vienodus skyriklius, paliekant tik vieną jų
     atstovą. Įterpimo taisyklę taikome, siekdami gauti lygiuotą minimalų
     tekstą. Pradinio teksto eilutės ilgis neviršija 80 simbolių.
     Spausdinkite faile ManoKnyga.txt pertvarkytą tekstą pagal tokias
     taisykles:
        Kiekvienos eilutės pirmasis žodis turi prasidėti pozicijoje p1=1.
        Antrasis kiekvienos eilutės žodis turi prasidėti minimalioje galimoje
     pozicijoje p2, tokioje, kad kiekvienos eilutės pirmasis žodis kartu
     su už jo esančiais skyrikliais baigiasi iki p2-2 arba p2-1.
        Trečiasis kiekvienos eilutės žodis turi prasidėti minimalioje
     galimoje pozicijoje p3, tokioje, kad kiekvienos eilutės antrasis žodis
     kartu su už jo esančiais skyrikliais baigiasi iki p3-2 arba p3-1.
        Ir t.t.
    */

    /// <summary>
    /// Main class
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            const string CFd = @"../../../Knyga.txt";
            const string CFr8 = "Rodikliai.txt";
            const string CFr10 = "ManoKnyga.txt";
            char[] punctuation = { ' ', '.', ',', '!', '?', ':', ';',
                '(', ')', '\t' };
            string punct = "\\s,.;:!?()\\-";
            string pattern = @"([\p{P}\s])\1+";
            string endOfSentence = ".!?";

            //Solves first two tasks
            InOut.ProcessFirstTasks(CFd, CFr8, punctuation, endOfSentence);

            //Solves the last task
            InOut.ProcessSecondTask(CFd, CFr10, pattern, punct);
        }
    }
}

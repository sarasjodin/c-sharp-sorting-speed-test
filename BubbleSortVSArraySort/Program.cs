using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2 || args[0] != "-p")
        {
            Console.WriteLine("Felaktiga argument! Ange: dotnet run -- -p 0 eller -p 1");
            return;
        }

        string orderText = "";
        bool ascending;
        if (args[1] == "0")
        {
            orderText = "stigande ordning";
            ascending = true;
        }
        else if (args[1] == "1")
        {
            orderText = "fallande ordning";
            ascending = false;
        }
        else
        {
            Console.WriteLine("Ogiltigt värde. Använd 0 för stigande eller 1 för fallande.");
            return;
        }

        // Skapa en array med 25000 slumpade heltal (för att få ett bra utfall att jämföra)
        int[] numbers = new int[25000];
        Random rand = new Random();
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = rand.Next(0, 25000);
        }

        // Skriv ut osorterad array och kolla hur lång tid det tar att endast skriva ut
        Console.WriteLine("==============================");
        Console.WriteLine("Osorterad array:");
        Console.WriteLine("==============================");
        Stopwatch sw0 = Stopwatch.StartNew();
        PrintArray(numbers);
        sw0.Stop();
        Console.WriteLine();

        // BubbleSort av ursprungsarray för att kolla hur lång tid endast sorteringen tar
        int[] bubbleCopy = (int[])numbers.Clone();
        Stopwatch sw1 = Stopwatch.StartNew();
        BubbleSort(bubbleCopy, ascending);
        sw1.Stop();

        // Array.Sort av ursprungsarray för kolla hur lång tid endast sorteringen tar
        int[] arrayCopy = (int[])numbers.Clone();
        Stopwatch sw2 = Stopwatch.StartNew();
        Array.Sort(arrayCopy);
        if (!ascending)
        {
            Array.Reverse(arrayCopy);
        }
        sw2.Stop();

        // Skriv ut sorterade arrayer
        Console.WriteLine("==============================");
        Console.WriteLine("Sorterad med BubbleSort:");
        Console.WriteLine("==============================");
        PrintArray(bubbleCopy);
        Console.WriteLine("==============================");
        Console.WriteLine();

        Console.WriteLine("==============================");
        Console.WriteLine("Sorterad med Array.Sort:");
        Console.WriteLine("==============================");
        PrintArray(arrayCopy);
        Console.WriteLine("==============================");
        Console.WriteLine();

        // Tidsjämförelse
        Console.WriteLine("==============================================");
        Console.WriteLine("=   Välkommen till Saras jämförelse mellan   =");
        Console.WriteLine("=    BubbleSort och C# inbygda ArraySort     =");
        Console.WriteLine("=           Version 4.0 2025-09-15           =");
        Console.WriteLine("==============================================");
        Console.WriteLine(" ");


        Console.WriteLine("=== Tidsjämförelse ===");
        Console.WriteLine("Enbart utskrift av ursprunglig array av " + numbers.Length + " nummer (ingen sortering): " + sw0.ElapsedMilliseconds + " ms");
        Console.WriteLine("BubbleSort tid (tid för utskrift exkluderad, " + orderText + "): " + sw1.ElapsedMilliseconds + " ms");
        Console.WriteLine("Array.Sort tid (tid för utskrift exkluderad, " + orderText + "): " + sw2.ElapsedMilliseconds + " ms");

        long diff = sw1.ElapsedMilliseconds - sw2.ElapsedMilliseconds;
        if (diff > 0)
        {
            Console.WriteLine("Skillnad: " + diff + " ms där BubbleSort var långsammare");
        }
        else if (diff < 0)
        {
            Console.WriteLine("Skillnad: " + (-diff) + " ms där Array.Sort var långsammare");
        }
        else
        {
            Console.WriteLine("Skillnad: 0 ms (lika snabba)");
        }
        Console.WriteLine("");
        Console.WriteLine("Vill du köra igen? Ange: dotnet run -- -p 0  (för stigande sortering) eller dotnet run -- -p 1 (för fallande sortering)");
        Console.WriteLine("");
    }

    // BubbleSort-algoritmen
    // Viss inspiration hämtad från https://csharpskolan.se/article/bubble-sort/
    // Metoden returnernar inget värde, den bara utför en sortering
    static void BubbleSort(int[] arr, bool ascending = true)
    {
        // Det finns inget att sortera om arrayen saknas eller har 0–1 element.
        if (arr == null || arr.Length < 2) return;

        // Bubble sort passarar upprepade gånger igenom arrayen
        for (int pass = 0; pass < arr.Length - 1; pass++)
        {
            // Bool uppdateras om något byte sker, för att kunna avbryta tidigare om array redan är sorterad
            bool swapped = false;

            // Kontroll av två närliggande element (par). Om byte sker ligger bytet kvar i och med "-pass" (snabbar på sortering) 
            for (int j = 0; j < arr.Length - 1 - pass; j++)
            {
                // Bestämmer om paret är i fel ordning
                bool needsSwap = (ascending && arr[j] > arr[j + 1]) ||
                                 (!ascending && arr[j] < arr[j + 1]);

                if (needsSwap)
                // Klassisk swap med temporär variabel
                {
                    int tmp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = tmp;
                    swapped = true;
                }
            }

            // Om en hel passering gick utan byten är arrayen redan sorterad och avslutas i förväg
            if (!swapped) break;
        }
    }

    // Utskrift av array
    static void PrintArray(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i]);
            if (i < arr.Length - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine();
    }
}
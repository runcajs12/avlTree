using System;
using System.IO;
namespace Zadanie_4_B
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<string> binaryTree = new BinaryTree<string>();
            string wejscie = "in.txt";

           
            
            StreamReader sr = new StreamReader(wejscie);
            // while (sr.ReadLine() != null) {
            //   tree.Insert(sr.ReadLine()); }
            string[] pom;
            foreach (string line in File.ReadLines(wejscie))
            {
                pom = line.Split(' ');
                string append = pom[0] + ' ' + pom[1] + ' '+ pom[2];
                string num = pom[3];
                binaryTree.Insert(append, num);
            
            }
            sr.Close();
            //binaryTree.Save();
            Console.WriteLine("Binary Search Tree\n");
            bool zakonczenie = true;
            while (zakonczenie)
            {
                Console.Clear();
              
                Console.WriteLine("Wybierz opcje: ");
                Console.WriteLine("1. Wyswietl liste ");
                Console.WriteLine("2. Zapisz do pliku. ");
                Console.WriteLine("3. Usun kogos.");
                Console.WriteLine("4. Znajdz kogos.");
                Console.WriteLine("5. Zakończ program.");


                int wybor = Convert.ToInt32(Console.ReadLine()); // Wybieranie 
                switch (wybor)
                {
                    case 1:
                        binaryTree.InOrder();
                        Console.ReadKey();
                        break;
                    case 2:
                        binaryTree.Save();
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("Podaj dane abonenta do usuniecia:");
                        string del = Console.ReadLine();
                        binaryTree.Remove(del);
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("Podaj dane do znalezienia:");
                        string dane = Console.ReadLine();
                        var wynik = binaryTree.Find(dane);
                        if (wynik is null)
                        {
                            Console.WriteLine("Brak abonenta");
                        }
                        else { Console.WriteLine("Znaleziono abonenta - " + wynik.Data); Console.WriteLine(wynik.Numer); }
                        Console.ReadKey();
                        break;

                    case 5:
                        zakonczenie = false;
                        break;
                }

            }

            //binaryTree.InOrder();



            

            //Console.WriteLine(node.Data);

            //Console.WriteLine("Delete a Node with one child (93)");
            //binaryTree.Remove(93);
            //Console.WriteLine("Delete a Node with two child nodes (75)");
            //binaryTree.Remove(75);
            //Console.WriteLine("SoftDelete a Node with one child (93)");
            //binaryTree.SoftDelete(93);



        }


       
       


    }

}

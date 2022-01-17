using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Zadanie_4_B
{
    public class Węzeł<TItem> where TItem : IComparable<TItem>
    {   
        private TItem data;
        private TItem numer;
        public TItem Data
        {
            get { return data; }
        }
        public TItem Numer
        {
            get { return numer; }
        }

        private Węzeł<TItem> rightNode;
        public Węzeł<TItem> RightNode
        {
            get { return rightNode; }
            set { rightNode = value; }
        }

        private Węzeł<TItem> leftNode;
        public Węzeł<TItem> LeftNode
        {
            get { return leftNode; }
            set { leftNode = value; }
        }

    

        //Konstruktor
        public Węzeł(TItem value, TItem _numer)
        {
            data = value;
            numer = _numer;
        }

     

        public Węzeł<TItem> Find(TItem value)
        {
            //Pobieramy korzeń
            Węzeł<TItem> currentNode = this;

           
            while (currentNode != null)
            {
                //Jeśli aktualna wartość jest równa szukanej zwracamy ją
                if (currentNode.data.CompareTo(value)==0)
                {
                    return currentNode;
                }
                else if (currentNode.data.CompareTo(value) < 0)//Jeśli szukana wartość jest większa od aktualnej idziemy do prawego poddrzewa
                {
                    currentNode = currentNode.rightNode;
                }
                else//Jeśli szukana wartość jest mniejsza od aktualnej idziemy do lewego poddrzewa
                {
                    currentNode = currentNode.leftNode;
                }
            }
            //Jeśli nie znaleziono wartości zwracamy wartość null
             return null;
        }




        
        public void Insert(TItem value, TItem _numer)
        {
            //jeśli obecna wartość węzła jest mniejsza od elementu do dodania to element trafi do prawego poddrzewa
            if (data.CompareTo(value) < 0)
            {   //Jeśli prawe poddrzewo jest puste to tworzymy w nim nowy węzeł
                if (rightNode == null)
                {
                    rightNode = new Węzeł<TItem>(value, _numer);
                }
                else
                {//W przeciwnym razie wywołujemy metodę Insert dla prawego poddrzewa
                    rightNode.Insert(value, _numer);
                }
            }
            else
            {//jeśli obecna wartość węzła jest większa od elementu do dodania to element trafi do lewego poddrzewa
                if (leftNode == null)
                {//Jeśli lewe poddrzewo jest puste to tworzymy w nim nowy węzeł
                    leftNode = new Węzeł<TItem>(value, _numer);
                }
                else
                {//W przeciwnym razie wywołujemy metodę Insert dla lewego poddrzewa
                    leftNode.Insert(value, _numer);
                }
            }
        }



        //Przechodzenie po drzewie Lewy->korzeń->prawy
        public void InOrder()
        {
            
            
            if (leftNode != null)
                leftNode.InOrder();
            
            Console.Write(data + "\n");
            Console.WriteLine(numer);
            
            
            if (rightNode != null)
                rightNode.InOrder();
        }

        public void Save()
        {
            string save = "dane.txt";
            
           
            if (leftNode != null)
                leftNode.Save();
            //Zamiast wypisywać zapisujemy dane do pliku
            StreamWriter sw = File.AppendText(save);
            sw.Write(data + "\n");
            sw.Close();
            
            if (rightNode != null)
                rightNode.Save();
            
        }

        public int Height()
        {
            //return 1 when leaf node is found
            if (this.leftNode == null && this.rightNode == null)
            {
                return 1; //found a leaf node
            }

            int left = 0;
            int right = 0;

            //recursively go through each branch
            if (this.leftNode != null)
                left = this.leftNode.Height();
            if (this.rightNode != null)
                right = this.rightNode.Height();

            //return the greater height of the branch
            if (left > right)
            {
                return (left + 1);
            }
            else
            {
                return (right + 1);
            }

        }

        public int NumberOfLeafNodes()
        {
            //return 1 when leaf node is found
            if (this.leftNode == null && this.rightNode == null)
            {
                return 1; //found a leaf node
            }

            int leftLeaves = 0;
            int rightLeaves = 0;

            //recursively call NumOfLeafNodes returning 1 for each leaf found
            if (this.leftNode != null)
            {
                leftLeaves = leftNode.NumberOfLeafNodes();
            }
            if (this.rightNode != null)
            {
                rightLeaves = rightNode.NumberOfLeafNodes();
            }

            //add values together 
            return leftLeaves + rightLeaves;
        }

        public bool IsBalanced()
        {
            int LeftHeight = LeftNode != null ? LeftNode.Height() : 0;
            int RightHeight = RightNode != null ? RightNode.Height() : 0;

            int heightDifference = LeftHeight - RightHeight;

            if (Math.Abs(heightDifference) > 1)
            {
                return false;
            }
            else
            {
                return ((LeftNode != null ? LeftNode.IsBalanced() : true) && (RightNode != null ? RightNode.IsBalanced() : true));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Zadanie_4_B
{
    public class BinaryTree<TItem> where TItem :IComparable<TItem>
    {
        private Węzeł<TItem> root;
        public Węzeł<TItem> Root
        {
            get { return root; }
        }


        //O(Log n)
        public Węzeł<TItem> Find(TItem data)
        {
            
            if (root != null)
            {
                
                return root.Find(data);
            }
            else
            {//Jeśli węzeł jest pusty metoda zwraca null
                
                return null;
            }
        }



        //O(Log n)
        public void Insert(TItem data, TItem _numer)
        {
            //Jeśli korzeń nie jest pusty wywołujemy dla niego metodę Insert z klasy Węzeł
            if (root != null)
            {
                root.Insert(data, _numer);
            }
            else
            {//Jeżeli korzeń jest pusty tworzymy węzeł
                root = new Węzeł<TItem>(data, _numer);
            }
        }

        //O(Log n)
        public void Remove(TItem data)
        {
            //Wezły pamiętające rodzica i aktualną wartość, niezbędne do usunięcia

            Węzeł<TItem> current = root;
            Węzeł<TItem> parent = root;
            bool isLeftChild = false;
            //Zmienna boolowska pamiętająca w której którym dzieckiem(lewym czy prawym) jest szukana wartość


            if (current == null)
            {//Nie znaleziono wartości, więc nie można jej usunąć
                return;
            }

            
            //Znajdujemy wartość do usunięcia
            while (current != null && current.Data.CompareTo(data) != 0)
            {
                //Zapamiętujemy rodzica szukanej wartości
                parent = current;

                //Jeśli szukana wartość jest mniejsza od aktualnej idziemy do lewego poddrzewa
                if (current.Data.CompareTo(data)>0)
                {
                    current = current.LeftNode;
                    isLeftChild = true; //Ustawiamy zmienną na true
                }
                else
                {//Jeśli szukana wartość jest większa od aktualnej idziemy do prawego poddrzewa
                    current = current.RightNode;
                    isLeftChild = false; //Ustawiamy zmienną na false
                }
            }

            
            if (current == null)
            {
                return;
            }

            //Jeśli znaleziony węzeł nie ma dzieci

            if (current.RightNode == null && current.LeftNode == null)
            {
                //Jeśli znaleziony element był korzeniem
                if (current == root)
                {
                    root = null;
                }
                else
                {
                    
                    //Używając zmiennych pomocnicznych usuwamy odpowiednią wartość
                    if (isLeftChild)
                    {
                        
                        parent.LeftNode = null;
                    }
                    else
                    {   
                        parent.RightNode = null;
                    }
                }
            }
            else if (current.RightNode == null) //Węzeł ma tylko lewe dziecko
            {
                //Jeśli węzeł jest korzeniem zamieniamy jego wartość(tę do usunięcia) na wartość lewego dziecka
                if (current == root)
                {
                    root = current.LeftNode;
                }
                else
                {
                    //Używając zmiennej pomocniczej isLeftChild aby wiedziec którym dzieckiem jest aktualna wartosc
                    if (isLeftChild)
                    {
                        //Jeśli wartosc do usuniecia jest lewym dzieckiem to zamieniamy ją na swoje lewe dziecko
                        parent.LeftNode = current.LeftNode;
                    }
                    else
                    {   
                        parent.RightNode = current.LeftNode;
                    }
                }
            }
            else if (current.LeftNode == null) //Węzeł ma tylko prawe dziecko
            {
                //Jeśli węzeł jest korzeniem zamieniamy jego wartość(tę do usunięcia) na wartość prawego dziecka
                if (current == root)
                {
                    root = current.RightNode;
                }
                else
                {
                   
                    if (isLeftChild)
                    {   
                        parent.LeftNode = current.RightNode;
                    }
                    else
                    { 
                        parent.RightNode = current.RightNode;
                    }
                }
            }
            else //Węzeł ma dwójkę dzieci
            {
                

                //Szukamy kolejnej wartości po tej do usunięcia
                Węzeł<TItem> successor = GetSuccessor(current);
                //if the current node is the root node then the new root is the successor node
                if (current == root)
                {
                    root = successor;
                }
                else if (isLeftChild)
                {//if this is the left child set the parents left child node as the successor node
                    parent.LeftNode = successor;
                }
                else
                {//if this is the right child set the parents right child node as the successor node
                    parent.RightNode = successor;
                }

            }

        }

        private Węzeł<TItem> GetSuccessor(Węzeł<TItem> node)
        {
            Węzeł<TItem> parentOfSuccessor = node;
            Węzeł<TItem> successor = node;
            Węzeł<TItem> current = node.RightNode;

            //Idziemy raz do prawego dziecka a potem do końca w lewo dzięki czemu znajdziemy następna wartość(successor)
            while (current != null)
            {
                parentOfSuccessor = successor;
                successor = current;
                current = current.LeftNode;
            }
            
            if (successor != node.RightNode)
            {
                //Lewe dziecko rodzica staje się prawym dzieckiem następnika
                parentOfSuccessor.LeftNode = successor.RightNode;
                //Prawe poddrzewo przepinamy do prawego poddrzewa następnika
                successor.RightNode = node.RightNode;
            }
            //Lewe poddrzewo przepinamy do lewego poddrzewa nastepnika
            successor.LeftNode = node.LeftNode;

            return successor;
        }

   
    


        //Tree Traversal 
        //In order - goes left to right basically find the left leaf node then its parent then see if the right node has a left node then recursivly go up the tree
        // basically keep going left then recursive to parent then right
        //numbers will be in ascending order
        public void InOrder()
        {
            if (root != null)
                root.InOrder();
        }

        public void Save()
        {
            if (root != null)
                root.Save();
        }


        public int NumberOfLeafNodes()
        {
            //if root is null then  number of leafs is zero
            if (root == null)
            { return 0; }

            return root.NumberOfLeafNodes();
        }

        public int Height()
        {
            //if root is null then height is zero
            if (root == null)
            { return 0; }

            return root.Height();
        }


        //Check if the binary tree is balanced. A balanced tree occurs when the height of two subtrees of any node do not differe more than 1.
        public bool IsBalanced()
        {
            if (root == null)//Empty Tree
            {
                return true;
            }

            return root.IsBalanced();
        }


        //There are many self balancing trees
        //Some to look at are
        //Red Black Trees
        //AVL Trees




    }
}

using System;
using System.IO;
using System.Collections.Generic;

namespace CurseCase
{
    public class GenTree
    {
        private GenTree right;
        private GenTree left;
        private string name;
        private int suspicion;

        public GenTree Right => right;
        public GenTree Left => left;
        public string Name
        {
            get => name;
            set => name = value;
        }
        public int Suspicion
        {
            get => suspicion;
            set => suspicion = value;
        }

        public GenTree(string name, int suspicion, GenTree left, GenTree right)
        {
            this.right = right;
            this.left = left;
            this.name = name;
            this.suspicion = suspicion;
        }

        /**
         * \brief Takes the path of a .gen file and builds the GenTree from it
         */
        public GenTree(string path)
        {
            StreamReader reader = new StreamReader(path);

            string data = reader.ReadLine();
            string[] lines = data.Split('-');

            reader.Close();

            GenTree tree = Build(lines, 0);
            this.right = tree.right;
            this.left = tree.left;
            this.name = tree.name;
            this.suspicion = tree.suspicion;
        }

        private GenTree Build(string[] lines, int i)
        {
            if (i >= lines.Length)
                return null;

            string[] names = lines[i].Split(' ');
            string name = names[0];
            int suspicion = Int32.Parse(names[1]);

            return new GenTree(name, suspicion, Build(lines, 2 * i + 1), Build(lines, 2 * i + 2));
        }

        private static void Printer(GenTree tree)
        {
            Console.Write($"{tree.name}");
            if (tree.left != null)
            {
                Console.Write(" (");
                Printer(tree.left);
            }

            if (tree.right != null)
            {
                Console.Write(' ');
                Printer(tree.right);
                Console.Write(")");
            }
        }

        /**
         * \brief Prints the tree in the following format: name(right, left)
         */
        public void PrintTree()
        {
            Printer(this);
            Console.WriteLine();
        }

        public static bool ChangeName(GenTree root, string oldname, string newname)
        {
            if (root == null)
                return false;

            if (root.name == oldname)
            {
                root.name = newname;
                return true;
            }

            if (ChangeName(root.left, oldname, newname) || ChangeName(root.right, oldname, newname))
                return true;

            return false;
        }

        /**
         * \brief Recursive traversal of a tree to find the path from the root
         * to the descendant, saving the names encountered in the list path.
         * If no path exists, then the list must be empty.
         */
        public static bool FindPath(GenTree root, string name, List<string> path)
        {
            if (root == null)
                return false;

            if (root.name == name)
            {
                return true;
            }

            bool found = false;

            if ((found = FindPath(root.left, name, path)) == true)
                path.Add(root.name);
            else if ((found = FindPath(root.right, name, path)) == true)
                path.Add(root.name);

            return found;
        }

        private static string FindLcd(List<string> pathA, List<string> pathB)
        {
            for (int i = 0; i < pathA.Count; i++)
                for (int j = 0; j < pathB.Count; j++)
                    if (pathA[i] == pathB[j])
                        return pathA[i];

            return "";
        }

        /**
         * \brief Returns the name of the lowest common descendant in the tree,
         * between PersonA and PersonB, if the descendant doesn't exist,
         * the return value must be empty.
         * If one of the Persons are either empty or null, then an appropriate
         * exception must be thrown.
         */
        public static string LowestCommonDescendant(GenTree root, string PersonA, string PersonB)
        {
            if (PersonA == null || PersonB == null)
                throw new ArgumentNullException();

            if (PersonA == "" || PersonB == "")
                throw new ArgumentException();

            if (PersonA == PersonB)
                return "";

            List<string> pathA = new List<string>();
            List<string> pathB = new List<string>();
            FindPath(root, PersonA, pathA);
            FindPath(root, PersonB, pathB);

            return FindLcd(pathA, pathB);
        }

        private bool IsLeaf()
        {
            return this.left == null && this.right == null;
        }

        /**
         * \brief Saves the GenTree into a .dot file (bonus)
         */
        public void ToDot()
        {
            StreamWriter writer = new StreamWriter($"{this.name}.dot", !File.Exists($"{this.name}.dot"));
            Queue<GenTree> q = new Queue<GenTree>();
            q.Enqueue(this);

            writer.WriteLine($"graph {this.name} {{");
            while (q.Count > 0)
            {
                GenTree root = q.Dequeue();

                if (!root.IsLeaf())
                {
                    writer.WriteLine($"{root.name} -- {root.left.name};");
                    writer.WriteLine($"{root.name} -- {root.right.name};");
                    q.Enqueue(root.left);
                    q.Enqueue(root.right);
                }
            }

            writer.WriteLine("}");
            writer.Close();
        }
    }
}

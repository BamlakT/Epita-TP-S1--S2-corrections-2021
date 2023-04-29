using System;
using System.IO;
using System.Collections;

namespace Basics
{
    public class Basics
    {
        public static void FileOrDir(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine(path+" is a file !");
            }
            else if (Directory.Exists(path))
            {
                Console.WriteLine(path + " is a directory");
            }
            else
            {
                Console.WriteLine(path + " is neither a file nor a directory");
            }
        }

        public static void DisplayFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Error : No such file or directory");
                return;
            }
            StreamReader myReader = new StreamReader(path);
            string line;
            int i = 0;

            while ((line = myReader.ReadLine()) != null)
                Console.WriteLine("Line {0}: " + line, i++);
            
            myReader.Close();
        }

        public static void WriteInFile(string path, string content)
        {
            StreamWriter myWriter = new StreamWriter(path, true);
            
            myWriter.WriteLine(content);
            
            myWriter.Close();
        }

        public static int CopyFile(string source, string dest)
        {
            if (!File.Exists(source))
                return -1;
            
            StreamReader srcReader = new StreamReader(source);
            StreamWriter dstWriter = new StreamWriter(dest);
            string line;

            while ((line = srcReader.ReadLine()) != null)
                dstWriter.WriteLine(line);
            
            srcReader.Close();
            dstWriter.Close();
            return 0;
        }

        public static void PrintReformStr(string path)
        {
            int len = path.Length;
            int i = len - 1;
            for (; i >= 0; i--)
            {
                if (path[i] == '/' || i == 0)
                    break;
            }

            if (path[i] == '/')
                i++;
            
            while (i < len)
            {
                Console.Write(path[i]);
                i++;
            }
        }

        public static void MiniLs(string path)
        {
            if (File.Exists(path))
            {
                PrintReformStr(path);
                return;
            }
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Error : No such file or directory");
                return;
            }
            string[] fileEntries = Directory.GetFiles(path);
            int len = fileEntries.Length;
            int nbFiles = 0;
            for (int i = 0; i < len; i++)
            { 
                if (nbFiles != 0)
                    Console.Write(" ");
                PrintReformStr(fileEntries[i]);
                nbFiles++;
            }
        }

        public static void ConstructLine(string path, string branch)
        {
            Console.Write("|-- ");
            Console.Write(branch);
            PrintReformStr(path);
        }

        public static void ConstructTree(string path, string branch)
        {
            if (File.Exists(path))
            {
                PrintReformStr(path);
                return;
            }
            if (Directory.Exists(path))
            {
                ConstructLine(path, branch);
                Console.WriteLine("/");
            }

            branch += "-- ";
            string[] fileEntries = Directory.GetFiles(path);
            string[] dirEntries = Directory.GetDirectories(path);
            int lenFile = fileEntries.Length;
            int lenDir = dirEntries.Length;
            for (int i = 0; i < lenFile; i++)
            {
                ConstructLine(fileEntries[i], branch);
                Console.WriteLine("");
            }

            for (int i = 0; i < lenDir; i++)
            {
                ConstructTree(dirEntries[i], branch);
            }
        }

        public static void MyTree(string path)
        {
            if (File.Exists(path))
            {
                PrintReformStr(path);
                return;
            }
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Error : No such file or directory");
                return;
            }

            string branch = "";
            string[] fileEntries = Directory.GetFiles(path);
            
            int len = fileEntries.Length;
            
            ConstructTree(path, branch);
        }
    
    }
}
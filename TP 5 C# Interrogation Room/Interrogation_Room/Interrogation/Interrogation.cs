using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Security;

namespace TP5
{
    public class Interview
    {
        public class Profile
        {
            public string Name;
            public string Sex = ""; // ex: Male or Female
            public string Address = "";
            public uint Age = 0;
            public uint Size = 0;
            public List<string> Answer = new List<string>();
            public List<string> Question = new List<string>();
            public Profile(string name)
            {
                Name = name;
            }
        }
        public static Profile ReadProfile(string path)
        {
            StreamReader file = new StreamReader(path);
            Profile prof = new Profile("");
            string line = file.ReadLine();
            while (line != null)
            {
                string prefix = line.Split(':')[0];
                string res = line.Split(':')[1];
                switch (prefix)
                {
                    case "name":
                        prof.Name = res;
                        break;
                    case "age":
                        prof.Age = UInt32.Parse(res);
                        break;
                    case "size":
                        prof.Size = UInt32.Parse(res);
                        break;
                    case "sex":
                        prof.Sex = res;
                        break;
                    case "adress":
                        prof.Address = res;
                        break;
                    default:
                        Console.WriteLine("Didn't recognize this prefix : "  + prefix);
                        break;
                }
                
                line = file.ReadLine();
            }
            file.Close();
            return prof;
        }

        public static void ReadQuestion(List<Profile> allProfiles, string path)
        {
            StreamReader file = new StreamReader(path);
            string line = file.ReadLine();
            string question = line.Split(':')[1];
            line = file.ReadLine();
            while (line != null)
            {
                string name = line.Split(':')[0];
                int ind = 0;
                while (ind < allProfiles.Count && allProfiles[ind].Name != name)
                {
                    ind++;
                }

                if (ind < allProfiles.Count)
                {
                    allProfiles[ind].Answer.Add(line.Split(':')[1]);
                    allProfiles[ind].Question.Add(question);
                }
                line = file.ReadLine();
            }
            file.Close();
        }

        public static void PrintInformation(Profile profile)
        {
            Console.WriteLine("Name: " + profile.Name);
            Console.WriteLine("Sex: " + profile.Sex);
            Console.WriteLine("Age: " + profile.Age);
            Console.WriteLine("Size: "+ profile.Size);
            Console.WriteLine("Address: " + profile.Address);
            for (int i = 0; i < profile.Question.Count; i++)
            {
                Console.WriteLine("Question: " + profile.Question[i]);
                Console.WriteLine("Answer: " + profile.Answer[i]);
            }
        }
        
        public static void SaveProfile(Profile profile)
        {
            if (File.Exists(profile.Name + ".profile"))
            {
                File.Delete(profile.Name + ".profile");
            }
            StreamWriter writer = new StreamWriter(profile.Name + ".profile");
            writer.WriteLine("name:" + profile.Name);
            if (profile.Sex != "")
            {
                writer.WriteLine("sex:" + profile.Sex);
            }
            if (profile.Age != 0)
            {
                writer.WriteLine("age:" + profile.Age);
            }
            if (profile.Size != 0)
            {
                writer.WriteLine("size:" + profile.Size);
            }
            if (profile.Address != "")
            {
                writer.WriteLine("address:" + profile.Address);
            }
            writer.Close();
        }
        
        public static void CreateProfile()
        {
            Profile prof = new Profile("");
            Console.WriteLine("which field ?");
            string str = Console.ReadLine();
            while (str != "exit")
            {
                switch (str)
                {
                    case "name":
                        prof.Name = Console.ReadLine();
                        break;
                    case "age":
                        str = Console.ReadLine();
                        if (str != null)
                            prof.Age = UInt32.Parse(str);
                        break;
                    case "size":
                        str = Console.ReadLine();
                        if (str != null)
                            prof.Size = UInt32.Parse(str);
                        break;
                    case "sex":
                        prof.Sex = Console.ReadLine();;
                        break;
                    case "address":
                        prof.Address = Console.ReadLine();;
                        break;
                    default:
                        Console.WriteLine("Didn't recognize this field : "  + str);
                        break;
                }
                Console.WriteLine("which field ?");
                str = Console.ReadLine();
            }

            if (prof.Name != "")
            {
                SaveProfile(prof);
            }
            else
            {
                Console.WriteLine("Failed to create this new Profile");
            }
        }

        public static void Interrogation()
        {
            Console.WriteLine("Enter a command:");
            string user = Console.ReadLine();
            string file;
            List<Profile> profiles = new List<Profile>();
            while (user != "exit")
            {
                switch (user)
                {
                    case "read profile":
                        file = Console.ReadLine();
                        if (file != null && File.Exists(file))
                        { 
                            profiles.Add(ReadProfile(file));
                        }
                        break;
                    case "read interview":
                        file = Console.ReadLine();
                            if (file != null && File.Exists(file))
                            {
                                ReadQuestion(profiles, file);
                            }
                        break;
                    case "print profile":
                        string name = Console.ReadLine();
                        int j;
                        for (j = 0; j < profiles.Count; j++)
                        {
                            if (name == profiles[j].Name)
                            {
                                Console.WriteLine("tst");
                                PrintInformation(profiles[j]);
                                break;
                            }
                        }
                        if (j == profiles.Count)
                        {
                            Console.WriteLine("There is no profile Name that match with " + name);
                        }
                        break;
                    case "create profile":
                        CreateProfile();
                        break;
                    case "save profile":
                        string tmp = Console.ReadLine();
                        int cpt;
                        for (cpt = 0; cpt < profiles.Count; cpt++)
                        {
                            if (tmp == profiles[cpt].Name)
                            {
                                break;
                            }
                        }
                        if (profiles.Count == cpt)
                        {
                            Console.WriteLine("There is no profile that matches with " + tmp + " name");
                        }
                        else
                        {
                            SaveProfile(profiles[cpt]);
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        break; 
                       
                }
                Console.WriteLine("Enter a command:");
                user = Console.ReadLine();
            }
        }
    }
}
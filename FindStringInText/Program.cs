using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindStringInText
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Path: ");
            string path = Console.ReadLine();
            Console.Write("Search Keyword: ");
            string search = Console.ReadLine();
            Console.Write("Filetype: ");
            string filetype = Console.ReadLine();
            List<string> dirs = FindDirectories(path);
            List<string> strings = new List<string>();
            if(filetype == "")
            {
                strings = SearchDirectories(dirs, search);
            }
            else
            {
                strings = SearchDirectories(dirs, search,filetype);
            }
            foreach(string s in strings)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static List<string> FindDirectories(string path)
        {
            List<string> dirs = new List<string>();           
            string[] currentDirs = Directory.GetDirectories(path);
            foreach(var v in currentDirs)
            {
                try
                {
                    if (Directory.GetDirectories(v).Length > 0)
                    {
                        dirs.AddRange(FindDirectories(v));
                    }
                    else
                    {
                        dirs.Add(v);
                    }
                }
                catch(UnauthorizedAccessException e)
                {
                    Console.WriteLine(e);
                }
            }
            return dirs;
        }

        public static List<string> SearchDirectories(List<string> dirs, string search)
        {
            List<string> strings = new List<string>();
            foreach (string s in dirs)
            {
                foreach (string v in Directory.GetFiles(s))
                {
                    int i = 0;
                    foreach (string q in File.ReadAllLines(v))
                    {
                        i++;
                        if (q.Contains(search))
                        {
                            strings.Add("String: " + search + " was found in " + v + " at line " + i);
                        }
                    }
                }
            }
            return strings;
        }

        public static List<string> SearchDirectories(List<string> dirs, string search, string filetype)
        {
            List<string> strings = new List<string>();
            foreach (string s in dirs)
            {
                foreach (string v in Directory.GetFiles(s))
                {
                    int i = 0;
                    if (v.Contains(filetype))
                    {
                        foreach (string q in File.ReadAllLines(v))
                        {
                            i++;
                            if (q.Contains(search))
                            {
                                strings.Add("String: " + search + " was found in " + v + " at line " + i);
                            }
                        }
                    }
                }
            }
            return strings;
        }
    }
}

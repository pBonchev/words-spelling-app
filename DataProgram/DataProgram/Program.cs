﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataProgram
{
    class Program
    {

        static string word;
        static int mode;

        static void Exists(string x)
        {

        }

        static void WriteWordInFile()
        {

        }

        static string GetKeyWord(string x)
        {
            //Get only the key word    input: horse - horce
            //                         return: horse
            int i = 0;
            while (x[i] != ' ' && x[i] != '-' && i < x.Length - 1)
                i++;
            if (i == x.Length - 1 && x[i] != ' ' && x[i] != '-') i++;
                return x.Substring(0, i);
        }
        
        //Option 1 from the main menu
        static void CheckOrAddWord()
        {
            string keyWord;

            while (true)
            {
                Console.WriteLine("Enter a word:");
                word = Console.ReadLine();

                keyWord = GetKeyWord(word);
               
                //Open file to check if the word is there
                StreamReader reader = new StreamReader("WordList.txt");
                string fileLine; //one line from the file

                bool wordExists = false;
                string AddOrNo = "n";
                while(!reader.EndOfStream)
                    {
                     fileLine = reader.ReadLine();
                     if (keyWord == GetKeyWord(fileLine)) wordExists = true;
                    }

                if(wordExists)
                    {
                     Console.WriteLine("The word is in the list!");
                    }
                else
                    {
                     Console.WriteLine("The word is not in the list!\nDo you want to add it? y/n");
                     AddOrNo = Console.ReadLine();
                    }
                reader.Close();
               
                if(AddOrNo == "y")
                    {
                     StreamWriter writer = new StreamWriter("WordList.txt",true);
                     writer.WriteLine(word);
                     writer.Close();
                    }
            }
        }

        static void Main(string[] args)
        {
            
            Console.WriteLine("Menu:\n1 - Add new word or check if exists");
            mode = int.Parse(Console.ReadLine());

            switch(mode)
            {
             case 1:
                {
                 CheckOrAddWord();
                 break;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using WordToSNArticle.Utilities;

namespace WordToSNArticle
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].IndexOf("?") > -1)
            {
                Console.WriteLine("Usage: WordToSNArticle.exe File Topic Category [doc password]");
                Console.WriteLine("Info: Additional Settings in WordToSNArticle.exe.config file.");
                return;
            }
            if (args.Length == 0)
            {
                args = new string[3];
                args[0] = GetFileName();
                Console.Write("Article Topic?: ");
                args[1] = Console.ReadLine();
                Console.Write("Article Category?: ");
                args[2] = Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            convert thisConversion = new convert();
            thisConversion.InsertKBArticle(args);
        }
        
        static string GetFileName()
        {
            string filename;
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Select the Word Document to post as a Service Now Article";
            fd.Multiselect = false;
            fd.Filter = "Word Documents(*.DOC;*.DOCX)|*.DOC;*.DOCX";
            fd.ShowDialog();
            filename = fd.FileName;
            //Console.WriteLine(filename);
            //Console.Read();
            return filename;
        }
    }

}

using System;
using System.IO;
using System.Collections.Generic;

using HeyRed.Mime;
using ByteSizeLib;

namespace PDFCompress
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && (args[0] == "-h" || args[0] == "--help")) {
                printHelp();
                Environment.Exit(0);
            }

            if (args.Length == 0) {
                Console.WriteLine("Please provide a directory path.");
                Environment.Exit(0);
            }

            string directory = args[0];

            if (!Directory.Exists(directory)) {
                Console.WriteLine("The given directory path is invalid!");
                Environment.Exit(0);
            }

            Console.WriteLine("Compressing PDF's in " + directory);

            string[] files = Directory.GetFiles(directory);
            List<string> pdfs = new List<string>();

            foreach (string file in files) {
                FileInfo fileInformation = new FileInfo(file);
                string mimeType = MimeTypesMap.GetMimeType(file);
                ByteSize fileSize = ByteSize.FromBytes(fileInformation.Length);

                if (
                    fileInformation.Extension == ".pdf" &&
                    mimeType == "application/pdf" &&
                    fileSize.MegaBytes > 1
                ) {
                    pdfs.Add(file);
                }
            }

            foreach(var item in pdfs) {
                Console.WriteLine($"Compressing: {item.ToString()}...");
                bool compress = Compress.compress(item);

                if (compress) {
                    Console.WriteLine($"{item.ToString()} compressed!");
                } else {
                    Console.WriteLine($"An error occurred while compressing {item.ToString()}");
                }
            }
        }

        static void printHelp()
        {
            Console.WriteLine("PDF Compress");
            Console.WriteLine("This utility requires you to have ghostscript and bash installed.");
            Console.WriteLine("Build from source: dotnet build");
            Console.WriteLine("Usage: ./pdf-compress [directory containing pdfs]");
        }
    }
}

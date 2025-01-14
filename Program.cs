using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEncryptionTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("--- Text Encryption Tool ---");
                Console.WriteLine("1. Encrypt Text");
                Console.WriteLine("2. Decrypt Text");
                Console.WriteLine("3. Save Encrypted Text");
                Console.WriteLine("4. Load Encrypted Text");
                Console.WriteLine("5. List Saved File");
                Console.WriteLine("6. Delete Saved File");
                Console.WriteLine("7. Exit");
                Console.WriteLine("Choose an option by entering the corresponding number: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    EncryptText();
                }
                else if (choice == "2")
                {
                    DecryptText();
                }
                else if (choice == "3")
                {
                    SaveText();
                }
                else if (choice == "4")
                {
                    LoadText();
                }
                else if (choice == "5")
                {
                    ListFiles();
                }
                else if (choice == "6")
                {
                    DeleteText();
                }
                else if (choice == "7")
                {
                    Console.WriteLine("Exiting... Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
                Console.WriteLine();
            }
        }

        static void EncryptText()
        {
            Console.WriteLine("Enter text to enrypt: ");
            string input = Console.ReadLine();
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Text cannot be empty. Please enter valid text: ");
                input = Console.ReadLine();
            }
            Console.WriteLine("Enter shift amount (e.g., 3): ");
            int shift;
            while (!int.TryParse(Console.ReadLine(), out shift))
            {

                Console.WriteLine("Invalid input. Please enter a numeric value for the shift amount: ");
            }
            string encrypted = CaesarCipher(input, shift);
            Console.WriteLine($"Encrypted Text: {encrypted}");
        }

        static void DecryptText()
        {
            Console.WriteLine("Enter text to decrypt: ");
            string input = Console.ReadLine();
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Text cannot be empty. Please enter valid text: ");
                input = Console.ReadLine();
            }
            Console.WriteLine("Enter shift amount (e.g., 3): ");
            int shift;
            while (!int.TryParse(Console.ReadLine(), out shift))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value for the shift amount: ");
            }
            string decrypted = CaesarCipher( input, -shift);
            Console.WriteLine($"Decrypted Text: {decrypted}");
        }

        static string CaesarCipher(string text, int shift)
        {
            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    letter = (char)(((letter + shift - offset) % 26 + 26) % 26 + offset);
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }

        static void SaveText()
        {
            Console.WriteLine("Enter text to save: ");
            string text = Console.ReadLine();
            Console.WriteLine("Enter file name to save (e.g., enrypted.txt): ");
            string fileName = Console.ReadLine();
            File.WriteAllText(fileName, text);
            Console.WriteLine($"Text saved successfully as {fileName}!");
        }
        
        static void LoadText()
        {
            Console.WriteLine("Enter file name to load (e.g., encrypted.txt): ");
            string fileName = Console.ReadLine();
            if (File.Exists(fileName))
            {
                string text = File.ReadAllText(fileName);
                Console.WriteLine($"Loaded Text: {text}");
            }
            else
            {
                Console.WriteLine($"File '{fileName}' not found.");
            }
        }
        
        static void ListFiles()
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt");
            if (files.Length == 0)
            {
                Console.WriteLine("No files found in the current directory.");
            }
            else
            {
                Console.WriteLine("--- Files in Current Directory ---");
                foreach (var file in files)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }
            }
        }
        static void DeleteText()
        {
            Console.WriteLine("Enter the file name to delete (e.g., encrypted.txt): ");
            string fileName = Console.ReadLine();

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                Console.WriteLine($"File '{fileName}' deleted successfully!");
            }
            else
            {
                Console.WriteLine($"File '{fileName}' does not exist.");
            }
        }
    }
}

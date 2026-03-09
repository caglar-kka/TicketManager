using PasswordValidation;
using TicketManager.Models;
using TicketManager.Security;

namespace TicketManager.Services
{
    internal class UserService
    {
        private readonly UserStorageService storage;
        private readonly PasswordValidator validator;

        public UserService(UserStorageService storage)
        {
            this.storage = storage;
            validator = new PasswordValidator();
        }

        public void CreateNewUser(List<User> users)
        {
            Console.Write("Username eingeben: ");
            string username = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username darf nicht leer sein.");
                return;
            }

            bool exists = users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                Console.WriteLine("Dieser Username existiert bereits.");
                return;
            }

            Console.Write("Passwort eingeben: ");
            string password = ReadPasswordHidden();

            try
            {
                validator.ValidatePassword(password);
            }
            catch (PasswordException ex)
            {
                Console.WriteLine("Das Passwort ist ungültig:");

                foreach (string error in ex.Errors)
                {
                    Console.WriteLine("- " + error);
                }

                return;
            }

            var (hash, salt) = PasswordHashing.HashPassword(password);

            users.Add(new User
            {
                Username = username,
                PasswordHash = hash,
                PasswordSalt = salt
            });

            storage.SaveUsers(users);
            Console.WriteLine("User wurde angelegt.");
        }

        public bool Login(List<User> users)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username darf nicht leer sein.");
                return false;
            }

            Console.Write("Passwort: ");
            string password = ReadPasswordHidden();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Passwort darf nicht leer sein.");
                return false;
            }

            User? foundUser = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (foundUser == null)
            {
                Console.WriteLine("User nicht gefunden.");
                return false;
            }

            bool ok = PasswordHashing.VerifyPassword(password, foundUser.PasswordHash, foundUser.PasswordSalt);

            if (ok)
            {
                Console.WriteLine("Login erfolgreich.");
                return true;
            }
            else
            {
                Console.WriteLine("Falsches Passwort.");
                return false;
            }
        }
        private static string ReadPasswordHidden()
        {
            var chars = new List<char>();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (chars.Count > 0)
                    {
                        chars.RemoveAt(chars.Count - 1);

                        // Zeichen auf Konsole "löschen"
                        Console.Write("\b \b");
                    }
                    continue;
                }

                // Optional: Tab, Escape etc. ignorieren
                if (char.IsControl(key.KeyChar))
                    continue;

                chars.Add(key.KeyChar);
                Console.Write("*");
            }

            return new string(chars.ToArray());
        }
        public void DeleteUser(List<User> users)
        {
            Console.Write("Username zum Löschen: ");
            string username = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username darf nicht leer sein.");
                return;
            }

            User? user = users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                Console.WriteLine("User nicht gefunden.");
                return;
            }
            Console.Write($"Wirklich User '{user.Username}' löschen? (j/n): ");
            string confirm = Console.ReadLine() ?? string.Empty;

            if (!string.Equals(confirm, "j", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Abgebrochen.");
                return;
            }

            users.Remove(user);
            storage.SaveUsers(users);

            Console.WriteLine($"User '{user.Username}' wurde gelöscht.");
        }
    }
}
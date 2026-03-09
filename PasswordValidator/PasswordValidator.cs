namespace PasswordValidation
{
    public class PasswordValidator
    {
        public void ValidatePassword(string password)
        {
            var errors = new List<string>();

            ApplyRule(CheckNotEmpty, password, errors);
            ApplyRule(CheckMinLength, password, errors);
            ApplyRule(CheckHasUppercase, password, errors);
            ApplyRule(CheckHasDigit, password, errors);
            ApplyRule(CheckHasSpecialCharacter, password, errors);
            ApplyRule(CheckHasLowercase, password, errors);
            ApplyRule(CheckNoWhitespace, password, errors);
            ApplyRule(CheckNoTripleCharacters, password, errors);
            ApplyRule(CheckNotOnlyDigits, password, errors);
            ApplyRule(CheckNoForbiddenWord, password, errors);

            if (errors.Count > 0)
                throw new PasswordException(errors);
        }

        private string CheckNotEmpty(string password)
        {
            if (password == null || password.Length == 0)
                return "Passwort darf nicht leer sein.";

            return null;
        }
        private string CheckMinLength(string password)
        {
            if (password.Length < 8)
                return "mindestens 8 Zeichen";

            return null;
        }
        private string CheckHasUppercase(string password)
        {
            foreach (var c in password)
            {
                if (char.IsUpper(c))
                    return null;

            }
            return "mindestens einen Großbuchstaben";
        }
        private string CheckHasDigit(string password)
        {
            foreach (var c in password)
            {
                if (char.IsDigit(c))
                    return null;
            }
            return "mindestens eine Ziffer";
        }
        private string CheckHasSpecialCharacter(string password)
        {
            char[] special = { '!', '@', '#', '$', '%', '&', '/', '?', '*' };

            foreach (var c in password)
            {
                if (special.Contains(c))
                    return null;
            }
            return "mindestens ein Sonderzeichen";
        }
        private string CheckHasLowercase(string password)
        {
            foreach (var c in password)
            {
                if (char.IsLower(c))
                    return null;
            }
            return "mindestens einen Kleinbuchstaben";
        }
        private string CheckNoWhitespace(string password)
        {
            foreach (var c in password)
            {
                if (char.IsWhiteSpace(c))
                    return "keine Leerzeichen";
            }
            return null;
        }
        private string CheckNoTripleCharacters(string password)
        {
            for (int i = 0; i < password.Length - 2; i++)
            {
                if (password[i] == password[i + 1] && password[i] == password[i + 2])
                    return "keine drei identische Zeichen hintereinander";
            }
            return null;
        }
        private string CheckNotOnlyDigits(string password)
        {
            bool allDigits = true;

            foreach (var c in password)
            {
                if (!char.IsDigit(c))
                {
                    allDigits = false;
                    break;
                }
            }

            if (allDigits)
            {
                return "nicht nur aus Ziffern";
            }
            return null;
        }
        private string CheckNoForbiddenWord(string password)
        {
            string lower = password.ToLower();

            if (lower.Contains("passwort") || lower.Contains("password"))
                return "nicht das verbotene Wort „Passwort”";

            return null;
        }
        private void ApplyRule(Func<string, string> rule, string password, List<string> errors)
        {
            string error = rule(password);
            if (error != null)
                errors.Add(error);
        }
    }
}
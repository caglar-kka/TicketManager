namespace PasswordValidation
{
    public class PasswordException : Exception
    {
        public List<string> Errors { get; }

        public PasswordException(List<string> errors)
            : base("Das Passwort ist ungültig.")
        {
            Errors = errors;
        }
    }
}

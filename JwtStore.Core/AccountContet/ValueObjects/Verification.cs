using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AccountContet.ValueObjects;
    public class Verification : ValueObject
    {
        public string Code {get;} = Guid.NewGuid().ToString("N")[0..6].ToUpper();
        public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(10);
        public DateTime? VerifieAt { get; private set; } = null;
        public bool IsActive => VerifieAt != null && ExpiresAt == null;

        public void Verify(string code)
        {
            if(IsActive)
                throw new Exception("Este item já foi ativado!");

            if(ExpiresAt < DateTime.UtcNow)
                throw new Exception("Este codigo já expirou!");

            if(!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Código de verificação invalido!");

            ExpiresAt = null; 
            VerifieAt = DateTime.UtcNow;
        }
    }
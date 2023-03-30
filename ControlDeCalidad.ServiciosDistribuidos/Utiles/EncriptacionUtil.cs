using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ControlDeCalidad.ServiciosDistribuidos.Utiles
{
    internal static class EncriptacionUtil
    {
        private static byte[] _salt = BitConverter.GetBytes(123);

        internal static string ObtenerHash(string cadena)
        {
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: cadena!,
                salt: _salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hash;
        }
    }
}

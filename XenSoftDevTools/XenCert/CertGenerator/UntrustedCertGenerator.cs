using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace XenCert.CertGenerator {
    public class TrustedCertGenerator {
        private static Random _random = new Random();

        private static byte[] _getSerialNumberBytes()
        {
            byte[] serialNumber = new byte[12];
            _random.NextBytes(serialNumber);
            return serialNumber;
        }

        public static void GenerateCert()
        {
            // Generate a certificate request
            CertificateRequest request = new CertificateRequest(
                new X500DistinguishedName("CN=XenCertificate"),
                ECDsa.Create(),
                new HashAlgorithmName("SHA256")
            );

            // Add certificate extensions
            request.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
            request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(new OidCollection { new Oid("1.3.6.1.5.5.7.3.1") }, true));
            request.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(request.PublicKey, true));

            // Generate the certificate
            X509Certificate2 certificate = request.CreateSelfSigned(
                DateTime.Now,
                DateTime.Now.AddYears(1)
            );

            // Export the certificate to a .pfx file
            string fileName = $"F://{certificate.Subject}.pfx";
            byte[] pfxBytes = certificate.Export(X509ContentType.Pfx, "MyPassword");
            File.WriteAllBytes(fileName, pfxBytes);
        }
    }
}
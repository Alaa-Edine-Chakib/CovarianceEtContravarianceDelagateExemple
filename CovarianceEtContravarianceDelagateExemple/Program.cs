using System;
using System.IO;
using System.Linq;
namespace CovarianceEtContravarianceDelagateExemple
{
    class Program
    {
        delegate Voiture VoitureFactoryDelegate(int id, string nom);
        delegate void logDetailVoitureCombustion(VoitureEssence voitureEssence);
        delegate void logDetailVoitureElectrique(VoitureElectrique voitureElectrique);
        static void Main(string[] args)
        {
            VoitureFactoryDelegate voitureFactoryDelegate = VoitureFactory.RetourneVoitureCombustion;

            Voiture voitureEssence = voitureFactoryDelegate(1, "Audi R8");

            //Console.WriteLine($"Type Objet: {voitureEssence.GetType()}");
            //Console.WriteLine($"Detail Voiture: {voitureEssence.GetInfosVoiture()}");

            voitureFactoryDelegate = VoitureFactory.RetourneVoitureElectrique;
            Voiture voitureElectrique = voitureFactoryDelegate(2, "Tesla Model S");

            //Console.WriteLine($"Type Objet: {voitureElectrique.GetType()}");
            //Console.WriteLine($"Detail Voiture: {voitureElectrique.GetInfosVoiture()}");

            logDetailVoitureCombustion logDetailVoitureCombustionDelegate = LogDetailsVoiture;
            logDetailVoitureCombustionDelegate(voitureEssence as VoitureEssence);
            logDetailVoitureElectrique logDetailVoitureElectriqueDelegate = LogDetailsVoiture;
            logDetailVoitureElectriqueDelegate(voitureElectrique as VoitureElectrique);

            Console.ReadKey();
        }
        static void LogDetailsVoiture(Voiture voiture)
        {
            if(voiture is VoitureEssence)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DetailsElectrique.txt"), true))
                {
                    sw.WriteLine($"Type Objet: {voiture.GetType()}");
                    sw.WriteLine($"Detail Voiture: {voiture.GetInfosVoiture()}");
                }
            }
            else if (voiture is VoitureElectrique)
            {

                Console.WriteLine($"Type Objet: {voiture.GetType()}");
                Console.WriteLine($"Detail Voiture: {voiture.GetInfosVoiture()}");
            }
            else
            {
                throw new ArgumentException("Type de voiture non reconnu");
            }
        }
    }

    public static class VoitureFactory
    {
        public static VoitureEssence RetourneVoitureCombustion(int id, string nom)
        {
            return new VoitureEssence { Id = id, Nom = nom };
        }

        public static VoitureElectrique RetourneVoitureElectrique(int id, string nom)
        {
            return new VoitureElectrique { Id = id, Nom = nom };
        }
    }

    public abstract class Voiture
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public virtual string GetInfosVoiture()
        {
            return $"{Id} - {Nom}";
        }

    }

    public class VoitureEssence : Voiture
    {
        public override string GetInfosVoiture()
        {
            return $"{base.GetInfosVoiture()} - Moteur Combustion Interne";
        }
    }

    public class VoitureElectrique : Voiture
    {
        public override string GetInfosVoiture()
        {
            return $"{base.GetInfosVoiture()} - Moteur Electrique";
        }
    }


}

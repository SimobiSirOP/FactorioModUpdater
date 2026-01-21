

namespace FactorioModUpdater
{
    public static class Main_
    {
        public static void Main(string[] args)
        {
            try
            {
                Master.Load(args);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            ModDataHandler.InitiateModData();
        }
    }
}
using SubSonic;

namespace VNS.HIS.DAL
{
    public class SubSonicHelper
    {
        #region Constants Variables
        private const string DEFAULT_CONNECITON_STRING =
            "Data Source=HUNGMAI-PC; Initial Catalog=TestDB; User ID=sa;Password=123;";
        private const string PROVIDER_NAME = "ORM";

        #endregion

        #region Public Static Method
        public static void InitSubSonic(string connectionString)
        {
            string workingConnectionString = (!string.IsNullOrEmpty(connectionString))
                                                 ? connectionString
                                                 : DEFAULT_CONNECITON_STRING;
            
            DataService.Providers = new DataProviderCollection();
            var myProvider = new CustomSqlProvider(workingConnectionString);

            if (DataService.Providers[PROVIDER_NAME] == null)
            {
                DataService.Providers.Add(myProvider);
                DataService.Provider = myProvider;
            }
        }
        #endregion
    }
}

using SubSonic;

namespace VNS.HIS.DAL
{
    internal class CustomSqlProvider : SqlDataProvider
    {
        private const string PROVIDER_NAME = "ORM";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public CustomSqlProvider(string connectionString)
        {
            this.DefaultConnectionString = connectionString;
        }

        /// <summary>
        /// override property Provider Name
        /// </summary>
        public override string Name
        {
            get
            {
                return PROVIDER_NAME;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Contracts.InitializerContracts
{
    public  interface IDbInitializer
    {
        public static void Initialize(DbContext context);
    }
}
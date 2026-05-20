namespace Quan_ly_Homestay.DAL
{
    public static class DatabaseConfig
    {
        public static readonly string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=HomestayDB;Integrated Security=true;" +
            "Min Pool Size=5;Max Pool Size=100;Pooling=true;Connection Lifetime=0;";
    }
}

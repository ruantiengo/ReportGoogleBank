using Dapper;
using Npgsql;
using System.Linq;

namespace ReportGoogleBank{
    public class TransferRepository{

        public async Task<IEnumerable<dynamic>> GetTransfersRepository(NpgsqlConnection conn,int userId)
        {
            var sql = $@"
                SELECT s.id as sender_id, s.name AS sender_name, r.id as receiver_id, r.name AS receiver_name, value
                FROM ""public"".""Transfer"" t
                INNER JOIN ""public"".""User"" s ON t.sender_id = s.id
                INNER JOIN ""public"".""User"" r ON t.receiver_id = r.id
                WHERE s.id = {userId} OR r.id = {userId}
            ";
           
            var transfers = await conn.QueryAsync(sql);
           
            return transfers;
        }
    }
}
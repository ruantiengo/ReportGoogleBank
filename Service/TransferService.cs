using Dapper;
using Npgsql;
using System.Linq;

namespace ReportGoogleBank{
    public class TransferService{

        private readonly TransferRepository transferRepository;

        public TransferService(){
            transferRepository = new TransferRepository();
        }

        public async Task<IEnumerable<Transfer>> GetTransfers(NpgsqlConnection conn,int userId)
        {
            var response = await transferRepository.GetTransfersRepository(conn, userId);
            List<Transfer> transfers = new List<Transfer>();
            // set the user role (if it is receiver or sender)

            foreach (var row in response)
            {
                Transfer transfer = new Transfer
                {
                    SenderId = Convert.ToInt32(row.sender_id),
                    ReceiverId = Convert.ToInt32(row.receiver_id),
                    Value = Convert.ToSingle(row.value),
                    ReceiverName = Convert.ToString(row.receiver_name),
                    SenderName = Convert.ToString(row.sender_name),
                    // outras propriedades da classe Transfer
                };
                transfer.SetUserIdRole(userId);
                transfers.Add(transfer);
            }
        
            return transfers;
        }
    }
}
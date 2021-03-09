using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryCRM.Data.Models;
using LibraryCRM.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryCRM.Data
{
    public class EFTransferRepository : ITransferRepository
    {
        private ApplicationDbContext context;

        public EFTransferRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Transfer> Transfers => context.Transfers
                            .Include(o => o.Lines)
                            .ThenInclude(l => l.Book);

        public async Task SaveTransferAsync(Transfer transfer)
        {
            context.AttachRange(transfer.Lines.Select(l => l.Book));
            if (transfer.TransferID == 0)
            {
                context.Transfers.Add(transfer);
            }
            await context.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using LibraryCRM.Data.Models;

namespace LibraryCRM.Data
{
    public interface ITransferRepository
    {
        IQueryable<Transfer> Transfers { get; }
        Task SaveTransferAsync(Transfer transfer);
    }
}

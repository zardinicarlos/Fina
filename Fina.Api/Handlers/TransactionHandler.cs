using Fina.Api.Data;
using Fina.Core.Common;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class TransactionHandler : ITransactionHandler
    {
        private AppDbContext appDbContext;

        public TransactionHandler(AppDbContext context)
        {
            appDbContext = context;
        }

        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            var transaction = new Transaction()
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                Amount = (request.Type == Core.Enums.ETransactionType.Withdraw && request.Amount >= 0 ? -1 : 1) * request.Amount,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Title = request.Title,
                Type = request.Type,
            };
            try
            {
                await appDbContext.Transaction.AddAsync(transaction);
                await appDbContext.SaveChangesAsync();
                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Transaction?>(null, 500, "Não foi possível criar a transação");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await appDbContext.Transaction.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction == null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                appDbContext.Transaction.Remove(transaction);
                await appDbContext.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação excluida com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Transaction?>(null, 500, "Não foi possível criar a transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                var startDate = request.StartDate ?? DateTime.Now.GetFirstDay(null, null);
                var endDate = request.EndDate ?? DateTime.Now.GetLastDay(null, null);
                
                var query = appDbContext.Transaction.AsNoTracking().Where(x => x.UserId == request.UserId && x.PaidOrReceivedAt >= startDate && x.PaidOrReceivedAt <= endDate).OrderBy(x => x.PaidOrReceivedAt);

                var transactions = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi recuperar as transações");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await appDbContext.Transaction.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction == null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                return new Response<Transaction?>(transaction);
            }
            catch (Exception ex)
            {
                return new Response<Transaction?>(null, 500, "Não foi recuperar criar a transação");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                var transaction = await appDbContext.Transaction.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction == null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = (request.Type == Core.Enums.ETransactionType.Withdraw && request.Amount >= 0 ? -1 : 1) * request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

                appDbContext.Transaction.Update(transaction);
                await appDbContext.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Transaction?>(null, 500, "Não foi possível criar a transação");
            }
        }
    }
}

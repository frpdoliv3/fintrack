using FinTrack.Application.Operation.CreateOperation;
using FinTrack.Domain.Entities;
using Entities = FinTrack.Domain.Entities;

namespace FinTrack.Application.Security;

public static class OperationOrderValidator
{
    public static bool ValidateOperations(List<CreateOperationRequest> operations)
    {
        operations.Sort((a, b) => 
            a.OperationDate.CompareTo(b.OperationDate));
        decimal curQuantity = 0;
        foreach (var operation in operations)
        {
            switch (operation.OperationType)
            {
                case OperationType.Sell:
                    curQuantity -= operation.Quantity;
                    break;
                case OperationType.Buy:
                    curQuantity += operation.Quantity;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (curQuantity < 0)
            {
                return false;
            }
        }
        return true;
    }

    public static async Task<bool> ValidateOperations(IAsyncEnumerable<Entities.Operation> operations)
    {
        decimal curQuantity = 0;
        await foreach (var operation in operations)
        {
            switch (operation.OperationType)
            {
                case OperationType.Sell:
                    curQuantity -= operation.Quantity;
                    break;
                case OperationType.Buy:
                    curQuantity += operation.Quantity;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (curQuantity < 0)
            {
                return false;
            }
        }
        return true;
    }
}
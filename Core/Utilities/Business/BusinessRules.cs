using Core.Entities.Abstract;
using Core.Utilities.Results;

namespace Core.Utilities.Business;

public static class BusinessRules
{
    public static IResult Run(params IResult[] logics)
    {
        foreach (var logic in logics)
        {
            if (!logic.Success)
            {
                return new ErrorResult(logic.Message);
            }
        }

        return new SuccessResult();
    }
}
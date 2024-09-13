using Core.Entities.Abstract;
using Core.Utilities.Results;

namespace Core.Utilities.Business;

public static class BusinessRules
{
    public static IResult Run(params IResult[] logics)
    {
        foreach (var logic in logics)
            if (!logic.Status)
                return new ErrorResult(logic.Message);

        return new SuccessResult();
    }
}
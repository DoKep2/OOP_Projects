namespace Banks.Classes
{
    public class ExtraConditions
    {
        public ExtraConditions(decimal operationLimit)
        {
            OperationLimit = operationLimit;
        }

        public decimal OperationLimit { get; }
    }
}
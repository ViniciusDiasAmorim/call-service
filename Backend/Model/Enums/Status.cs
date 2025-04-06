using System.ComponentModel;

namespace CallServiceFlow.Model.Enums
{
    public enum Status
    {
        [Description("Aberto")]
        Open = 0,
        [Description("Em Andamento")]
        InProgress = 1,
        [Description("Resolvido")]
        Resolved = 2,
        [Description("Cancelado")]
        Canceled = 3
    }
}

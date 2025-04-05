using System.ComponentModel;

namespace CallServiceFlow.Model.Enums
{
    public enum Status
    {
        [Description("Aberto")]
        Open = 1,
        [Description("Em Andamento")]
        InProgress = 2,
        [Description("Resolvido")]
        Resolved = 3,
        [Description("Cancelado")]
        Canceled = 4
    }
}

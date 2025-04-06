using System.ComponentModel;

namespace CallServiceFlow.Model.Enums
{
    public enum Priority
    {
        [Description("Baixa")]
        Low = 0,
        [Description("Média")]
        Medium = 1,
        [Description("Alta")]
        High = 2,
        [Description("Crítica")]
        Critical = 3
    }
}

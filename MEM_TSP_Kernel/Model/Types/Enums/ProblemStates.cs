namespace MEM_TSP.Kernel.Model.Enums
{
    /// ****************************************************************************************************************
    /// <summary>
    /// Beschreibt die verschiedenen Stati eines TSP-Objekts
    /// </summary>
    /// ****************************************************************************************************************
    public enum ProblemStates
    {
        /// <summary>Das Problem wurde geloest</summary>
        solved,

        /// <summary>Das Problem wurde noch nicht geloest</summary>
        open,

        /// <summary>Das Problem wird aktuell geloest</summary>
        running,

        /// <summary>Das Problem wurde noch nicht geloest und kann wieder aufgenommen werden</summary>
        resumeable
    }
}

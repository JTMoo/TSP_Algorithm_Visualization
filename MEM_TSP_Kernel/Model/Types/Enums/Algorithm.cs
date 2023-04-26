using System.ComponentModel;


namespace MEM_TSP.Kernel.Model.Enums
{
    /// ****************************************************************************************************************
    /// <summary>
    /// Beschreibt die Namen der vorhandenen Algorithmen
    /// </summary>
    /// ****************************************************************************************************************
    public enum Algorithm
    {
        /// <summary>Der Nearest Neighbor Algorithmus.</summary>
        [Description("NearestNeighbor")]
        NearestNeighbor,

        /// <summary>Der Random Algorithmus</summary>
        [Description("Random")]
        Random,

        /// <summary>Der GAN Algorithmus</summary>
        [Description("GAN")]
        GAN
    }
}

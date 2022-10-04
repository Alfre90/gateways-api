namespace Gateways.Services.Common.Validations
{
    /// <summary>
    /// Reference validation action
    /// </summary>
    public enum ValidationAction : int
    {
        #region General rules

        /// <summary>
        /// CRUD Add
        /// </summary>
        Add,

        /// <summary>
        /// CRUD Update
        /// </summary>
        Update,

        /// <summary>
        /// CRUD Delete
        /// </summary>
        Delete,

        /// <summary>
        /// Add Return transaction
        /// </summary>
        AddReturn

        #endregion

    }
}

using System;

namespace Peachpie.WordPress
{
    /// <summary>
    /// Represents a WordPress website.
    /// </summary>
    public interface IWpApp
    {
        /// <summary>
        /// Gets WordPress version string.
        /// </summary>
        string GetVersion();

        /// <summary>
        /// Calls <c>add_filter</c> function, <see cref="https://developer.wordpress.org/reference/functions/add_filter/"/>.
        /// </summary>
        void AddFilter(string tag, Delegate @delegate);

        /// <summary>
        /// Writes text to the output.
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        void Echo(string text);
    }
}

using System;

namespace MoviesApp.Model
{
    /// <summary>
    /// Represents a Language entity in the system.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Gets or sets the unique identifier for the language.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the language.
        /// Defaults to an empty string if not set.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}

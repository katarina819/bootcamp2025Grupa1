using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Model
{
    /// <summary>
    /// Represents a Director entity in the system.
    /// </summary>
    public class Director
    {
        /// <summary>
        /// Gets or sets the unique identifier for the director.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the director.
        /// This field is required and cannot exceed 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public required string Name { get; set; }
    }
}

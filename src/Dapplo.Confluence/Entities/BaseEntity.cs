using System.Runtime.Serialization;

namespace Dapplo.Confluence.Entities
{
    /// <summary>
    ///     Base entity, defines things that are in every entity.
    ///     Content, Space, Attachments are all entities.
    /// </summary>
    public class BaseEntity<TId>
    {
        /// <summary>
        ///     Unique ID for this entity
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public TId Id { get; set; }

        /// <summary>
        ///     Different links for this entity, depending on the entity
        /// </summary>
        [DataMember(Name = "_links", EmitDefaultValue = false)]
        public Links Links { get; set; }

        /// <summary>
        ///     Type of the entity
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }
    }
}

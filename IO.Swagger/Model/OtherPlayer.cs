/* 
 * Klootzakken API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace IO.Swagger.Model
{
    /// <summary>
    /// OtherPlayer
    /// </summary>
    [DataContract]
    public partial class OtherPlayer :  IEquatable<OtherPlayer>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OtherPlayer" /> class.
        /// </summary>
        /// <param name="IsActive">IsActive.</param>
        /// <param name="User">User.</param>
        /// <param name="PlaysThisRound">PlaysThisRound.</param>
        public OtherPlayer(bool? IsActive = default(bool?), User User = default(User), List<PlayView> PlaysThisRound = default(List<PlayView>))
        {
            this.IsActive = IsActive;
            this.User = User;
            this.PlaysThisRound = PlaysThisRound;
        }
        
        /// <summary>
        /// Gets or Sets CardCount
        /// </summary>
        [DataMember(Name="cardCount", EmitDefaultValue=false)]
        public int? CardCount { get; private set; }
        /// <summary>
        /// Gets or Sets ExchangedCardsCount
        /// </summary>
        [DataMember(Name="exchangedCardsCount", EmitDefaultValue=false)]
        public int? ExchangedCardsCount { get; private set; }
        /// <summary>
        /// Gets or Sets IsActive
        /// </summary>
        [DataMember(Name="isActive", EmitDefaultValue=false)]
        public bool? IsActive { get; set; }
        /// <summary>
        /// Gets or Sets User
        /// </summary>
        [DataMember(Name="user", EmitDefaultValue=false)]
        public User User { get; set; }
        /// <summary>
        /// Gets or Sets PlaysThisRound
        /// </summary>
        [DataMember(Name="playsThisRound", EmitDefaultValue=false)]
        public List<PlayView> PlaysThisRound { get; set; }
        /// <summary>
        /// Gets or Sets NewRank
        /// </summary>
        [DataMember(Name="newRank", EmitDefaultValue=false)]
        public string NewRank { get; private set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OtherPlayer {\n");
            sb.Append("  CardCount: ").Append(CardCount).Append("\n");
            sb.Append("  ExchangedCardsCount: ").Append(ExchangedCardsCount).Append("\n");
            sb.Append("  IsActive: ").Append(IsActive).Append("\n");
            sb.Append("  User: ").Append(User).Append("\n");
            sb.Append("  PlaysThisRound: ").Append(PlaysThisRound).Append("\n");
            sb.Append("  NewRank: ").Append(NewRank).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as OtherPlayer);
        }

        /// <summary>
        /// Returns true if OtherPlayer instances are equal
        /// </summary>
        /// <param name="other">Instance of OtherPlayer to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OtherPlayer other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.CardCount == other.CardCount ||
                    this.CardCount != null &&
                    this.CardCount.Equals(other.CardCount)
                ) && 
                (
                    this.ExchangedCardsCount == other.ExchangedCardsCount ||
                    this.ExchangedCardsCount != null &&
                    this.ExchangedCardsCount.Equals(other.ExchangedCardsCount)
                ) && 
                (
                    this.IsActive == other.IsActive ||
                    this.IsActive != null &&
                    this.IsActive.Equals(other.IsActive)
                ) && 
                (
                    this.User == other.User ||
                    this.User != null &&
                    this.User.Equals(other.User)
                ) && 
                (
                    this.PlaysThisRound == other.PlaysThisRound ||
                    this.PlaysThisRound != null &&
                    this.PlaysThisRound.SequenceEqual(other.PlaysThisRound)
                ) && 
                (
                    this.NewRank == other.NewRank ||
                    this.NewRank != null &&
                    this.NewRank.Equals(other.NewRank)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.CardCount != null)
                    hash = hash * 59 + this.CardCount.GetHashCode();
                if (this.ExchangedCardsCount != null)
                    hash = hash * 59 + this.ExchangedCardsCount.GetHashCode();
                if (this.IsActive != null)
                    hash = hash * 59 + this.IsActive.GetHashCode();
                if (this.User != null)
                    hash = hash * 59 + this.User.GetHashCode();
                if (this.PlaysThisRound != null)
                    hash = hash * 59 + this.PlaysThisRound.GetHashCode();
                if (this.NewRank != null)
                    hash = hash * 59 + this.NewRank.GetHashCode();
                return hash;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        { 
            yield break;
        }
    }

}
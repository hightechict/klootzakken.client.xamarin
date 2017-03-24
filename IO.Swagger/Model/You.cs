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
    /// You
    /// </summary>
    [DataContract]
    public partial class You :  IEquatable<You>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="You" /> class.
        /// </summary>
        /// <param name="CardsInHand">CardsInHand.</param>
        /// <param name="PossibleActions">PossibleActions.</param>
        /// <param name="ExchangedCards">ExchangedCards.</param>
        /// <param name="User">User.</param>
        /// <param name="PlaysThisRound">PlaysThisRound.</param>
        public You(List<string> CardsInHand = default(List<string>), List<PlayView> PossibleActions = default(List<PlayView>), List<string> ExchangedCards = default(List<string>), User User = default(User), List<PlayView> PlaysThisRound = default(List<PlayView>))
        {
            this.CardsInHand = CardsInHand;
            this.PossibleActions = PossibleActions;
            this.ExchangedCards = ExchangedCards;
            this.User = User;
            this.PlaysThisRound = PlaysThisRound;
        }
        
        /// <summary>
        /// Gets or Sets CardsInHand
        /// </summary>
        [DataMember(Name="cardsInHand", EmitDefaultValue=false)]
        public List<string> CardsInHand { get; set; }
        /// <summary>
        /// Gets or Sets PossibleActions
        /// </summary>
        [DataMember(Name="possibleActions", EmitDefaultValue=false)]
        public List<PlayView> PossibleActions { get; set; }
        /// <summary>
        /// Gets or Sets ExchangedCards
        /// </summary>
        [DataMember(Name="exchangedCards", EmitDefaultValue=false)]
        public List<string> ExchangedCards { get; set; }
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
            sb.Append("class You {\n");
            sb.Append("  CardsInHand: ").Append(CardsInHand).Append("\n");
            sb.Append("  PossibleActions: ").Append(PossibleActions).Append("\n");
            sb.Append("  ExchangedCards: ").Append(ExchangedCards).Append("\n");
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
            return this.Equals(obj as You);
        }

        /// <summary>
        /// Returns true if You instances are equal
        /// </summary>
        /// <param name="other">Instance of You to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(You other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.CardsInHand == other.CardsInHand ||
                    this.CardsInHand != null &&
                    this.CardsInHand.SequenceEqual(other.CardsInHand)
                ) && 
                (
                    this.PossibleActions == other.PossibleActions ||
                    this.PossibleActions != null &&
                    this.PossibleActions.SequenceEqual(other.PossibleActions)
                ) && 
                (
                    this.ExchangedCards == other.ExchangedCards ||
                    this.ExchangedCards != null &&
                    this.ExchangedCards.SequenceEqual(other.ExchangedCards)
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
                if (this.CardsInHand != null)
                    hash = hash * 59 + this.CardsInHand.GetHashCode();
                if (this.PossibleActions != null)
                    hash = hash * 59 + this.PossibleActions.GetHashCode();
                if (this.ExchangedCards != null)
                    hash = hash * 59 + this.ExchangedCards.GetHashCode();
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
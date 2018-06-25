using System;
using System.Runtime.Serialization;

namespace PawsWCF.Contract
{
    [DataContract]
    public class PetContract
    {
        [DataMember(Order = 1)] public int Id { get; set; }
        [DataMember(Order = 2)] public string Name { get; set; }
        [DataMember(Order = 3)] public string Age { get; set; }
        [DataMember(Order = 4)] public string Description { get; set; }
        [DataMember(Order = 5)] public string Picture { get; set; }
        [DataMember(Order = 6)] public DateTime PublishDate { get; set; }
        [DataMember(Order = 7)] public bool State { get; set; }
        [DataMember(Order = 8)] public string OtherRace { get; set; }
        [DataMember(Order = 9)] public int SpecieId { get; set; }
        [DataMember(Order = 10)] public int RaceId { get; set; }
        [DataMember(Order = 11)] public int OwnerId { get; set; }
        //WE MARK ISREQUIERED TO PREVENT EXCEPTIONS WHEN DESERIALIZING THE REQUEST BODY AND EMITDEFAULTVALUE TO NOT RETURN THE PROPERTY IN THE RESPONSE BODY
        //SO THESE CAN ONLY BE RECEIEVED, BUT THEY ARE OPTIONAL AND ALSO THEY ARE NOT SENT WHEN NO VALUE IS ASSIGNED TO THEM
        [DataMember(Order = 12, IsRequired = false, EmitDefaultValue = false)] public string ImageBase64 { get; set; }
        [DataMember(Order = 13, IsRequired = false, EmitDefaultValue = false)] public string ImageExtension { get; set; }
    }
}
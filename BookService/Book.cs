using System;
using System.Runtime.Serialization;

namespace BookService
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Genre { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public DateTime PublishDate { get; set; }
    }
}